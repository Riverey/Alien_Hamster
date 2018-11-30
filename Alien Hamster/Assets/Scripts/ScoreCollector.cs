using UnityEngine;

public class ScoreCollector : MonoBehaviour {

    public AudioSource coinPickup;

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "ScorePoint")
        {
            GameManager.AddScore();
            Destroy(collider.gameObject);
            coinPickup.Play();
        }
    }
}
