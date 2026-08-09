using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    [Header("Sub Panels")]
    public GameObject scoresPanel;
    public GameObject shopPanel;
    public GameObject howToPlayPanel;
    public GameObject settingsPanel;

    void Start()
    {
        // Main Menu ખુલ્લું હોય ત્યારે ગેમ Pause રહે (ઓબ્સ્ટેકલ્સ ન બને)
        Time.timeScale = 0f;
    }

    // 1. PLAY BUTTON
    public void OnClickPlay()
    {
        PlaySound();
        Time.timeScale = 1f;          // ગેમ ચાલુ થશે
        gameObject.SetActive(false);  // Main Menu Hide થશે
    }

    // 2. SCORES BUTTON
    public void OnClickScores()
    {
        PlaySound();
        if (scoresPanel != null) scoresPanel.SetActive(true);
    }

    // 3. SHOP BUTTON
    public void OnClickShop()
    {
        PlaySound();
        if (shopPanel != null) shopPanel.SetActive(true);
    }

    // 4. HOW TO PLAY BUTTON
    public void OnClickHowToPlay()
    {
        PlaySound();
        if (howToPlayPanel != null) howToPlayPanel.SetActive(true);
    }

    // 5. SETTINGS BUTTON
    public void OnClickSettings()
    {
        PlaySound();
        if (settingsPanel != null) settingsPanel.SetActive(true);
    }

    // Sub-Panels (Shop, Settings વગેરે) બંધ કરવા માટે Close Button પર આ વાપરવું
    public void ClosePanel(GameObject panel)
    {
        PlaySound();
        if (panel != null) panel.SetActive(false);
    }

    private void PlaySound()
    {
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlayButtonClick();
        }
    }
}