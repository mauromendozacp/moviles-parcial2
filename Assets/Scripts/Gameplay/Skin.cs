using UnityEngine;

[CreateAssetMenu(fileName = "PlayerSkin_", menuName = "Skin", order = 1)]
public class Skin : ScriptableObject
{
    #region EXPOSED_FIELDS

    [SerializeField] private GameObject ballPrefab = null;

    #endregion

    #region PROPERTIES

    public GameObject BallPrefab => ballPrefab;

    #endregion
}
