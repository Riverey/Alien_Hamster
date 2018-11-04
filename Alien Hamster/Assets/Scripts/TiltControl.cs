using UnityEngine;

public class TiltControl : MonoBehaviour
{
    ///<summary>
    ///A final tilt value that is used by other scripts
    ///</summary>
    [HideInInspector]
    public Vector3 globalTilt;

    Matrix4x4 calibrationMatrix;
    Vector3 zeroTilt = Vector3.zero;
    [HideInInspector]
    public Vector3 virtualTilt = Vector3.zero;
    
    [Range(0, 1)]
    public float tiltPower = 1.0f;

    [Range(0, 1)]
    public float virtualTiltPower = 1.0f;

    public bool phoneConnected = false;

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

    public void changeMode (bool mode)
    {
        phoneConnected = mode;
    }


    //Method for calibration 
    public void calibrateAccelerometer()
    {
        zeroTilt = Input.acceleration;
        Debug.Log("Calibrated, new dead zone is " + zeroTilt);
        Quaternion rotateQuaternion = Quaternion.FromToRotation(new Vector3(0f, 0f, -1f), zeroTilt);
        //create identity matrix ... rotate our matrix to match up with down vec
        Matrix4x4 matrix = Matrix4x4.TRS(Vector3.zero, rotateQuaternion, new Vector3(1f, 1f, 1f));
        //get the inverse of the matrix
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
            globalTilt = getAccelerometer(Input.acceleration);  
        else
        {
            if (Input.GetKey("a"))
            {
                virtualTilt.x = Mathf.Clamp(virtualTilt.x - virtualTiltPower * Time.deltaTime, -1, 0);
            }
            if (Input.GetKey("d"))
            {
                virtualTilt.x = Mathf.Clamp(virtualTilt.x + virtualTiltPower * Time.deltaTime, 0, 1);
            }
            if (!Input.GetKey("a") && !Input.GetKey("d"))
            {
                virtualTilt.x = Mathf.Lerp(virtualTilt.x, 0, virtualTiltPower * Time.deltaTime * 10);
            }

            if (Input.GetKey("w"))
            {
                virtualTilt.y = Mathf.Clamp(virtualTilt.y + virtualTiltPower * Time.deltaTime, 0, 1);
            }
            if (Input.GetKey("s"))
            {
                virtualTilt.y = Mathf.Clamp(virtualTilt.y - virtualTiltPower * Time.deltaTime, -1, 0);
            }
            if (!Input.GetKey("w") && !Input.GetKey("s"))
                virtualTilt.y = Mathf.Lerp(virtualTilt.y, 0, virtualTiltPower * Time.deltaTime * 10);

            virtualTilt.z = 0.0f;
            globalTilt = virtualTilt;
        }
    }
}
