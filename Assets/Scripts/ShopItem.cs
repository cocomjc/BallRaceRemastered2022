using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour
{
    [SerializeField] private Sprite lockedSprite;
    private GameObject skinPrefab;
    private bool locked = true;
    private GameManager gameManager;

    private void Awake()
    {
        gameManager = GameManager.GetInstance();
        GetComponent<Image>().sprite = lockedSprite;
    }

    public void SetSkin(GameObject _skinPrefab)
    {
        skinPrefab = _skinPrefab;
    }

    public void Unlock()
    {
        GetComponent<Image>().sprite = skinPrefab.GetComponent<SkinComponent>().GetSkinSprite();
        locked = false;
    }

    public void Equip()
    {
        if (!locked)
        {
            gameManager.SetSkin(skinPrefab);
        }
    }
}
