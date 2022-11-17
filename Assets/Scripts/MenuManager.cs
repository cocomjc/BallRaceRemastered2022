using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    GameManager gameManager;
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject endMenu;
    [SerializeField] private GameObject shopMenu;
    [SerializeField] private TextMeshProUGUI levelText;


    private void Awake()
    {
        gameManager = GameManager.GetInstance();
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
                break;
        }
    }
    
    public void StartGame()
    {
        gameManager.StartGame();
        Debug.Log("Starting Game");
    }

    public void GoToShop()
    {
        shopMenu.SetActive(true);
        Debug.Log("Going to shop");
    }

    public void ExitShop()
    {
        shopMenu.SetActive(false);
        Debug.Log("Exiting shop");
    }

    public void GoToMainMenu() {
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
}
