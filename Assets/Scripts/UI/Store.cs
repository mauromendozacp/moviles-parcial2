﻿using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStore
{
    public int skinEquiped = 0;
    public bool[] skinsBuyed = null;
}

public class Store : MonoBehaviour
{
    #region EXPOSED_FIELDS

    [SerializeField] private Skin[] skins = null;
    [SerializeField] private Image skinImage = null;
    [SerializeField] private TMP_Text costText = null;
    [SerializeField] private TMP_Text playerStarsText = null;
    [SerializeField] private Button equipButtonGO = null;
    [SerializeField] private Button buyButtonGO = null;

    #endregion

    #region PRIVATE_FIELDS

    private int skinIndex = 0;
    private PlayerStore playerStore;

    #endregion

    #region UNITY_CALLS

    private void Start()
    {
        InitPlayerStore();
        UpdatePlayerStars();
        UpdateInfo();
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

        UpdateInfo();
    }

    public void EquipSkin()
    {
        GameManager.Get().Skin = skins[skinIndex];
        equipButtonGO.interactable = false;
        playerStore.skinEquiped = skinIndex;
    }

    public void BuySkin()
    {
        if (CheckBuy())
        {
            GameManager.Get().CurrentStars -= skins[skinIndex].Stars;
            playerStore.skinsBuyed[skinIndex] = true;
            equipButtonGO.gameObject.SetActive(true);
            buyButtonGO.gameObject.SetActive(false);
            UpdatePlayerStars();
        }
    }

    #endregion

    #region PRIVATE_METHODS

    private void InitPlayerStore()
    {
        if (GameManager.Get().PlayerStore == null)
        {
            playerStore = new PlayerStore();
            playerStore.skinEquiped = 0;
            playerStore.skinsBuyed = new bool[skins.Length];

            playerStore.skinsBuyed[0] = true;
            for (int i = 1; i < playerStore.skinsBuyed.Length; i++)
            {
                playerStore.skinsBuyed[i] = false;
            }

            GameManager.Get().PlayerStore = playerStore;
        }
        else
        {
            playerStore = GameManager.Get().PlayerStore;
        }
    }

    private void UpdateInfo()
    {
        skinImage.sprite = skins[skinIndex].SkinSprite;
        costText.text = skins[skinIndex].Stars.ToString();
        equipButtonGO.interactable = skinIndex != playerStore.skinEquiped;
        equipButtonGO.gameObject.SetActive(playerStore.skinsBuyed[skinIndex]);
        buyButtonGO.interactable = CheckBuy();
        buyButtonGO.gameObject.SetActive(!playerStore.skinsBuyed[skinIndex]);
    }

    private void UpdatePlayerStars()
    {
        playerStarsText.text = GameManager.Get().CurrentStars.ToString();
    }

    private void LoadData()
    {

    }

    private bool CheckBuy()
    {
        return GameManager.Get().CurrentStars >= skins[skinIndex].Stars;
    }

    #endregion
}
