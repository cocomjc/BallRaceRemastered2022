using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Road : MonoBehaviour
{
    [SerializeField] private GameObject[] obstaclesPrefabs;
    private GameObject currentObstacle = null;
    private int obstacleIndex = -1;
    private RoadsManager roadsManager;

    private void Awake()
    {
        roadsManager = RoadsManager.GetInstance();
    }

    public void LoadNewObstacles()
    {
        if (currentObstacle != null)
        {
            Destroy(currentObstacle);
        }
        obstacleIndex = Random.Range(0, obstaclesPrefabs.Length);
        if (obstacleIndex != -1 && roadsManager.GetLastRoad())
        {
            while (obstacleIndex == roadsManager.GetLastRoad().GetObstacleIndex())
            {
                Debug.Log("Same obstacle as last road, trying again");
                obstacleIndex = Random.Range(0, obstaclesPrefabs.Length);
                Debug.Log("New obstacle index is " + obstacleIndex + " and last road index is " + roadsManager.GetLastRoad().GetObstacleIndex());
            }
        }
        currentObstacle = Instantiate(obstaclesPrefabs[obstacleIndex], transform.position, Quaternion.identity, transform);
        currentObstacle.transform.SetParent(this.transform);
        //Debug.Log("Spawning obstacle index " + randomIndex);
    }
    
    public int GetObstacleIndex()
    {
        return obstacleIndex;
    }
}
