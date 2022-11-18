using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndBonus : MonoBehaviour
{
    [SerializeField] private float bonus = 1f;
    [SerializeField] private TextMeshProUGUI bonusText;

    private void Start()
    {
        bonusText.text = "x" + bonus.ToString("0.0");
    }

    public float GetBonus()
    {
        return bonus;
    }
}
