using UnityEngine;
using System.Collections;

[RequireComponent(typeof(MeshCollider))]
public class BreakablePlatform : MonoBehaviour
{
    [Tooltip("Tijd in seconden voordat het platform breekt nadat de speler op de trigger staat")]
    public float breakDelay = 3f;

    [Tooltip("Trigger Box (optioneel: als leeg wordt er automatisch 1 aangemaakt net boven de mesh)")]
    public BoxCollider triggerBox;

    [Tooltip("Herstelt het platform wanneer de speler het verlaat?")]
    public bool reEnableOnExit = false;

    [Tooltip("Schakel ook de MeshRenderer uit (visual) wanneer het breekt)")]
    public bool hideMeshOnBreak = true;

    private MeshCollider meshCollider;
    private MeshRenderer meshRenderer;
    private bool isBreaking = false;

    void Awake()
    {
        meshCollider = GetComponent<MeshCollider>();
        meshRenderer = GetComponent<MeshRenderer>();

        // Zorg dat meshCollider geen trigger is (zodat de speler erop kan staan)
        if (meshCollider) meshCollider.isTrigger = false;

        // Als geen triggerBox toegewezen: maak er eentje aan net boven de mesh
        if (triggerBox == null)
        {
            CreateTriggerBoxAboveMesh();
        }
        else
        {
            // zorg dat het een trigger is
            triggerBox.isTrigger = true;
        }
    }

    void CreateTriggerBoxAboveMesh()
    {
        // Maak een child object met een BoxCollider die net boven de mesh zit
        GameObject go = new GameObject("PlatformTriggerBox");
        go.transform.SetParent(transform, false);

        // Probeer bounds te gebruiken als er een MeshRenderer is
        Bounds b = (meshRenderer != null) ? meshRenderer.bounds : new Bounds(transform.position, Vector3.one);

        BoxCollider bc = go.AddComponent<BoxCollider>();
        bc.isTrigger = true;

        // Bepaal grootte en positie in lokale coördinaten
        // breedte en diepte van de trigger komen van de mesh bounds (x,z)
        Vector3 sizeWorld = new Vector3(b.size.x, 0.25f, b.size.z);
        // hoogte in wereldruimte waarop trigger moet zitten (net boven bovenkant van mesh)
        float yOffsetWorld = (b.extents.y + 0.125f); // een klein beetje bovenop

        // Converteer wereld-maat naar lokale voor scale/center
        // Omdat de parent mogelijk geschaald is, zetten we size op lokale door te delen door lossyScale
        Vector3 invScale = new Vector3(
            SafeDivide(1f, transform.lossyScale.x),
            SafeDivide(1f, transform.lossyScale.y),
            SafeDivide(1f, transform.lossyScale.z)
        );
        bc.size = Vector3.Scale(sizeWorld, invScale);

        // center: bepaal world center en zet teruggerekend naar local
        Vector3 triggerWorldCenter = b.center + Vector3.up * yOffsetWorld;
        bc.center = transform.InverseTransformPoint(triggerWorldCenter);

        triggerBox = bc;
    }

    float SafeDivide(float a, float b)
    {
        return (Mathf.Approximately(b, 0f)) ? 0f : a / b;
    }

    void OnTriggerEnter(Collider other)
    {
        if (isBreaking) return;
        if (other.CompareTag("Player"))
        {
            // optioneel: check of speler echt boven het platform is
            // if (other.transform.position.y < transform.position.y - 0.1f) return;

            StartCoroutine(BreakAfterDelay());
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (!reEnableOnExit) return;

        if (other.CompareTag("Player"))
        {
            // Als we full reset willen bij exit: start re-enable coroutine
            StartCoroutine(ReEnableAfterDelay());
        }
    }

    IEnumerator BreakAfterDelay()
    {
        isBreaking = true;
        yield return new WaitForSeconds(breakDelay);

        // zet mesh collider uit zodat de speler erdoorheen valt
        if (meshCollider) meshCollider.enabled = false;

        // visueel verbergen (optioneel)
        if (hideMeshOnBreak && meshRenderer) meshRenderer.enabled = false;

        // (optioneel) je kan hier ook een particle/geluid afspelen voordat je uitschakelt
    }

    IEnumerator ReEnableAfterDelay()
    {
        // wacht even voordat je hem terugzet
        yield return new WaitForSeconds(breakDelay);

        if (meshCollider) meshCollider.enabled = true;
        if (hideMeshOnBreak && meshRenderer) meshRenderer.enabled = true;
        isBreaking = false;
    }

    // Debug viz in de Scene view
    void OnDrawGizmosSelected()
    {
        if (triggerBox != null)
        {
            Gizmos.color = new Color(0f, 1f, 0f, 0.25f);
            // wereld center en world size
            Vector3 worldCenter = transform.TransformPoint(triggerBox.center);
            Vector3 worldSize = Vector3.Scale(triggerBox.size, transform.lossyScale);
            Gizmos.DrawCube(worldCenter, worldSize);
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(worldCenter, worldSize);
        }
    }
}
