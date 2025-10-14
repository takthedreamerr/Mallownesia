using TMPro;
using UnityEngine;

public class KeypadController : MonoBehaviour
{
    [Header("Keypad Settings")]
    [SerializeField] private string correctCode = "1234";
    [SerializeField] private DoorController door;
    [SerializeField] private TextMeshPro textMeshProDisplay;

    private string enteredCode = "";
    private bool isActive = true; // Always active if using keyboard

    private void Update()
    {
        if (!isActive) return;

        // Numbers 0-9
        for (int i = 0; i <= 9; i++)
        {
            if (Input.GetKeyDown(i.ToString()))
            {
                AddDigit(i.ToString());
            }
        }

        // Enter key
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            EnterCode();
        }

        // Backspace / Clear
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            ClearCode();
        }
    }

    public void AddDigit(string digit)
    {
        if (enteredCode.Length < 4)
        {
            enteredCode += digit;
            UpdateDisplay();
        }
    }

    public void ClearCode()
    {
        enteredCode = "";
        UpdateDisplay();
    }

    public void EnterCode()
    {
        if (enteredCode == correctCode)
        {
            Debug.Log("Correct code entered! Opening door...");
            if (door != null)
                door.OpenDoorFromKeypad();
            ClearCode();
        }
        else
        {
            Debug.LogWarning("Incorrect code entered. Try again.");
            ClearCode();
        }
    }

    private void UpdateDisplay()
    {
        if (textMeshProDisplay != null)
            textMeshProDisplay.text = enteredCode;
    }
}
