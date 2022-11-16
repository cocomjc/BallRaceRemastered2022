using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndBonus : MonoBehaviour
{
    [SerializeField] private float bonus = 1f;

    public float GetBonus()
    {
        return bonus;
    }
}
