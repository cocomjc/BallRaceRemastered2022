using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndMenuManager : MonoBehaviour
{
    private GameManager gameManager;
    [SerializeField] private TextMeshProUGUI diamonds;
    
    // Start is called before the first frame update
    void Awake()
    {
        gameManager = GameManager.GetInstance();
        Debug.Log("gathered " + gameManager.GetLastRunDiamonds() + " diamonds with " + gameManager.GetLastRunBonus()  + "Bonus !");
        int totalDiamonds = (int)(gameManager.GetLastRunDiamonds() * gameManager.GetLastRunBonus());
        diamonds.text = "+" + totalDiamonds;
        PlayerPrefs.SetInt("Diamonds", PlayerPrefs.GetInt("Diamonds") + totalDiamonds);
    }
}
