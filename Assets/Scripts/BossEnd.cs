using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.HID;

public class BossEnd : MonoBehaviour
{
    [SerializeField] private ParticleSystem[] confettis;
    [SerializeField] private float bonus;

    public float GetBonus()
    {
        return bonus;
    }

    public void TriggerBonus()
    {
        foreach (ParticleSystem confetti in confettis)
        {
            confetti.Play();
        }
        GetComponent<AudioSource>().Play();
    }
}
