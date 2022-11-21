using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinComponent : MonoBehaviour
{
    [SerializeField] private Sprite skinSprite;
    [SerializeField] private string id;

    public Sprite GetSkinSprite()
    {
        return skinSprite;
    }
    
    public string GetId()
    {
        return id;
    }
}
