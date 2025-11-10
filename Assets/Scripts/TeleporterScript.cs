using UnityEngine;
using TMPro;

public class TeleporterScript : MonoBehaviour
{
    public TMP_Text promptText; // Assign a TextMeshPro UI element in the Inspector
    private bool isPlayerNear = false;

    void Start()
    {
        if (promptText != null)
        {
            promptText.gameObject.SetActive(false); // Hide prompt at start
        }
    }

    void Update()
    {
        if (isPlayerNear && Input.GetKeyDown(KeyCode.E))
        {
            // Use the new Unity API to find the CollectibleUI
            CollectibleUI ui = FindFirstObjectByType<CollectibleUI>();
            if (ui != null)
            {
                ui.ReturnToMainScene();
            }
            else
            {
                Debug.LogWarning("No CollectibleUI found in scene!");
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true;
            if (promptText != null)
            {
                promptText.text = "Press E to return to Main Scene";
                promptText.gameObject.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
            if (promptText != null)
            {
                promptText.gameObject.SetActive(false);
            }
        }
    }
}
