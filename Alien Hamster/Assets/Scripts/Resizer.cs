using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Resizer : MonoBehaviour
{
    [Range(0.1f, 10f)]
    public float radius = 1;

    void Update()
    {
        gameObject.transform.localScale = new Vector3(radius, gameObject.transform.localScale.y, radius);
    }
}
