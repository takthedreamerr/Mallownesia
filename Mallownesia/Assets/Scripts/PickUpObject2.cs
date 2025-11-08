using TMPro;
using UnityEngine;

public class PickUpObject2 : MonoBehaviour
{
    ///////////////////////// BOOKS //////////////////////////

    [Header("References")]
    public TextMeshProUGUI interact; // "Press E"
    public GameObject bookPanel;
    public GameObject key1;

    private bool nearBook = false;
    public ParticleSystem bookParticleSystem; // Reference to the book's particle system
    public Light bookLight; // Reference to the book's light

    private void Awake()
    {
        if (interact != null) interact.gameObject.SetActive(false);
        if (bookPanel != null) bookPanel.SetActive(false);
        if (key1 != null) key1.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (nearBook)
            {
                Debug.Log("Interacted with book!");
                if (bookPanel != null) bookPanel.SetActive(true);
                if (interact != null) interact.gameObject.SetActive(false);

                // Stop the particle system when book panel is opened
                StopBookParticleSystem();
                StopBookLight();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Book"))
        {
            if (interact != null)
            {
                interact.text = "Press E to read Book";
                interact.gameObject.SetActive(true);
                key1.SetActive(true);
                Debug.Log("BOOK OPEN");
            }
            nearBook = true;

            // Get the particle system from the book object
            bookParticleSystem = other.GetComponentInChildren<ParticleSystem>();

            // If we found a particle system and it's not playing, start it
            if (bookParticleSystem != null && !bookParticleSystem.isPlaying)
            {
                bookParticleSystem.Play();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Book"))
        {
            if (interact != null) interact.gameObject.SetActive(false);
            nearBook = false;
        }
    }

    public void CloseBookPanel()
    {
        if (bookPanel != null)
        {
            bookPanel.SetActive(false);

            // DON'T restart the particle system when book panel is closed
            // The particle system should remain stopped
        }
    }

    private void StopBookParticleSystem()
    {
        if (bookParticleSystem != null && bookParticleSystem.isPlaying)
        {
            bookParticleSystem.Stop();
        }
    }

    private void StopBookLight()
    {
        if (bookLight != null && bookLight.enabled)
        {
            bookLight.enabled = false;
        }
    }
}