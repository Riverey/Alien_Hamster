using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviour {
    public static int score = 0;
    public static Text scoreText;
    public Text _scoreText;

    public GameObject[] scorePoints;

    public GameObject winScreen;
    public Text scoreWinResult;
    public GameObject[] stars = new GameObject[3];
    private bool canWin = true;

    public enum GameState { Active, Paused, Over, Win }
    public static GameState currentGameState = GameState.Active;

    

    //public GameObject endScreen;

    private void Start()
    {
        scoreText = _scoreText;
        scorePoints = GameObject.FindGameObjectsWithTag("ScorePoint");
        Debug.Log("Found " + scorePoints.Length + " points");
    }

    private void Update()
    {
        if (currentGameState == GameState.Active)
            Time.timeScale = 1.0f;
        else if (currentGameState == GameState.Paused)
            Time.timeScale = 0.0f;
        else if (currentGameState == GameState.Over)
            Time.timeScale = 0.0f;
        else if (currentGameState == GameState.Win)
        {
            if (canWin)
            {
                Time.timeScale = 0.0f;
                Win();
                canWin = false;
            }
        }
    }

    public void Win ()
    {
        int raiting = 0;
        winScreen.SetActive(true);
        Debug.Log("Win, score points are " + scorePoints.Length);
        if (scorePoints.Length == score)
        {
            stars[0].SetActive(true);
            stars[1].SetActive(true);
            stars[2].SetActive(true);
            raiting = 3;
            Debug.Log("Giving 3 stars");
        }
        else if (score > (scorePoints.Length * 0.5f))
        {
            stars[0].SetActive(true);
            stars[1].SetActive(true);
            raiting = 2;
            Debug.Log("Giving 2 stars");

        }
        else if (score > (scorePoints.Length * 0.3f))
        {
            stars[0].SetActive(true);
            Debug.Log("Giving 1 star");
            raiting = 1;
        }
        else
            raiting = 0;

        scoreWinResult.text = "FINAL SCORE: " + score + " / " + scorePoints.Length;
        
        if (PlayerPrefs.GetInt("Lvl_" + SceneManager.GetActiveScene().buildIndex + "raiting") < raiting)
        {
            PlayerPrefs.SetInt("Lvl_" + SceneManager.GetActiveScene().buildIndex + "raiting", raiting);
        }
        PlayerPrefs.SetInt("Lvl_" + (SceneManager.GetActiveScene().buildIndex + 1) + "isUnlckd", 1);
    }

    public static void AddScore ()
    {
        score = score + 1;
        scoreText.text = score.ToString();
    }

    public static void GameOver()
    {
        currentGameState = GameState.Over;
    }

    public void LoadMenu()
    {
        currentGameState = GameState.Active;
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(0);
    }
    
    public void Reload()
    {
        currentGameState = GameState.Active;
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
