using UnityEngine;

public class TiltControl : MonoBehaviour
{
    ///<summary>
    ///A final tilt value that is used by other scripts
    ///</summary>
    [HideInInspector]
    public static Vector3 globalTilt;

    private Matrix4x4 calibrationMatrix;

    ///<summary>
    ///Vector used for storing the inital rotation of the phone
    ///</summary>
    private Vector3 zeroTilt = Vector3.zero;

    [HideInInspector]
    public static Vector3 virtualTilt = Vector3.zero;
    
    [Range(0, 1)]
    public float tiltPower = 1.0f;

    [Range(0, 1)]
    public float virtualTiltPower = 1.0f;

    private static bool phoneConnected = false; //determines the control methode for the character

    void Start () {
        calibrateAccelerometer();
        DetectPhone();
    }    

    private void DetectPhone ()
    {
        if (Input.acceleration != Vector3.zero)
        {
            phoneConnected = true;
            Debug.Log("Phone connected, input vector was " + Input.acceleration);
        }
        else
            Debug.LogWarning("No phone detected!");
    }

    //Method for calibration 
    public void calibrateAccelerometer()
    {
        zeroTilt = Input.acceleration;
        if (DebugManager.debugEnabled) Debug.Log("Calibrated, new dead zone is " + zeroTilt);
        Quaternion rotateQuaternion = Quaternion.FromToRotation(new Vector3(0f, 0f, -1f), zeroTilt); //create identity matrix and rotate the matrix to match up with down vec
        Matrix4x4 matrix = Matrix4x4.TRS(Vector3.zero, rotateQuaternion, new Vector3(1f, 1f, 1f)); //get the inverse of the matrix
        calibrationMatrix = matrix.inverse;
    }

    //Method to get the calibrated input 
    Vector3 getAccelerometer(Vector3 accelerator)
    {
        Vector3 accel = this.calibrationMatrix.MultiplyVector(accelerator);
        return accel;
    }

    void Update()
    {
        if (phoneConnected)
            globalTilt = Quaternion.Euler(90, 0, 0) * getAccelerometer(Input.acceleration);

        else
        {
            if (Input.GetKey("a"))
                virtualTilt.x = Mathf.Clamp(virtualTilt.x - virtualTiltPower * Time.deltaTime, -1, 0);
            if (Input.GetKey("d"))
                virtualTilt.x = Mathf.Clamp(virtualTilt.x + virtualTiltPower * Time.deltaTime, 0, 1);
            if (!Input.GetKey("a") && !Input.GetKey("d"))
                virtualTilt.x = Mathf.Lerp(virtualTilt.x, 0, virtualTiltPower * Time.deltaTime * 10); // slow down
            if (Input.GetKey("w"))
                virtualTilt.y = Mathf.Clamp(virtualTilt.y + virtualTiltPower * Time.deltaTime, 0, 1);
            if (Input.GetKey("s"))
                virtualTilt.y = Mathf.Clamp(virtualTilt.y - virtualTiltPower * Time.deltaTime, -1, 0);
            if (!Input.GetKey("w") && !Input.GetKey("s"))
                virtualTilt.y = Mathf.Lerp(virtualTilt.y, 0, virtualTiltPower * Time.deltaTime * 10); // slow down

            virtualTilt.z = 0.0f; //zero the y axis

            globalTilt = Quaternion.Euler(90, 0, 0) * virtualTilt;
        }
    }
}
