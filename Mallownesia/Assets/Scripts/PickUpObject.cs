using TMPro;
using UnityEngine;

public class PickUpObject_Remote : MonoBehaviour
{
    [Header("References")]
    public GameObject PickObjct; // 3D remote
    public GameObject remote;    // remote UI panel
    public TextMeshProUGUI interact; // "Press E"
   
    private bool holdingRemote = false;
    private bool canPickUp = false;

    private void Awake()
    {
        if (remote != null) remote.SetActive(false); 
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (holdingRemote)
            {
                // Drop remote
                if (remote != null) remote.SetActive(false);
                if (PickObjct != null) PickObjct.SetActive(true);

                holdingRemote = false;
            }
        } 
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pickupable"))
        {
            if (interact != null)
            {
                interact.text = "Press E to pick up Remote";
                interact.gameObject.SetActive(true);
                Debug.Log("PICKED UP REMOTE");
            }
            canPickUp = true;
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Pickupable"))
        {
            if (!holdingRemote && interact != null)
                interact.gameObject.SetActive(false);
            canPickUp = false;
        }
    }
}
