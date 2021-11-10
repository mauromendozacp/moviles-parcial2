using System.Collections;
using System.Collections.Generic;
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
        int score = GameManager.Get().Score;
        scoreText.text = "SCORE: " + score + "pts";
    }

    #endregion

    #region PUBLIC_METHODS

    public void BackToMenu()
    {
        GameManager.Get().ChangeScene(GameManager.SceneGame.MainMenu);
    }

    #endregion
}
