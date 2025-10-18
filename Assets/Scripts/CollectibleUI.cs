using UnityEngine;
using TMPro;

public class CollectibleUI : MonoBehaviour
{
    public TMP_Text collectibleText; 
    private int totalCollectibles;

    void Start()
    {
    
        totalCollectibles = GameObject.FindGameObjectsWithTag("PlayerCollectible").Length;
        UpdateUI();
    }

    public void CollectibleCollected()
    {
        totalCollectibles--;
        UpdateUI();
    }
    void UpdateUI()
    {
        collectibleText.text = "Remaining: " + totalCollectibles;
    }
}
