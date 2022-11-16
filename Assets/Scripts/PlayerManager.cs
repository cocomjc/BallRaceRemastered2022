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
    }

    private void UpdateDiamondsCount()
    {
        diamondsText.text = "Diamonds: " + runDiamonds;
    }

    private void UpdateLifeCount()
    {
        lifesText.text = "Lifes: " + lifes;
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.CompareTag("Obstacle"))
        {
            if (lifes > 0)
            {
                Debug.Log("Hit Obstacle to destroy removing a life");
                // Get the parent of the obstacle
                Destroy(hit.gameObject);
                //Transform obstacleParent = hit.gameObject.transform.parent;
                lifes--;
                UpdateLifeCount();
                GetComponent<PlayerMovements>().SlowPlayer();
            }
            else
            {
                gameManager.EndGame(runDiamonds);
            }
        }
        if (hit.gameObject.CompareTag("Ramp"))
        {
            GetComponent<PlayerMovements>().Boost();
        }
    }
}
