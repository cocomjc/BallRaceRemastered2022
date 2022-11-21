using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour
{
    [SerializeField] private Sprite lockedSprite;
    [SerializeField] private Image previewImage;
    private SkinComponent skinPrefab;
    private bool locked = true;
    private SkinManager skinManager;

    private void Awake()
    {
        skinManager = GameObject.FindGameObjectWithTag("Player").GetComponent<SkinManager>();
        previewImage.sprite = lockedSprite;
    }

    private void Update()
    {
        if (skinManager.GetSkin() == skinPrefab.GetId())
        {
            GetComponent<Outline>().enabled = true;
        }
        else
        {
            GetComponent<Outline>().enabled = false;
        }
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
        previewImage.sprite = skinPrefab.GetSkinSprite();
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
