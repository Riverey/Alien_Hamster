using UnityEngine;
using UnityEngine.UI;

public class DebugManager : MonoBehaviour {
    public static bool debugEnabled = false;
    public bool _debugEnabled = false;
    public Text virtualTiltText;
    public Text inputTiltText;

    private void Start()
    {
        debugEnabled = _debugEnabled;
    }

    // Update is called once per frame
    void Update () {
        virtualTiltText.text = "<b>Virtual input is: </b>" + TiltControl.virtualTilt;
        inputTiltText.text = "<b>Acceletarion: </b>" + Input.acceleration;
    }
}
