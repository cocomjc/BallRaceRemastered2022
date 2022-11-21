using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DataPersistenceManager : Singleton<DataPersistenceManager>
{
    [Header("File Storage Config")]
    [SerializeField] private string fileName = "";
    private GameData gameData;
    private List<IDataPersistence> dataPersistenceObjects;
    private FileDataHandler fileDataHandler;

    private void Start()
    {
        fileDataHandler = new FileDataHandler(Application.persistentDataPath, fileName);
        dataPersistenceObjects = FinAllDataPersistenceObject();
        LoadGame();
    }
    
    public void NewGame()
    {
        this.gameData = new GameData();
    }

    public void LoadGame()
    {
        gameData = fileDataHandler.LoadData();
        
        if (gameData == null)
        {
            NewGame();
        }

        foreach (IDataPersistence dataPersistenceObject in dataPersistenceObjects)
        {
            dataPersistenceObject.LoadData(gameData);
        }
    }

    public void SaveGame() 
    {
        foreach (IDataPersistence dataPersistenceObject in dataPersistenceObjects)
        {
            dataPersistenceObject.SaveData(gameData);
        }

        fileDataHandler.SaveData(gameData);
    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }

    private List<IDataPersistence> FinAllDataPersistenceObject()
    {
        IEnumerable<IDataPersistence> dataPersistenceObjects = FindObjectsOfType<MonoBehaviour>().OfType<IDataPersistence>();
        return new List<IDataPersistence>(dataPersistenceObjects);
    }
}
