using TMPro;
using UnityEngine;

public enum TargetType { None, PickupRemote, Book, Key, Keypad, Door, LockerDoor }
public class PickUpObject : MonoBehaviour
{
    [Header("References")]
    public GameObject PickObjct; // your “hands free” model (optional)
    public GameObject remote;    // the remote-in-hand (optional)
    public TMPro.TextMeshProUGUI interact; // not used anymore (kept for compatibility)
    public GameObject bookPanel; // legacy ref if you need it
    public Inventory playerInventory;

    [Header("Managers")]
    [SerializeField] UIContextController ui;

    TargetType current = TargetType.None;
    KeyItem nearbyKey;
    KeypadController nearbyKeypad;
    DoorController nearbyDoor;
    //LockerDoor lockerDoor;

    void Awake()
    {
        if (!ui) ui = FindFirstObjectByType<UIContextController>();
        if (!playerInventory) playerInventory = FindFirstObjectByType<Inventory>();
    }

    void Update()
    {
        if (!GameState.IsFreeToInteract) return;

        if (current != TargetType.None && Input.GetKeyDown(KeyCode.E))
        {
            switch (current)
            {
                case TargetType.PickupRemote:
                    bool nowHolding = !remote.activeSelf;
                    remote.SetActive(nowHolding);
                    if (PickObjct) PickObjct.SetActive(!nowHolding);
                    ui?.ClearPrompt();
                    break;

                case TargetType.Book:
                    ui?.ShowBookUI(true);
                    ui?.ClearPrompt();
                    break;

                case TargetType.Key:
                    if (nearbyKey)
                    {
                        playerInventory.AddKey(nearbyKey);
                        Destroy(nearbyKey.gameObject);
                        nearbyKey = null;
                        ui?.ClearPrompt();
                        //PickupJuice.SpawnAt(transform.position);
                        //SoundManager.Play("pickup");
                    }
                    break;

                case TargetType.Keypad:
                    if (nearbyKeypad)
                    {
                        ui?.ShowPhoneUI(true);
                        //nearbyKeypad.Open();
                    }
                    break;

                case TargetType.Door:
                    //if (nearbyDoor) nearbyDoor.TryInteract(playerInventory);
                    break;

               // case TargetType.LockerDoor:
                  //  if (lockerDoor) lockerDoor.Toggle();
                   // break;
            }
        }
    }

    void SetTarget(TargetType t, string prompt)
    {
        if (current == t) return;
        current = t;
        if (t == TargetType.None) ui?.ClearPrompt();
        else ui?.SetPrompt(prompt);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pickupable")) SetTarget(TargetType.PickupRemote, "Pick up Remote [E]");
        else if (other.CompareTag("Book")) SetTarget(TargetType.Book, "Read Note [E]");
        else if (other.CompareTag("Key")) { nearbyKey = other.GetComponent<KeyItem>(); SetTarget(TargetType.Key, "Pick up Key [E]"); }
        else if (other.CompareTag("Keypad")) { nearbyKeypad = other.GetComponent<KeypadController>(); SetTarget(TargetType.Keypad, "Use Phone Keypad [E]"); }
        else if (other.CompareTag("Door"))
        {
            nearbyDoor = other.GetComponentInParent<DoorController>();
           // string p = (nearbyDoor && nearbyDoor.IsKeypadDoor) ? "Door is locked by keypad" :
                       //(nearbyDoor && nearbyDoor.CanOpenWith(playerInventory)) ? "Unlock Door [E]" :
                       //"Door is locked";
            //SetTarget(TargetType.Door, p);
        }
        else if (other.CompareTag("LockerDoor"))
        {
            //lockerDoor = other.GetComponentInParent<LockerDoor>();
            SetTarget(TargetType.LockerDoor, "Open Panel [E]");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Pickupable") && current == TargetType.PickupRemote) SetTarget(TargetType.None, "");
        else if (other.CompareTag("Book") && current == TargetType.Book) SetTarget(TargetType.None, "");
        else if (other.CompareTag("Key") && other.GetComponent<KeyItem>() == nearbyKey) { nearbyKey = null; SetTarget(TargetType.None, ""); }
        else if (other.CompareTag("Keypad") && other.GetComponent<KeypadController>() == nearbyKeypad) { nearbyKeypad = null; SetTarget(TargetType.None, ""); }
        else if (other.CompareTag("Door") && other.GetComponentInParent<DoorController>() == nearbyDoor) { nearbyDoor = null; SetTarget(TargetType.None, ""); }
        //else if (other.CompareTag("LockerDoor") && other.GetComponentInParent<LockerDoor>() == lockerDoor) { lockerDoor = null; SetTarget(TargetType.None, ""); }
    }
}
