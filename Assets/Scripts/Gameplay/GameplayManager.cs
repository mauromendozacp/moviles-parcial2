using System;
using UnityEngine;

public class GMActions
{
    public Action OnPlayerDeath = null;
}

public class GameplayManager : MonoBehaviour
{
    #region EXPOSED_FIELDS

    [SerializeField] private Player player = null;
    [SerializeField] private HUD hud = null;
    [SerializeField] private FloorLoop floorLoop = null;
    [SerializeField] private DIFFICULTY difficulty = default;
    [SerializeField] private Skin defaultSkin = null;

    #endregion

    #region PRIVATE_FIELDS

    private GMActions gmActions = null;

    #endregion

    #region UNITY_CALLS

    void Start()
    {
        GameManager.Get().Init();
        Init();
    }

    #endregion

    #region PRIVATE_METHODS

    private void Init()
    {
        gmActions = new GMActions();
        gmActions.OnPlayerDeath += EndLevel;

        Skin skin = GameManager.Get().Skin;
        if (skin == null)
        {
            skin = defaultSkin;
            GameManager.Get().Skin = skin;
        }

        player.Init(gmActions, skin);
        hud.Init(player.PActions);

        player.Score = GameManager.Get().Score;
        floorLoop.LevelIndex = (int) difficulty;

        SendLog();
    }

    private void EndLevel()
    {
        GameManager.Get().FinishGame(player.Score);
    }

    private void SendLog()
    {
        MLogger.SendLog("Start game");
    }

    #endregion
}
