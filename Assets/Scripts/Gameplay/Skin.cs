using UnityEngine;

[CreateAssetMenu(fileName = "PlayerSkin_", menuName = "Skin", order = 1)]
public class Skin : ScriptableObject
{
    #region EXPOSED_FIELDS

    [SerializeField] private GameObject ballPrefab = null;
    [SerializeField] private Sprite skinSprite = null;
    [SerializeField] private int stars = 0;

    #endregion

    #region PROPERTIES

    public GameObject BallPrefab => ballPrefab;
    public Sprite SkinSprite => skinSprite;
    public int Stars => stars;

    #endregion
}
