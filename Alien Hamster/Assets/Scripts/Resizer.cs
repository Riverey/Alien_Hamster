using UnityEngine;

[ExecuteInEditMode]
public class Resizer : MonoBehaviour
{
    [Range(0.1f, 10f)]
    public float radius = 1;
    private Vector3 oldScale;

    void Update()
    {
        if (gameObject.transform.localScale != oldScale)
            gameObject.transform.localScale = new Vector3(radius, gameObject.transform.localScale.y, radius);
    }
}
