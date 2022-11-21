using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    private GameState gameState;
    [SerializeField] private bool resetEachLaunchMode;
    [SerializeField] private int initShields = 3;
    private float lastRunBonus = 1;
    private int lastRunDiamonds = 0;

    protected override void Awake()
    {
        base.Awake();
        if (resetEachLaunchMode)
        {
            PlayerPrefs.DeleteAll();
            PlayerPrefs.SetInt("Level", 1);
        }
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
        GameObject.FindGameObjectsWithTag("Player")[0].GetComponent<PlayerManager>().StartGame();
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
}

public enum GameState
{
    Menu,
    Game,
    End
}