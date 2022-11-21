using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinComponent : MonoBehaviour
{
    [SerializeField] private Sprite skinSprite;

    public Sprite GetSkinSprite()
    {
        return skinSprite;
    }
}
