using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerManager : MonoBehaviour
{
    private RoadsManager roadsManager;
    private GameManager gameManager;
    [SerializeField] private TextMeshProUGUI lifesText;
    [SerializeField] private TextMeshProUGUI diamondsText;
    [SerializeField] private int lifes;
    private int runDiamonds;
    private float endBonus = 1;

    private void Start()
    {
        roadsManager = RoadsManager.GetInstance();
        gameManager = GameManager.GetInstance();
        GetComponent<PlayerMovements>().SetPaused(true);
        UpdateDiamondsCount();
        UpdateLifeCount();
    }

    public void StartGame()
    {
        GetComponent<PlayerMovements>().SetPaused(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("RoadSpawn"))
        {
            roadsManager.MoveRoad();
        }
        if (other.CompareTag("Gatherable"))
        {
            Gatherable gatherable = other.GetComponent<Gatherable>();
            switch (gatherable.Type)
            {
                case GatherableType.Diamond:
                    runDiamonds++;
                    UpdateDiamondsCount();
                    break;
                case GatherableType.Life:
                    lifes++;
                    UpdateLifeCount();
                    break;
            }
            Destroy(other.gameObject);
        }
        if (other.CompareTag("FinishLine") && gameManager.GetGameState() == GameState.Game)
        {
            gameManager.SetGameState(GameState.End);
            EndBehavior();
        }
    }

    private void UpdateDiamondsCount()
    {
        diamondsText.text = "Diamonds: " + (PlayerPrefs.GetInt("Diamonds") + runDiamonds);
    }

    private void UpdateLifeCount()
    {
        lifesText.text = "Lifes: " + lifes;
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.CompareTag("EndBonus"))
        {
            endBonus = hit.gameObject.GetComponent<EndBonus>().GetBonus();
            if (!(lifes > 1))
            {
                GetComponent<PlayerMovements>().SetPaused(true);
                //Wait for a second
            }
        }
        if (hit.gameObject.CompareTag("Obstacle"))
        {
            if (lifes > 1)
            {
                Debug.Log("Hit Obstacle to destroy removing a life");
                // Get the parent of the obstacle
                Destroy(hit.gameObject);
                //Transform obstacleParent = hit.gameObject.transform.parent;
                lifes--;
                UpdateLifeCount();
                GetComponent<PlayerMovements>().SlowPlayer();
            }
            else if (gameManager.GetGameState() == GameState.Game)
            {
                gameManager.EndGame(runDiamonds, false);
            }
        }
        if (hit.gameObject.CompareTag("Ramp"))
        {
            GetComponent<PlayerMovements>().Boost();
        }
    }

    private void EndBehavior()
    {
        GetComponent<PlayerMovements>().TriggerEnd();
        //gameManager.EndGame(runDiamonds, true);
    }
}
