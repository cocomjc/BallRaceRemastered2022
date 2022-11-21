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
        
        priceText.text = gameManager.GetPrice().ToString();
        if (gameManager.GetPrice() == 0)
            priceText.transform.parent.gameObject.SetActive(false);
        foreach (GameObject skinPrefab in skinPrefabs)
        {
            shopItems.Add(Instantiate(shopItemPrefab, skinDisplay.transform));
            shopItems[shopItems.Count - 1].GetComponent<ShopItem>().SetSkin(skinPrefab);
            if (gameManager.IsItemUnlocked(skinPrefab.GetComponent<SkinComponent>().GetId()))
            {
                shopItems[shopItems.Count - 1].GetComponent<ShopItem>().Unlock();
            }
        }
    }

    public void BuySkin()
    {
        if (PlayerPrefs.GetInt("Diamonds") >= gameManager.GetPrice() && gameManager.GetPrice() != 0)
        {
            Debug.Log("Bought");
            PlayerPrefs.SetInt("Diamonds", PlayerPrefs.GetInt("Diamonds") - gameManager.GetPrice());
            priceText.text = gameManager.GetPrice().ToString();
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>().UpdateDiamondsCount();
            gameManager.UnlockItem(shopItems).Unlock();
        }
    }
}
