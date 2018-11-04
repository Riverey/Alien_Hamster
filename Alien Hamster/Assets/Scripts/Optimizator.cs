using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Optimizator : MonoBehaviour {

    private Material material;
    private int objectsInCollision = 0;
    private int targetState = 0;

    private void Start()
    {
        material = gameObject.GetComponent<Renderer>().material;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "MovableBuilding")
        {
            other.gameObject.GetComponent<MovableBlock>().isActive = true;
            objectsInCollision++;
            ChangeColor();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "MovableBuilding")
        {
            other.gameObject.GetComponent<MovableBlock>().isActive = false;
            objectsInCollision = Mathf.Clamp(objectsInCollision - 1, 0, objectsInCollision);
            ChangeColor();
        }
    }

    private void ChangeColor()
    {
        if (objectsInCollision == 0)
        {
            targetState = 0;
        }
        else
        {
            targetState = 1;
        }
    }

    private void Update()
    {
        material.SetFloat("Vector1_4ECB9F43", Mathf.Lerp(material.GetFloat("Vector1_4ECB9F43"), targetState, 0.1f));
    }
}
