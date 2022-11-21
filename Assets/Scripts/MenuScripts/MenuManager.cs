using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    private GameManager gameManager;
    private PlayerManager playerManager;
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject endMenu;
    [SerializeField] private GameObject shopMenu;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI priceShieldText;


    private void Awake()
    {
        gameManager = GameManager.GetInstance();
        playerManager = GameObject.FindGameObjectsWithTag("Player")[0].GetComponent<PlayerManager>();
        mainMenu.SetActive(false);
        endMenu.SetActive(false);
        shopMenu.SetActive(false);
        switch (gameManager.GetGameState())
        {
            case (GameState.Menu):
                mainMenu.SetActive(true);
                break;
            case (GameState.End):
                endMenu.SetActive(true);
                playerManager.DisplayShields(false);
                break;
        }
        SetPriceText();
    }

    public void StartGame()
    {
        gameManager.StartGame();
        Debug.Log("Starting Game");
    }

    public void GoToShop()
    {        
        playerManager.DisplayShields(false);
        shopMenu.SetActive(true);
        Debug.Log("Going to shop");
    }

    public void ExitShop()
    {
        playerManager.DisplayShields(true);
        shopMenu.SetActive(false);
        Debug.Log("Exiting shop");
    }

    public void GoToMainMenu() {
        playerManager.DisplayShields(true);
        gameManager.SetGameState(GameState.Menu);
        endMenu.SetActive(false);
        mainMenu.SetActive(true);
        levelText.text = "Level: " + PlayerPrefs.GetInt("Level");
        Debug.Log("Going to main menu");
    }

    public void GoToRemoveAds()
    {
        Debug.Log("Going to remove ads");
    }
    
    private void SetPriceText()
    {
        priceShieldText.text = GetShieldPrice().ToString();
    }

    private int GetShieldPrice()
    {
        return ((PlayerPrefs.GetInt("Shields") - 1) * 384);
    }

    public void BuyShield()
    {
        if (PlayerPrefs.GetInt("Diamonds") >= GetShieldPrice())
        {
            PlayerPrefs.SetInt("Diamonds", PlayerPrefs.GetInt("Diamonds") - GetShieldPrice());
            PlayerPrefs.SetInt("Shields", PlayerPrefs.GetInt("Shields") + 1);
            SetPriceText();
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>().ResetShield();
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>().UpdateDiamondsCount();
        }
    }
}
