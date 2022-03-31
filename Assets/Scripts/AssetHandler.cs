using UnityEngine;
using TMPro;

public class AssetHandler : MonoBehaviour
{
    public static AssetHandler Instance { get; private set; }

    public void Awake()
    {
        if (Instance == null) {
            Instance = this;
        }
    }

    public GameObject obstaclePrefab;
    public GameObject gameOverWindow;
    public TextMeshProUGUI ScoreText;
    public TextMeshProUGUI HighScoreText;
}
