using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RemoteControl : MonoBehaviour
{
    [Header("TV Channels (assign panels / quads)")]
    [SerializeField] private List<GameObject> tvChannels = new List<GameObject>();
    private int currentChannel = 0;

    [Header("Speaker")]
    [SerializeField] private AudioSource speakerAudio;
    private bool speakerOn = false;

    [Header("UI Buttons (drag from the Canvas)")]
    [SerializeField] private Button channelLeftButton;
    [SerializeField] private Button channelRightButton;
    [SerializeField] private Button speakerOnButton;
    [SerializeField] private Button speakerOffButton;

    [Header("Optional: UI text for channel/speaker feedback")]
    [SerializeField] private TextMeshProUGUI channelLabel; // If using TextMeshPro, change to TextMeshProUGUI
    [SerializeField] private TextMeshProUGUI speakerLabel;

    [Header("UI root (so we can enable/disable when picked up)")]
    [SerializeField] private GameObject remoteUIRoot;

    private void Start()
    {
        if (channelLeftButton != null) channelLeftButton.onClick.AddListener(ChannelLeft);
        if (channelRightButton != null) channelRightButton.onClick.AddListener(ChannelRight);

        if (speakerOnButton != null) speakerOnButton.onClick.AddListener(SpeakerOn);
        if (speakerOffButton != null) speakerOffButton.onClick.AddListener(SpeakerOff);

        if (remoteUIRoot != null) remoteUIRoot.SetActive(false);

        UpdateChannels();
        UpdateSpeakerUI();
    }

    public void SpeakerOn()
    {
        speakerOn = true;
        if (speakerAudio != null) speakerAudio.UnPause();
        UpdateSpeakerUI();
    }

    public void SpeakerOff()
    {
        speakerOn = false;
        if (speakerAudio != null) speakerAudio.Pause();
        UpdateSpeakerUI();
    }

    public void ChannelLeft()
    {
        if (tvChannels == null || tvChannels.Count == 0) return;
        currentChannel = (currentChannel - 1 + tvChannels.Count) % tvChannels.Count;
        UpdateChannels();
    }

    public void ChannelRight()
    {
        if (tvChannels == null || tvChannels.Count == 0) return;
        currentChannel = (currentChannel + 1) % tvChannels.Count;
        UpdateChannels();
    }

    public void ToggleSpeaker()
    {
        speakerOn = !speakerOn;
        if (speakerAudio != null)
        {
            if (speakerOn) speakerAudio.UnPause();
            else speakerAudio.Pause();
        }
        UpdateSpeakerUI();
    }

    private void UpdateChannels()
    {
        for (int i = 0; i < tvChannels.Count; i++)
            if (tvChannels[i] != null) tvChannels[i].SetActive(i == currentChannel);

        if (channelLabel != null)
            channelLabel.text = tvChannels.Count > 0 ? $"CH {currentChannel + 1} / {tvChannels.Count}" : "No channels";
    }

    private void UpdateSpeakerUI()
    {
        if (speakerLabel != null) speakerLabel.text = speakerOn ? "Speaker: On" : "Speaker: Off";
    }

    public void SetUIActive(bool on)
    {
        if (remoteUIRoot != null) remoteUIRoot.SetActive(on);
    }
}

