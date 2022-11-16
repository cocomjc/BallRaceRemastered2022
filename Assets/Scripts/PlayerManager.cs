using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private RoadsManager roadsManager;
    private GameManager gameManager;

    private void Start()
    {
        roadsManager = RoadsManager.GetInstance();
        gameManager = GameManager.GetInstance();
        GetComponent<PlayerMovements>().SetPaused(true);
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
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.CompareTag("Obstacle"))
        {
            gameManager.EndGame();
        }
        if (hit.gameObject.CompareTag("Ramp"))
        {
            GetComponent<PlayerMovements>().Boost();
        }
    }
}
