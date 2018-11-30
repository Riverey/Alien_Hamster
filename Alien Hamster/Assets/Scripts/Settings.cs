using UnityEngine;
using UnityEngine.UI;


public class Settings : MonoBehaviour {

    public Text sensitivitySettings;
    public GameObject settingsWindow;
    public TiltControl tiltControl;

    public MenuUI menuUI;

    public GameObject tutorialScreen;

    public Button wasd;
    public Button phone;
    public Button restart;
    public Button tutorial;

    public GameObject[] tutorialSlides = new GameObject[6];

    public bool startWithTutorial = false;

    private void Start()
    {
        tiltControl = FindObjectOfType<TiltControl>();

        if (startWithTutorial) PlayerPrefs.SetInt("FinishedTutorial", 0);

        if (PlayerPrefs.GetInt("FinishedTutorial") == 0)
        {
            tutorialScreen.SetActive(true);
            GameManager.currentGameState = GameManager.GameState.Paused;
            restart.interactable = false;
            tutorial.interactable = false;
            gameObject.GetComponent<Button>().interactable = false;
        }
    }

    public void switchMode ( bool mode)
    {
        TiltControl.phoneConnected = mode;
    }
    
    public void ToggleSettingWindow ()
    {
        settingsWindow.SetActive(!settingsWindow.activeSelf);
        if (settingsWindow.activeSelf)
            GameManager.currentGameState = GameManager.GameState.Paused;
        else
            GameManager.currentGameState = GameManager.GameState.Active;
    }

    public void UpdateSensitivityValue(Slider slider)
    {
        sensitivitySettings.text = "SENSITIVITY: " + slider.value.ToString("#");
        tiltControl.virtualTiltPower = slider.value.Remap(1, 10, 0, 1);
    }

    public void closeTutorial ()
    {
        tutorialScreen.SetActive(false);
        GameManager.currentGameState = GameManager.GameState.Active;
        PlayerPrefs.SetInt("FinishedTutorial", 1);
    }

    public void openTutorial ()
    {
        tutorialScreen.SetActive(true);
        GameManager.currentGameState = GameManager.GameState.Paused;
        int count = 0;
        foreach (GameObject slide in tutorialSlides)
        {
            if (count == 0) { slide.transform.position = menuUI.activeScreen.position; slide.SetActive(true); }
            else { slide.transform.position = menuUI.rightScreen.position; slide.SetActive(false); }
            count++;
            menuUI.isMoving = false;
        }
    }
}

public static class ExtensionMethods
{
    public static float Remap(this float value, float from1, float to1, float from2, float to2)
    {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }
}
