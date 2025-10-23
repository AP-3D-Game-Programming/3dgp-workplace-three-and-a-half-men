using UnityEngine;
using TMPro;

public class TutorialZone : MonoBehaviour
{
    public TMP_Text tutorialText;   // Gösterilecek yazı
    [TextArea]
    public string message;          // Bu bölgeye özel mesaj
    public bool hideOnExit = true;  // Çıkınca yazı gizlensin mi?

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            tutorialText.text = message;
            tutorialText.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && hideOnExit)
        {
            tutorialText.gameObject.SetActive(false);
        }
    }
}
