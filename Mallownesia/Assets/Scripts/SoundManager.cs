using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    [Header("15 Sound Clips")]
    [SerializeField] private AudioClip soundClip1;
    [SerializeField] private AudioClip soundClip2;
    [SerializeField] private AudioClip soundClip3;
    [SerializeField] private AudioClip soundClip4;
    [SerializeField] private AudioClip soundClip5;
    [SerializeField] private AudioClip soundClip6;
    [SerializeField] private AudioClip soundClip7;
    [SerializeField] private AudioClip soundClip8;
    [SerializeField] private AudioClip soundClip9;
    [SerializeField] private AudioClip soundClip10;
    [SerializeField] private AudioClip soundClip11;
    [SerializeField] private AudioClip soundClip12;
    [SerializeField] private AudioClip soundClip13;
    [SerializeField] private AudioClip soundClip14;
    [SerializeField] private AudioClip soundClip15;

    [Header("Audio Settings")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private float masterVolume = 1.0f;

    private void Awake()
    {
        // Singleton pattern
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Optional: keep between scenes
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        // Initialize audio source
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>() ?? gameObject.AddComponent<AudioSource>();
        }
        audioSource.volume = masterVolume;
    }

    // Individual play methods for each sound clip
    public void PlaySound1() => PlaySound(soundClip1);
    public void PlaySound2() => PlaySound(soundClip2);
    public void PlaySound3() => PlaySound(soundClip3);
    public void PlaySound4() => PlaySound(soundClip4);
    public void PlaySound5() => PlaySound(soundClip5);
    public void PlaySound6() => PlaySound(soundClip6);
    public void PlaySound7() => PlaySound(soundClip7);
    public void PlaySound8() => PlaySound(soundClip8);
    public void PlaySound9() => PlaySound(soundClip9);
    public void PlaySound10() => PlaySound(soundClip10);
    public void PlaySound11() => PlaySound(soundClip11);
    public void PlaySound12() => PlaySound(soundClip12);
    public void PlaySound13() => PlaySound(soundClip13);
    public void PlaySound14() => PlaySound(soundClip14);
    public void PlaySound15() => PlaySound(soundClip15);

    // Play with custom volume
    public void PlaySound1WithVolume(float volume) => PlaySoundWithVolume(soundClip1, volume);
    public void PlaySound2WithVolume(float volume) => PlaySoundWithVolume(soundClip2, volume);
    public void PlaySound3WithVolume(float volume) => PlaySoundWithVolume(soundClip3, volume);
    public void PlaySound4WithVolume(float volume) => PlaySoundWithVolume(soundClip4, volume);
    public void PlaySound5WithVolume(float volume) => PlaySoundWithVolume(soundClip5, volume);
    public void PlaySound6WithVolume(float volume) => PlaySoundWithVolume(soundClip6, volume);
    public void PlaySound7WithVolume(float volume) => PlaySoundWithVolume(soundClip7, volume);
    public void PlaySound8WithVolume(float volume) => PlaySoundWithVolume(soundClip8, volume);
    public void PlaySound9WithVolume(float volume) => PlaySoundWithVolume(soundClip9, volume);
    public void PlaySound10WithVolume(float volume) => PlaySoundWithVolume(soundClip10, volume);
    public void PlaySound11WithVolume(float volume) => PlaySoundWithVolume(soundClip11, volume);
    public void PlaySound12WithVolume(float volume) => PlaySoundWithVolume(soundClip12, volume);
    public void PlaySound13WithVolume(float volume) => PlaySoundWithVolume(soundClip13, volume);
    public void PlaySound14WithVolume(float volume) => PlaySoundWithVolume(soundClip14, volume);
    public void PlaySound15WithVolume(float volume) => PlaySoundWithVolume(soundClip15, volume);

    // Play at specific position
    public void PlaySound1AtPosition(Vector3 position) => PlaySoundAtPosition(soundClip1, position);
    public void PlaySound2AtPosition(Vector3 position) => PlaySoundAtPosition(soundClip2, position);
    public void PlaySound3AtPosition(Vector3 position) => PlaySoundAtPosition(soundClip3, position);
    public void PlaySound4AtPosition(Vector3 position) => PlaySoundAtPosition(soundClip4, position);
    public void PlaySound5AtPosition(Vector3 position) => PlaySoundAtPosition(soundClip5, position);
    public void PlaySound6AtPosition(Vector3 position) => PlaySoundAtPosition(soundClip6, position);
    public void PlaySound7AtPosition(Vector3 position) => PlaySoundAtPosition(soundClip7, position);
    public void PlaySound8AtPosition(Vector3 position) => PlaySoundAtPosition(soundClip8, position);
    public void PlaySound9AtPosition(Vector3 position) => PlaySoundAtPosition(soundClip9, position);
    public void PlaySound10AtPosition(Vector3 position) => PlaySoundAtPosition(soundClip10, position);
    public void PlaySound11AtPosition(Vector3 position) => PlaySoundAtPosition(soundClip11, position);
    public void PlaySound12AtPosition(Vector3 position) => PlaySoundAtPosition(soundClip12, position);
    public void PlaySound13AtPosition(Vector3 position) => PlaySoundAtPosition(soundClip13, position);
    public void PlaySound14AtPosition(Vector3 position) => PlaySoundAtPosition(soundClip14, position);
    public void PlaySound15AtPosition(Vector3 position) => PlaySoundAtPosition(soundClip15, position);

    // Utility methods
    public void StopAllSounds() => audioSource?.Stop();
    public void PauseAllSounds() => audioSource?.Pause();
    public void ResumeAllSounds() => audioSource?.UnPause();

    public void SetMasterVolume(float volume)
    {
        masterVolume = Mathf.Clamp01(volume);
        if (audioSource != null) audioSource.volume = masterVolume;
    }

    public float GetMasterVolume() => masterVolume;

    // Private helper methods
    private void PlaySound(AudioClip clip)
    {
        if (clip != null && audioSource != null)
        {
            audioSource.PlayOneShot(clip, masterVolume);
        }
    }

    private void PlaySoundWithVolume(AudioClip clip, float volume)
    {
        if (clip != null && audioSource != null)
        {
            audioSource.PlayOneShot(clip, Mathf.Clamp01(volume));
        }
    }

    private void PlaySoundAtPosition(AudioClip clip, Vector3 position)
    {
        if (clip != null)
        {
            AudioSource.PlayClipAtPoint(clip, position, masterVolume);
        }
    }
}
