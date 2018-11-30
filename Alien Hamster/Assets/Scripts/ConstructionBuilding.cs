using UnityEngine;

public class ConstructionBuilding : MonoBehaviour {
    public GameObject destroyEffect;

    public void DestroyTheBuilding ()
    {
        Instantiate(destroyEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
