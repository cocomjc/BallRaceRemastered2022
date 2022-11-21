using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EndMenuManager : MonoBehaviour
{
    private GameManager gameManager;
    [SerializeField] private TextMeshProUGUI diamonds;
    [SerializeField] private TextMeshProUGUI winLose;
    [SerializeField] private Image winLoseImage;
    [SerializeField] private Color loseColor;

    // Start is called before the first frame update
    void Awake()
    {
        gameManager = GameManager.GetInstance();
        if (gameManager.GetLastRunBonus() > 1)
        {
            winLose.text = "LEVEL FINISHED";
        }
        else
        {
            winLose.text = "YOU LOST";
            winLoseImage.color = loseColor;
        }
        int totalDiamonds = (int)(gameManager.GetLastRunDiamonds() * gameManager.GetLastRunBonus());
        diamonds.text = "+" + totalDiamonds;
        PlayerPrefs.SetInt("Diamonds", PlayerPrefs.GetInt("Diamonds") + totalDiamonds);
    }
}
