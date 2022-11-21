using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinManager : MonoBehaviour
{
    private GameObject playerSkin = null;

    public void Start()
    {
        SetSkin(PlayerPrefs.GetString("SelectedSkin", "Default"));
    }

    public void SetSkin(string skinId)
    {
        GameObject[] skinPrefabs = Resources.LoadAll<GameObject>("Skins");
        
        if (playerSkin != null)
            Destroy(playerSkin);

        foreach (GameObject skinPrefab in skinPrefabs)
        {
            if (skinPrefab.GetComponent<SkinComponent>().GetId() == skinId)
            {
                playerSkin = Instantiate(skinPrefab, transform);
                PlayerPrefs.SetString("SelectedSkin", skinId);
            }
        }
    }

    public string GetSkin()
    {
        return playerSkin.GetComponent<SkinComponent>().GetId();
    }
}
