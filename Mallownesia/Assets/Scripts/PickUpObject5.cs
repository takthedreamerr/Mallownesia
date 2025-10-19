using TMPro;
using UnityEngine;

public class PickUpObject_Keypad : MonoBehaviour
{
    [Header("References")]

    public TextMeshProUGUI interact; // "Press E"
    private KeypadController nearbyKeypad = null; // keypad currently in trigger

    private void Awake()
    {

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
           if (nearbyKeypad != null)
            {
                // Interact with keypad
                Debug.Log("Interacted with keypad!");
                //nearbyKeypad.OpenKeypadPanel(); // <-- Make sure your KeypadItem has this method
                if (interact != null) interact.gameObject.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Keypad"))
        {
            nearbyKeypad = other.GetComponent<KeypadController>();
            if (interact != null)
            {
                interact.text = "INTERACT USING numbers (BACKSPACE = Clear, ENTER =Enter)";
                interact.gameObject.SetActive(true);
                Debug.Log("KEYPAD IN USE");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {

        if (other.CompareTag("Keypad") && nearbyKeypad != null && other.GetComponent<KeypadController>() == nearbyKeypad)
        {
            if (interact != null) interact.gameObject.SetActive(false);
            nearbyKeypad = null;
        }
    }
}
