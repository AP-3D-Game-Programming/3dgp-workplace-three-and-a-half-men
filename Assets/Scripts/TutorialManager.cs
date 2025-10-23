using UnityEngine;
using TMPro;

public class TutorialManager : MonoBehaviour
{
    [Header("UI Reference")]
    public TMP_Text tutorialText;

    [Header("Tutorial Messages")]
    [TextArea] public string startMessage = "Welcome to the game!";
    [TextArea] public string moveMessage = "Move robot Kyle with 'W A S D' keys.";
    [TextArea] public string jumpMessage = "To jump Kyle, press the SPACE key.";
    [TextArea] public string collectMessage = "To collect a collectible item, press the E key.";

    private void Start()
    {
        ShowMessage(startMessage);
    }

    public void ShowMessage(string message)
    {
        tutorialText.text = message;
    }
}
