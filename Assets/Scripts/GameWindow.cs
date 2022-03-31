using UnityEngine;

public class GameWindow : MonoBehaviour
{
    public static GameWindow Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void UpdateScore(int score)
    {
        AssetHandler.Instance.ScoreText.text = "Score: " + score;
    }

    public void UpdateHighScore(int highScore)
    {
        AssetHandler.Instance.HighScoreText.text = "High Score: " + highScore;
    }
}
