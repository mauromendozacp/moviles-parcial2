using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Store : MonoBehaviour
{
    #region EXPOSED_FIELDS

    [SerializeField] private Skin[] skins = null;
    [SerializeField] private Image skinImage = null;
    [SerializeField] private Button equipButtonGO = null;
    [SerializeField] private Button buyButtonGO = null;

    #endregion

    #region PRIVATE_FIELDS

    private int skinIndex = 0;
    private bool[] skinsEquiped = null;
    private bool[] skinsBuyed = null;

    #endregion

    #region UNITY_CALLS

    private void Start()
    {
        skinsEquiped = new bool[skins.Length];
        skinsBuyed = new bool[skins.Length];
        LoadData();
    }

    #endregion

    #region PUBLIC_METHODS

    public void ChangeSkin(bool next)
    {
        skinIndex = next ? skinIndex + 1 : skinIndex - 1;

        if (skinIndex < 0)
            skinIndex = skins.Length - 1;
        if (skinIndex > skins.Length - 1)
            skinIndex = 0;

        skinImage.sprite = skins[skinIndex].SkinSprite;

        equipButtonGO.interactable = skinsEquiped[skinIndex];
        equipButtonGO.gameObject.SetActive(skinsBuyed[skinIndex]);
        buyButtonGO.interactable = CheckBuy();
        buyButtonGO.gameObject.SetActive(!skinsBuyed[skinIndex]);
    }

    public void EquipSkin()
    {
        GameManager.Get().Skin = skins[skinIndex];
        equipButtonGO.interactable = false;
    }

    public void BuySkin()
    {
        if (CheckBuy())
        {
            GameManager.Get().Score -= skins[skinIndex].Stars;
            equipButtonGO.gameObject.SetActive(true);
            buyButtonGO.gameObject.SetActive(false);
        }
    }

    #endregion

    #region PRIVATE_METHODS

    private void LoadData()
    {

    }

    private bool CheckBuy()
    {
        return GameManager.Get().Score >= skins[skinIndex].Stars;
    }

    #endregion
}
