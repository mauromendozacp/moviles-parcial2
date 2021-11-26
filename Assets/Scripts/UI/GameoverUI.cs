using TMPro;
using UnityEngine;

public class GameoverUI : MonoBehaviour
{
    #region EXPOSED_FIELDS

    [SerializeField] private TMP_Text scoreText = null;

    #endregion

    #region UNITY_CALLS

    void Start()
    {
        int score = GameManager.Get().RecollectedStars;
        scoreText.text = "STARS: " + score;
        Debug.Log("End Gameplay");
        UnlockAcvhievements();
        PostScore();
    }

    #endregion

    #region PUBLIC_METHODS

    public void BackToMenu()
    {
        GameManager.Get().ChangeScene(GameManager.SceneGame.MainMenu);
    }

    #endregion

    #region PRIVATE_METHODS

    private void UnlockAcvhievements()
    {
        if (GameManager.Get().RecollectedStars == 0)
        {
            PlayGames.Get().UnlockAchievement(0);
        }
    }

    private void PostScore()
    {
        if (GameManager.Get().RecollectedStars > 0)
        {
            PlayGames.Get().UpdateLeadboard(GameManager.Get().RecollectedStars);
        }
    }

    #endregion
}
