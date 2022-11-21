using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject skinDisplay;
    [SerializeField] private GameObject shopItemPrefab;

    private void Start()
    {
        GameObject[] skinPrefabs = Resources.LoadAll<GameObject>("Skins");

        Debug.Log("Found " + skinPrefabs.Length + " skins");

        foreach (GameObject skinPrefab in skinPrefabs)
        {
            GameObject skin = Instantiate(shopItemPrefab, skinDisplay.transform);
            skin.GetComponent<ShopItem>().SetSkin(skinPrefab);
            skin.GetComponent<ShopItem>().Unlock();
        }
    }

}
