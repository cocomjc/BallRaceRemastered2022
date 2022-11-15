using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Road : MonoBehaviour
{
    [SerializeField] private GameObject[] obstaclesPrefabs;
    private GameObject currentObstacle = null;

    public void LoadNewObstacles()
    {
        if (currentObstacle != null)
        {
            Destroy(currentObstacle);
        }
        int randomIndex = Random.Range(0, obstaclesPrefabs.Length);
        currentObstacle = Instantiate(obstaclesPrefabs[randomIndex], transform.position, Quaternion.identity, transform);
        currentObstacle.transform.SetParent(this.transform);
        //Debug.Log("Spawning obstacle index " + randomIndex);
    }
}
