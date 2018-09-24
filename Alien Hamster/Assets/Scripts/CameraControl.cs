﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour {
    /// <summary>
    /// Target of the camera, i.e. player
    /// </summary>
    [Tooltip("Target of the camera, i.e. player")]
    public Transform target;
    /// <summary>
    /// Speed of the camera smoothing
    /// </summary>
    [Tooltip("Speed of the camera smoothing")]
    [Range(0, 1)]
    public float smoothSpeed = 0.1f;
    /// <summary>
    /// Vertical offset of the camera
    /// </summary>
    [Tooltip("Vertical offset of the camera")]
    [Range(0,20)]
    public int offset = 16;
    /// <summary>
    /// Offset used to put the light at an angle and produce shadows
    /// </summary>
    [Tooltip("Offset used to put the light at an angle and produce shadows")]
    [Range(0,20)]
    public int rotationOffset = 0;
    /// <summary>
    /// Offset used for the tilt camera rotation
    /// </summary>
    private Vector3 gravityOffset;
    /// <summary>
    /// How much to rotate the camera with the phone tilt
    /// </summary>
    [Tooltip("Offset used to put the light at an angle and produce shadows")]
    [Range(0.0f, 2.0f)]
    public float gravityOffsetMultyplier = 1.0f;

	void LateUpdate () {
        gravityOffset = Quaternion.Euler(90, 0, 0) * Input.acceleration * gravityOffsetMultyplier;
        Vector3 desiredPosition = new Vector3 (target.position.x + gravityOffset.x, target.position.y + offset, target.position.z + gravityOffset.z);
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

        transform.LookAt(new Vector3(target.transform.position.x + rotationOffset, target.transform.position.y, target.transform.position.z + rotationOffset), Vector3.forward);
	}
}