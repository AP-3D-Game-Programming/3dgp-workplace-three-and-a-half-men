using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

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

         if (totalCollectibles <= 0)
        {
            Debug.Log("All collectibles collected! Returning to MainScene...");
            SceneManager.LoadScene("Scenes/MainScene");
        }
    }
    void UpdateUI()
    {
        collectibleText.text = "Remaining: " + totalCollectibles;
    }
}
