using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public bool isFlat = true;
    [Range(2,4)]
    public float speed = 1.0f;
    private Rigidbody rigid;

	// Use this for initialization
	void Start () {
        
        rigid = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 tilt = Input.acceleration * speed;
        if (isFlat)
            tilt = Quaternion.Euler(90, 0, 0) * tilt;
        rigid.AddForce(tilt);
        Debug.DrawRay(transform.position + Vector3.up, tilt, Color.red);
	}
}
