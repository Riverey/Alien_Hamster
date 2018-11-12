using UnityEngine;

public class ScoreCollector : MonoBehaviour {

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "ScorePoint")
        {
            ScoreManager.AddScore();
            Destroy(collision.gameObject);
        }
    }
}
