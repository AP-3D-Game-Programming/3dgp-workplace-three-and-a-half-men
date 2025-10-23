using UnityEngine;

public class TutorialZone : MonoBehaviour
{
    public enum ZoneType { Move, Jump, Collect }
    public ZoneType zoneType;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var manager = FindFirstObjectByType<TutorialManager>();
            if (manager == null) return;

            switch (zoneType)
            {
                case ZoneType.Move:
                    manager.ShowMessage(manager.moveMessage);
                    break;
                case ZoneType.Jump:
                    manager.ShowMessage(manager.jumpMessage);
                    break;
                case ZoneType.Collect:
                    manager.ShowMessage(manager.collectMessage);
                    break;
            }
        }
    }
}
