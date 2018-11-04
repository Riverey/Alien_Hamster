using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Settings : MonoBehaviour {

    public Text sensitivitySettings;
    public GameObject settingsWindow;
    public TiltControl tiltControl;

    private void Start()
    {
        tiltControl = FindObjectOfType<TiltControl>();
    }
    
    public void ToggleSettingWindow ()
    {
        settingsWindow.SetActive(!settingsWindow.activeSelf);
        Debug.Log(settingsWindow.isStatic);
    }

    public void UpdateSensitivityValue(Slider slider)
    {
        sensitivitySettings.text = "SENSITIVITY: " + slider.value.ToString("#");
        tiltControl.virtualTiltPower = slider.value.Remap(1, 10, 0, 1);
    }
}

public static class ExtensionMethods
{
    public static float Remap(this float value, float from1, float to1, float from2, float to2)
    {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }
}
