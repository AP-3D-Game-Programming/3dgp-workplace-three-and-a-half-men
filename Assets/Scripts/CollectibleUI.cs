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
            
            // Save progress: mark the current room as done
            string currentRoom = SceneManager.GetActiveScene().name;
            PlayerPrefs.SetInt(currentRoom + "_Completed", 1);
            PlayerPrefs.Save();

            SceneManager.LoadScene("Scenes/MainScene");
        }
    }
    void UpdateUI()
    {
        collectibleText.text = "Remaining: " + totalCollectibles;
    }
    public void ReturnToMainScene()
    {
        Debug.Log("Returning to MainScene...");
        SceneManager.LoadScene("Scenes/MainScene");
    }
}
