using UnityEngine;

public class ZoneTrigger : MonoBehaviour
{
    public NPCZoneTriggerManager manager;
    public int zoneNumber = 1; // 1 or 2

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag(manager.playerTag)) return;

        if (zoneNumber == 1)
            manager.TriggerZone1();
        else if (zoneNumber == 2)
            manager.TriggerZone2();
    }
}
