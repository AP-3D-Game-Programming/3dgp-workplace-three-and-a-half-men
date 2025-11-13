using UnityEngine;
using TMPro;

public class message : MonoBehaviour
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
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true;
            if (promptText != null)
            {
                promptText.text = "Be careful that you don't fall through the ice!!!";
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

