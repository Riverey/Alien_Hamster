using UnityEngine;

public class ParticleDestroy : MonoBehaviour {

    public float lifeTime = 10.0f;
    private float elapsedTime = 0.0f;
	
	// Update is called once per frame
	void Update () {
        if (elapsedTime < lifeTime)
        {
            elapsedTime += Time.deltaTime;
        }
        else
            Destroy(gameObject);
	}
}
