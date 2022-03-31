using UnityEngine;

public class GameOverWindow : MonoBehaviour
{
    public static GameOverWindow Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void OnRetryButtonClicked()
    {
        GameHandler.Instance.ReloadScene();
    }

    public void OnMainMenuButtonClicked()
    {
        GameHandler.Instance.LoadMainMenu();
    }
}
