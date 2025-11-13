using UnityEngine;

public class GameStartup : MonoBehaviour
{
    private static bool clearedThisSession = false;

    void Awake()
    {
        if (!clearedThisSession)
        {
            PlayerPrefs.DeleteAll();
            clearedThisSession = true;
            Debug.Log("PlayerPrefs cleared at startup");
        }
    }
}
