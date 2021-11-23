using UnityEngine;

public class MainmenuUI : MonoBehaviour
{
    #region EXPOSED_FIELDS

    [SerializeField] private GameObject menuPanel = null;
    [SerializeField] private GameObject storePanel = null;
    [SerializeField] private GameObject creditsPanel = null;
    [SerializeField] private GameObject logsPanel = null;

    #endregion

    #region UNITY_CALLS

    private void Start()
    {
        MLogger.SendLog("Start game");
    }

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
        logsPanel.SetActive(false);
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

    public void ShowLogs()
    {
        menuPanel.SetActive(false);
        logsPanel.SetActive(true);
    }

    public void ExitGame()
    {
        MLogger.SendLog("End game");
        Application.Quit();
    }

    #endregion
}