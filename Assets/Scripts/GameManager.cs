using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    private GameState gameState;
    
    private void Start()
    {
        gameState = GameState.Menu;
        //SceneManager.LoadScene("GameScene", LoadSceneMode.Additive);
        //SceneManager.LoadScene("MenuScene", LoadSceneMode.Additive);
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

    public void EndGame()
    {
        Debug.Log("Game End");
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