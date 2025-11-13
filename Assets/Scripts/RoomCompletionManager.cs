using UnityEngine;

public class RoomCompletionManager : MonoBehaviour
{
    [System.Serializable]
    public class RoomCompletion
    {
        public string roomName;
        public GameObject completionObject; // object to show when completed
    }

    public RoomCompletion[] rooms;

    void Start()
    {
        foreach (var room in rooms)
        {
            // check if this room was completed
            int completed = PlayerPrefs.GetInt(room.roomName + "_Completed", 0);
            room.completionObject.SetActive(completed == 1);
        }
    }
}

