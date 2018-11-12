using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public bool inRange = true;
    [Range(2, 4)]
    public float speed = 1.0f;
    [Range(2, 20)]
    public float rotationSpeed = 1.0f;
    private Rigidbody rigid;

    public GameObject playerModel;
    private Vector3 tilt;
    private Vector3 test;

    // Use this for initialization
    void Start() {
        rigid = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update() {
        tilt = TiltControl.globalTilt * speed;
        if (DebugManager.debugEnabled) Debug.DrawRay(transform.position + Vector3.up, tilt, Color.red);
    }
    private void FixedUpdate()
    {
        rigid.AddForce(tilt);
        rigid.AddForce(rigid.velocity * -0.25f);

        if (DebugManager.debugEnabled) Debug.DrawRay(transform.position, new Vector3(tilt.x * 2, 0, tilt.z * 2), Color.green);

        if (new Vector3 (tilt.x, 0.0f, tilt.z) != Vector3.zero)
        {
            playerModel.transform.rotation = Quaternion.Lerp(playerModel.transform.rotation, Quaternion.LookRotation(new Vector3(tilt.x, 0.1f, tilt.z), Vector3.up), Time.deltaTime * rotationSpeed);
        }
        else if (rigid.velocity != Vector3.zero)
        {
            playerModel.transform.rotation = Quaternion.Lerp(playerModel.transform.rotation, Quaternion.LookRotation(rigid.velocity, Vector3.up), Time.deltaTime * rotationSpeed);
            if (DebugManager.debugEnabled) Debug.DrawRay(transform.position, rigid.velocity * 2, Color.yellow);
        }

    }
}
