using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class FileDataHandler
{
    private String dataDirPath = "";
    private String dataFileName = "";

    public FileDataHandler(String dataDirPath, String dataFileName)
    {
        this.dataDirPath = dataDirPath;
        this.dataFileName = dataFileName;
    }

    public GameData LoadData()
    {
        String fullPath = Path.Combine(this.dataDirPath, this.dataFileName);
        GameData gameData = null;
        
        if (File.Exists(fullPath))
        {
            try
            {
                String dataToLoad = "";
                using (FileStream stream = new FileStream(fullPath, FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        dataToLoad = reader.ReadToEnd();
                    }
                }
                gameData = JsonUtility.FromJson<GameData>(dataToLoad);
            }
            catch (Exception e)
            {
                Debug.LogError("Error loading data: " + e.Message);
            }           
        }
        return gameData;
    }

    public void SaveData(GameData gameData)
    {
        String fullPath = Path.Combine(this.dataDirPath, this.dataFileName);
        try
        {
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));
            String dataToStore = JsonUtility.ToJson(gameData, true);
            using (FileStream stream = new FileStream(fullPath, FileMode.Create))
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(dataToStore);
                }
            }
        }
        catch (Exception e)
        {
            Debug.LogError(e.Message);
        }
    }
}
