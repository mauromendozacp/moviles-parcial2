using UnityEngine;
using TMPro;

public class LogsUI : MonoBehaviour
{
    #region EXPOSED_FIELDS

    [SerializeField] private TMP_Text logsText = null;

    #endregion

    #region PUBLIC_METHODS

    public void UpdateLogs()
    {
        logsText.text = FileManager.Get().ReadFile();
    }

    #endregion
}
