using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    private GameState gameState;
    [SerializeField] private bool resetEachLaunchMode;


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
        gameState = GameState.Menu;
//        SceneManager.LoadScene("GameScene", LoadSceneMode.Additive);
//        SceneManager.LoadScene("MenuScene", LoadSceneMode.Additive);
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

    public void EndGame(int collectedDiamons, bool win)
    {
        if (win)
        {
            int level = PlayerPrefs.GetInt("Level");
            PlayerPrefs.SetInt("Level", level + 1);
        }
        Debug.Log("Game End with " + collectedDiamons + " diamonds");
        PlayerPrefs.SetInt("Diamonds", collectedDiamons + PlayerPrefs.GetInt("Diamonds"));
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
}

public enum GameState
{
    Menu,
    Game,
    End
}