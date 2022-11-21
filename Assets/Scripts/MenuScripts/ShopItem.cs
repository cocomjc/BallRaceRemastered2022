using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour
{
    [SerializeField] private Sprite lockedSprite;
    private SkinComponent skinPrefab;
    private bool locked = true;
    private SkinManager skinManager;

    private void Awake()
    {
        skinManager = GameObject.FindGameObjectWithTag("Player").GetComponent<SkinManager>();
        GetComponent<Image>().sprite = lockedSprite;
    }

    public void SetSkin(GameObject _skinPrefab)
    {
        skinPrefab = _skinPrefab.GetComponent<SkinComponent>();
    }
    public SkinComponent GetSkin()
    {
        return skinPrefab;
    }
    
    public void Unlock()
    {
        GetComponent<Image>().sprite = skinPrefab.GetSkinSprite();
        locked = false;
    }

    public void Equip()
    {
        if (!locked)
        {
            skinManager.SetSkin(skinPrefab.GetId());
        }
    }
}
