using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlinkPulse : MonoBehaviour
{
    [SerializeField] private float minimum = 0.3f;
    [SerializeField] private float maximum = 1f;
    [SerializeField] private float cyclesPerSecond = 2.0f;
    [SerializeField] private bool pulse = false;
    [SerializeField] private bool isUI = false;
    [SerializeField] private AudioSource audioSource = null;
    private float a;
    private bool increasing = true;

    void Start()
    {
        a = maximum;
    }

    void Update()
    {
        if (pulse)
        {
            float t = Time.deltaTime;
            if (a >= maximum) {
                increasing = false;
                if (audioSource) audioSource.Play(); ;
            }
            if (a <= minimum) increasing = true;
            a = increasing ? a += t * cyclesPerSecond * 2 : a -= t * cyclesPerSecond;
            if (!isUI)
            {
                Color color = GetComponent<Renderer>().material.color;
                color.a = a;
                GetComponent<Renderer>().material.color = color;
            }
            else
            {
                Color color = GetComponent<Image>().color;
                color.a = a;
                GetComponent<Image>().color = color;
            }
            
        }
    }

    public void StartPulse()
    {
        pulse = true;
    }

    public void StopPulse()
    {
        if (!isUI)
        {
            Color color = GetComponent<Renderer>().material.color;
            color.a = 0f;
            GetComponent<Renderer>().material.color = color;
        }
        else
        {
            Color color = GetComponent<Image>().color;
            color.a = 0f;
            GetComponent<Image>().color = color;
        }
        pulse = false;
    }
}
