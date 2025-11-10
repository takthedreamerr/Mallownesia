using UnityEngine;
using System.Collections;

public class NPCZoneTriggerManager : MonoBehaviour
{
    [Header("NPC Settings")]
    public Animator npcAnimator;               // NPC's Animator

    [Header("Player Settings")]
    public string playerTag = "Player";        // Player's tag

    [Header("Zone Settings")]
    public Collider zone1Collider;             // First trigger collider
    public string zone1Animation = "Hurry";    // Animation trigger for zone 1

    public Collider zone2Collider;             // Second trigger collider
    public string zone2Animation = "Run";      // Animation trigger for zone 2
    public float zone2Delay = 15f;             // Delay before Zone 2 is enabled

    private bool zone1Played = false;
    private bool zone2Played = false;

    private void Start()
    {
        if (npcAnimator == null)
            Debug.LogWarning("NPC Animator not assigned!");

        // Ensure colliders exist and are triggers
        if (zone1Collider != null)
            zone1Collider.isTrigger = true;
        else
            Debug.LogError("Zone 1 Collider not assigned!");

        if (zone2Collider != null)
        {
            zone2Collider.isTrigger = true;
            zone2Collider.enabled = false; // Zone 2 starts disabled
        }
        else
            Debug.LogError("Zone 2 Collider not assigned!");
    }

    // This function should be attached to both zone colliders as a trigger
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag(playerTag))
            return;

        // Zone 1 logic
        if (!zone1Played && other == null) // Placeholder, replaced below
        {
            // We actually need to detect **which collider triggered this**
            // So attach this script to an empty manager object and call via ZoneTrigger scripts
        }
    }

    // --- Public methods called from ZoneTrigger scripts ---
    public void TriggerZone1()
    {
        if (zone1Played) return;

        zone1Played = true;
        zone1Collider.enabled = false;

        // Trigger animation first
        npcAnimator.SetTrigger(zone1Animation);

        // Play sound immediately after — same frame, same Update call
        SoundManager.Instance.PlaySound3();

        Debug.Log("Zone 1 triggered animation and sound: " + zone1Animation);

        StartCoroutine(EnableZone2AfterDelay());
    }


    public void TriggerZone2()
    {
        if (zone2Played || !zone2Collider.enabled) return;

        zone2Played = true;
        zone2Collider.enabled = false;
        npcAnimator.SetTrigger(zone2Animation);
        Debug.Log("Zone 2 triggered animation: " + zone2Animation);
    }

    private IEnumerator EnableZone2AfterDelay()
    {
        yield return new WaitForSeconds(zone2Delay);
        if (zone2Collider != null)
        {
            zone2Collider.enabled = true;
            Debug.Log("Zone 2 is now active after delay.");
        }
    }
}
