using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {
    public static int score = 0;
    public static Text scoreText;

    public static void AddScore ()
    {
        score++;
        scoreText.text = score.ToString();
    }
    
}
