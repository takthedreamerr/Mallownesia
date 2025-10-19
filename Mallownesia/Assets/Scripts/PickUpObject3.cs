using TMPro;
using UnityEngine;

public class PickUpObject_Invite : MonoBehaviour
{
    [Header("References")]
    public TextMeshProUGUI interact; // "Press E"
    public GameObject InvitePanel;

    private bool nearInvite = false;

    private void Awake()
    {
        if (interact != null) interact.gameObject.SetActive(false);
        if (InvitePanel != null) InvitePanel.SetActive(false);   
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
             if (nearInvite)
            {
                Debug.Log("Interacted with book!");
                if (InvitePanel != null) InvitePanel.SetActive(true);
                if (interact != null) interact.gameObject.SetActive(false);
            }
           
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("InvitePoster"))
        {
            if (interact != null)
            {
                
                interact.gameObject.SetActive(true);
                Debug.Log("INVITE OPEN");
            }
            nearInvite = true;
        }
    }
}
