using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadsManager : Singleton<RoadsManager>
{
    private List<Road> roads = new List<Road>();
    
    [SerializeField] private float roadOffsetf = 30;
    [SerializeField] private int numberOfRoads = 5;
    [SerializeField] private GameObject roadPrefab;

    protected override void Awake()
    {
        base.Awake();
        for (int i = 0; i < numberOfRoads; i++)
        {
            GameObject newRoad = Instantiate(roadPrefab, new Vector3(0, 0, i * roadOffsetf), Quaternion.identity);
            if (i > 2)
            {
                //Debug.Log("Loading obstacles for road " + i);
                newRoad.GetComponent<Road>().LoadNewObstacles();
            }
            roads.Add(newRoad.GetComponent<Road>());
            roads[roads.Count - 1].transform.SetParent(this.transform);
            //Debug.Log("Instatiating Road at " + i * roadOffsetf);
        }
    }

    public void MoveRoad()
    {
        Road movedRoad = roads[0];
        roads.Remove(movedRoad);
        float newZ = roads[roads.Count - 1].transform.position.z + roadOffsetf;
        movedRoad.transform.position = new Vector3(0, 0, newZ);
        movedRoad.LoadNewObstacles();
        roads.Add(movedRoad);
    }

    public Road GetLastRoad()
    {
        if (roads.Count <= 0)
            return null;
        return (roads[roads.Count - 1]);
    }
}
