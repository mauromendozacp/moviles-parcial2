using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainmenuUI : MonoBehaviour
{
    #region EXPOSED_FIELDS

    [SerializeField] private GameObject menuPanel = null;
    [SerializeField] private GameObject storePanel = null;
    [SerializeField] private GameObject creditsPanel = null;

    #endregion

    #region PUBLIC_METHODS

    public void PlayGame()
    {
        GameManager.Get().ChangeScene(GameManager.SceneGame.GamePlay);
    }

    public void ShowMenu()
    {
        menuPanel.SetActive(true);
        creditsPanel.SetActive(false);
        storePanel.SetActive(false);
    }

    public void ShowStore()
    {
        menuPanel.SetActive(false);
        storePanel.SetActive(true);
    }

    public void ShowCredits()
    {
        menuPanel.SetActive(false);
        creditsPanel.SetActive(true);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    #endregion
}