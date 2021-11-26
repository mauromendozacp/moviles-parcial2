using UnityEngine;

public class MainmenuUI : MonoBehaviour
{
    #region EXPOSED_FIELDS

    [SerializeField] private GameObject menuPanel = null;
    [SerializeField] private GameObject storePanel = null;
    [SerializeField] private GameObject creditsPanel = null;
    [SerializeField] private LogsUI logs = null;

    #endregion

    #region UNITY_CALLS

    private void Start()
    {
        MLogger.SendLog("Start game");
        Application.logMessageReceived += HandleLog;
        Debug.Log("Start Menu");
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
        logs.gameObject.SetActive(false);
    }

    public void ShowStore()
    {
        menuPanel.SetActive(false);
        storePanel.SetActive(true);

        Debug.Log("Show store");
    }

    public void ShowCredits()
    {
        menuPanel.SetActive(false);
        creditsPanel.SetActive(true);

        Debug.Log("Show Credits");
    }

    public void ShowLogs()
    {
        menuPanel.SetActive(false);
        logs.gameObject.SetActive(true);
        logs.UpdateLogs();

        Debug.Log("Show Logs");
    }

    public void ExitGame()
    {
        MLogger.SendLog("End game");
        Application.Quit();
    }

    #endregion

    #region PRIVATE_METHODS

    void HandleLog(string logString, string stackTrace, LogType type)
    {
        FileManager.WriteFile(logString + "\n");
    }

    #endregion
}