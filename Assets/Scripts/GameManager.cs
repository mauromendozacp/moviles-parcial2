using System;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviourSingleton<GameManager>
{
    public Skin Skin { get; set; } = null;
    public PlayerStore PlayerStore { get; set; } = null;
    public int CurrentStars { get; set; } = 0;
    public int RecollectedStars { get; set; } = 0;
    public bool GameOver { set; get; } = false;

    public enum SceneGame
    {
        MainMenu,
        GamePlay,
        GameOver
    }

    public void ChangeScene(SceneGame scene)
    {
        string sceneName;

        switch (scene)
        {
            case SceneGame.MainMenu:
                sceneName = "Mainmenu";
                break;
            case SceneGame.GamePlay:
                sceneName = "Gameplay";
                break;
            case SceneGame.GameOver:
                sceneName = "Gameover";
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(scene), scene, null);
        }

        SceneManager.LoadScene(sceneName);
    }

    public void Init()
    {
        GameOver = false;
    }

    public void FinishGame(int score)
    {
        RecollectedStars = score;
        CurrentStars += RecollectedStars;
        GameOver = true;
        ChangeScene(SceneGame.GameOver);
    }
}