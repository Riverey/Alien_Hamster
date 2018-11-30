using UnityEngine;

public class GameOver : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Player")
        {
            GameManager.currentGameState = GameManager.GameState.Win;
            Debug.Log("Player entered the trigger");
        }
    }
}
