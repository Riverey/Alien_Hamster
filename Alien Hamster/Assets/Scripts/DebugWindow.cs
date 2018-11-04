using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugWindow : MonoBehaviour {
    public bool debugEnabled = false;
    public Text virtualTiltText;
    public Text inputTiltText;

    public TiltControl tiltControl;
	
	// Update is called once per frame
	void Update () {
        virtualTiltText.text = "<b>Virtual input is: </b>" + tiltControl.virtualTilt;
        inputTiltText.text = "<b>Acceletarion: </b>" + Input.acceleration;
    }
}
