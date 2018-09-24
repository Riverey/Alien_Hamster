using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovableBlock : MonoBehaviour {

    public enum MovingDirection { UpDown, LeftRight, BothDirections};
    public MovingDirection movingDirection;

    [Range(1,4)]
    public float speed = 1.0f;

    private Rigidbody rigid;

    private void Start()
    {
        rigid = GetComponent<Rigidbody>();
        if (movingDirection == MovingDirection.UpDown)
            rigid.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezeRotation;
        else if (movingDirection == MovingDirection.LeftRight)
            rigid.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;
        else if (movingDirection == MovingDirection.BothDirections)
            rigid.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation;
    }

    // Update is called once per frame
    void Update () {
        Vector3 tilt = Input.acceleration * speed;
        tilt = Quaternion.Euler(90, 0, 0) * tilt;

        if (movingDirection == MovingDirection.UpDown)
            rigid.AddForce(new Vector3 (0,0,tilt.z));
        else if (movingDirection == MovingDirection.LeftRight)
            rigid.AddForce(new Vector3(tilt.x,0,0));
        else if (movingDirection == MovingDirection.BothDirections)
            rigid.AddForce(new Vector3(tilt.x,0,tilt.z));

        Debug.DrawRay(transform.position + Vector3.up, tilt, Color.cyan);         
    }

    void OnDrawGizmos()
    {
        if (movingDirection == MovingDirection.UpDown)
            Gizmos.DrawIcon(transform.position, "MB_UpDown.png", true);
        else if (movingDirection == MovingDirection.LeftRight)
            Gizmos.DrawIcon(transform.position, "MB_LeftRight.png", true);
        else if (movingDirection == MovingDirection.BothDirections)
            Gizmos.DrawIcon(transform.position, "MB_BothDirections.png", true);
    }
}
