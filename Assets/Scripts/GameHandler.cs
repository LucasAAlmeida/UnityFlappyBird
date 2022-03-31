using UnityEngine;
using UnityEngine.SceneManagement;

public class GameHandler : MonoBehaviour
{
    public static GameHandler Instance { get; private set; }
    private int score;
    private int highScore;

    public bool IsGameOver { get; private set; }

    private void Awake()
    {
        Instance = this;
        IsGameOver = false;
        Time.timeScale = 1;

        score = 0;
        highScore = PlayerPrefs.GetInt("highscore");
    }

    private void Start()
    {
        StartCoroutine(Obstacle.CreateRoutine());
        GameWindow.Instance.UpdateHighScore(highScore);
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void GameOver()
    {
        IsGameOver = true;
        Time.timeScale = 0;
        AssetHandler.Instance.gameOverWindow.SetActive(true);
        if (score > highScore) {
            PlayerPrefs.SetInt("highscore", score);
            GameWindow.Instance.UpdateHighScore(score);
        }
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void AddToScore(int scoreToAdd)
    {
        score += scoreToAdd;
        GameWindow.Instance.UpdateScore(score);
    }
}
