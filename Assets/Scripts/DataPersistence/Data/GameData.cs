using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData
{
    public List<string> unlockedSkin;
    public string selectedSkin;

    //Default values if no previous Data
    public GameData()
    {
        unlockedSkin = new List<string>();
        unlockedSkin.Add("Default");
        selectedSkin = "Default";
    }
}
