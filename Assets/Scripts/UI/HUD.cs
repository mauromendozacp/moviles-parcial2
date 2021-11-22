using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HUD : MonoBehaviour
{
    #region EXPOSED_FIELDS

    [SerializeField] private TMP_Text scoreText = null;

    #endregion

    #region PRIVATE_FIELDS

    private PActions pActions = null;

    #endregion

    #region PUBLIC_METHODS

    public void Init(PActions pActions)
    {
        this.pActions = pActions;

        pActions.OnScore += UpdateScore;
    }

    #endregion

    #region PRIVATE_METHODS

    private void UpdateScore(int score)
    {
        scoreText.text = ": " + score;
    }

    #endregion
}
