using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>, IDataPersistence
{
    private GameState gameState;
    [SerializeField] private bool resetEachLaunchMode;
    [SerializeField] private int initShields = 3;
    private float lastRunBonus = 1;
    private int lastRunDiamonds = 0;
    private List<string> unlockedItems = new List<string>();

    protected override void Awake()
    {
        base.Awake(); 
        if (resetEachLaunchMode)
        {
            PlayerPrefs.DeleteAll();
            PlayerPrefs.SetInt("Level", 1);
        }
    }

    public void LoadData(GameData data)
    {
        PlayerPrefs.SetString("SelectedSkin", data.selectedSkin);
        unlockedItems = data.unlockedSkin;
    }

    public void SaveData(GameData data)
    {
        data.selectedSkin = PlayerPrefs.GetString("SelectedSkin", "Default");
    }

    private void Start()
    {
        if (!PlayerPrefs.HasKey("Shields"))
        {
            PlayerPrefs.SetInt("Shields", initShields);
        }
        gameState = GameState.Menu;
        if (SceneManager.sceneCount > 1)
        {
            SceneManager.UnloadSceneAsync("MenuScene");
            SceneManager.UnloadSceneAsync("GameScene");
        }
        SceneManager.LoadScene("GameScene", LoadSceneMode.Additive);
        SceneManager.LoadScene("MenuScene", LoadSceneMode.Additive);

    }

    public void StartGame()
    {
        gameState = GameState.Game;
        SceneManager.UnloadSceneAsync("MenuScene");
        //Tell player to start the game
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>().StartGame();
    }

    public void ResetScene()
    {
        SceneManager.UnloadSceneAsync("GameScene");
        SceneManager.LoadScene("GameScene", LoadSceneMode.Additive);
        SceneManager.LoadScene("MenuScene", LoadSceneMode.Additive);
    }

    public void EndGame(int runDiamonds, float bonus)
    {
        lastRunDiamonds = runDiamonds;
        lastRunBonus = bonus;
        if (lastRunBonus > 1)
        {
            PlayerPrefs.SetInt("Level", PlayerPrefs.GetInt("Level") + 1);
        }
        Debug.Log("Game Ended with " + runDiamonds + " diamonds");
        gameState = GameState.End;
        ResetScene();
    }

    public GameState GetGameState()
    {
        return (gameState);
    }
    
    public void SetGameState(GameState state)
    {
        gameState = state;
    }

    public int GetLastRunDiamonds() {
        return (lastRunDiamonds);
    }

    public float GetLastRunBonus()
    {
        return (lastRunBonus);
    }

    public ShopItem UnlockItem(List<GameObject> items)
    {
        int rand = Random.Range(0, items.Count - unlockedItems.Count);
        Debug.Log("Random: " + rand + " and " + (items.Count - unlockedItems.Count));

        foreach (GameObject item in items)
        {
            string id = item.GetComponent<ShopItem>().GetSkin().GetId();
            if (!unlockedItems.Contains(id))
            {
                if (rand == 0)
                {
                    unlockedItems.Add(id);
                    return (item.GetComponent<ShopItem>());
                }
                rand--;
            }
        }
        return null;
    }

    public bool IsItemUnlocked(string id)
    {
        return (unlockedItems.Contains(id));
    }

    public int GetPrice()
    {
        return (unlockedItems.Count * 264);
    }
}

public enum GameState
{
    Menu,
    Game,
    End
}