﻿using System;
using System.Collections.Generic;
using UnityEngine;

public class GMActions
{
    public Action OnPlayerDeath = null;
}

public class GameplayManager : MonoBehaviour
{
    #region EXPOSED_FIELDS

    [SerializeField] private Player player = null;

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

    #region PUBLIC_METHODS

    public void SendLog()
    {
        MLogger.SendLog("Anashei");
    }

    #endregion

    #region PRIVATE_METHODS

    private void Init()
    {
        gmActions = new GMActions();
        gmActions.OnPlayerDeath += EndLevel;

        player.Init(gmActions);
    }

    private void EndLevel()
    {
        GameManager.Get().FinishGame(player.Score);
    }

    #endregion
}
