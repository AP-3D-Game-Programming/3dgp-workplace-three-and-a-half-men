using UnityEngine;
using TMPro;

[RequireComponent(typeof(AudioSource))]
public class SimpleCollectibleScript : MonoBehaviour
{
    public enum CollectibleTypes { NoType, Type1, Type2, Type3, Type4, Type5 };
    public CollectibleTypes CollectibleType;

    public bool rotate;
    public float rotationSpeed;
    public AudioClip collectSound;
    public GameObject collectEffect;

    [Header("UI Settings")]
    public TextMeshProUGUI pressEText; // Inspector'dan atayacağız

    private bool playerInRange = false;

    void Update()
    {
        if (rotate)
            transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime, Space.World);

        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            Collect();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            if (pressEText != null)
                pressEText.gameObject.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            if (pressEText != null)
                pressEText.gameObject.SetActive(false);
        }
    }

    void Collect()
    {
        if (collectSound)
            AudioSource.PlayClipAtPoint(collectSound, transform.position);
        if (collectEffect)
            Instantiate(collectEffect, transform.position, Quaternion.identity);

        CollectibleUI ui = Object.FindFirstObjectByType<CollectibleUI>();
        if (ui != null)
            ui.CollectibleCollected();

        if (pressEText != null)
            pressEText.gameObject.SetActive(false);

        Destroy(gameObject);
    }
}
