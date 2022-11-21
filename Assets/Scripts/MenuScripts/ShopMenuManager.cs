using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject skinDisplay;
    [SerializeField] private GameObject shopItemPrefab;
    [SerializeField] private TextMeshProUGUI priceText;
    private List<GameObject> shopItems = new List<GameObject>();
    private GameManager gameManager;

    private void Awake()
    {
        gameManager = GameManager.GetInstance();
    }

    private void Start()
    {
        GameObject[] skinPrefabs = Resources.LoadAll<GameObject>("Skins");
        
        foreach (GameObject skinPrefab in skinPrefabs)
        {
            shopItems.Add(Instantiate(shopItemPrefab, skinDisplay.transform));
            shopItems[shopItems.Count - 1].GetComponent<ShopItem>().SetSkin(skinPrefab);
            if (gameManager.IsItemUnlocked(skinPrefab.GetComponent<SkinComponent>().GetId()))
            {
                shopItems[shopItems.Count - 1].GetComponent<ShopItem>().Unlock();
            }
        }

        SetPriceText();
    }

    private void SetPriceText()
    {
        if (gameManager.GetPriceSkin(shopItems.Count) == 0)
            priceText.transform.parent.gameObject.SetActive(false);
        else
            priceText.text = gameManager.GetPriceSkin(shopItems.Count).ToString();
    }
    
    public void BuySkin()
    {
        if (PlayerPrefs.GetInt("Diamonds") >= gameManager.GetPriceSkin(shopItems.Count) && gameManager.GetPriceSkin(shopItems.Count) != 0)
        {
            PlayerPrefs.SetInt("Diamonds", PlayerPrefs.GetInt("Diamonds") - gameManager.GetPriceSkin(shopItems.Count));
            SetPriceText();
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>().UpdateDiamondsCount();
            gameManager.UnlockItem(shopItems).Unlock();
        }
    }
}
