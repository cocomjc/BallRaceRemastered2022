using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadsManager : Singleton<RoadsManager>
{
    private List<Road> roads = new List<Road>();
    
    [SerializeField] private float roadOffsetf = 30;
    [SerializeField] private int numberOfRoads = 5;
    [SerializeField] private GameObject roadPrefab;
    [SerializeField] private GameObject finishPrefab;
    private GameObject finishRoad = null;
    private int totalOfSpawnedRoads = 0;
    private int maxNumberOfRoads = 0;

    protected override void Awake()
    {
        base.Awake();
        maxNumberOfRoads = (PlayerPrefs.GetInt("Level") - 1) * 2 + 5;
        for (int i = 0; i < numberOfRoads && i <  maxNumberOfRoads; i++)
        {
            GameObject newRoad = Instantiate(roadPrefab, new Vector3(0, 0, i * roadOffsetf), Quaternion.identity);
            if (i > 1) //leave the first 2 roads empty
            {
                //Debug.Log("Loading obstacles for road " + i);
                newRoad.GetComponent<Road>().LoadNewObstacles();
            }
            roads.Add(newRoad.GetComponent<Road>());
            roads[roads.Count - 1].transform.SetParent(this.transform);
            //Debug.Log("Instatiating Road at " + i * roadOffsetf);
            totalOfSpawnedRoads++;
        }
    }

    public void MoveRoad()
    {
        if (totalOfSpawnedRoads <=  maxNumberOfRoads)
        {
            Road movedRoad = roads[0];
            roads.Remove(movedRoad);
            float newZ = roads[roads.Count - 1].transform.position.z + roadOffsetf;
            movedRoad.transform.position = new Vector3(0, 0, newZ);
            movedRoad.LoadNewObstacles();
            roads.Add(movedRoad);
            totalOfSpawnedRoads++;
        }
        else if (!finishRoad) {
            finishRoad = Instantiate(finishPrefab, new Vector3(0, 0, roads[roads.Count - 1].transform.position.z + roadOffsetf), Quaternion.identity);
            finishRoad.transform.SetParent(this.transform.parent.transform);
        }
    }

    public Road GetLastRoad()
    {
        if (roads.Count <= 0)
            return null;
        return (roads[roads.Count - 1]);
    }
}
