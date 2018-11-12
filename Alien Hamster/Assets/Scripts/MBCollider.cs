using UnityEngine;

public class MBCollider : MonoBehaviour {
    public bool isColliding = false;

    private void OnCollisionEnter(Collision collision)
    {
        isColliding = true;
    }

}
