using UnityEngine;
using TMPro;

public class UIContextController : MonoBehaviour
{
    [Header("Prompt")]
    [SerializeField] TMP_Text promptText;

    [Header("Panels")]
    [SerializeField] GameObject phonePanel;
    [SerializeField] GameObject remotePanel;
    [SerializeField] GameObject bookPanel;

    void OnEnable() => GameState.OnModeChanged += HandleMode;
    void OnDisable() => GameState.OnModeChanged -= HandleMode;

    void HandleMode(GameMode m)
    {
        bool showPrompt = (m == GameMode.Exploration);
        if (promptText) promptText.gameObject.SetActive(showPrompt);
        if (!showPrompt && promptText) promptText.text = "";
    }

    public void SetPrompt(string text)
    {
        if (promptText && GameState.IsFreeToInteract) promptText.text = text;
    }
    public void ClearPrompt() { if (promptText) promptText.text = ""; }

    public void ShowPhoneUI(bool on) { if (phonePanel) phonePanel.SetActive(on); GameState.Set(on ? GameMode.InPuzzleUI : GameMode.Exploration); }
    public void ShowRemoteUI(bool on) { if (remotePanel) remotePanel.SetActive(on); GameState.Set(on ? GameMode.InPuzzleUI : GameMode.Exploration); }
    public void ShowBookUI(bool on) { if (bookPanel) bookPanel.SetActive(on); GameState.Set(on ? GameMode.InPuzzleUI : GameMode.Exploration); }

}
