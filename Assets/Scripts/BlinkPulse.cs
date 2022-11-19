using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BlinkPulse : MonoBehaviour
{
    [SerializeField] private float minimum = 0.3f;
    [SerializeField] private float maximum = 1f;
    [SerializeField] private float cyclesPerSecond = 2.0f;
    [SerializeField] private bool pulse = false;
    //private Color initialColorEmission;
    private float a;
    private bool increasing = true;

    void Start()
    {
        a = maximum;
        //initialColorEmission = GetComponent<Renderer>().material.GetColor("_EmissionColor");
    }

    void Update()
    {
        if (pulse)
        {
            float t = Time.deltaTime;
            if (a >= maximum) increasing = false;
            if (a <= minimum) increasing = true;
            a = increasing ? a += t * cyclesPerSecond * 2 : a -= t * cyclesPerSecond;
            Color color = GetComponent<Renderer>().material.color;
            color.a = a;
            GetComponent<Renderer>().material.color = color;
            //float intensity = (a - minimum) / (maximum - minimum) * -10;
            //GetComponent<Renderer>().material.SetColor("_EmissionColor", initialColorEmission );
        }
    }

    public void StartPulse()
    {
        pulse = true;
    }

    public void StopPulse()
    {
        Color color = GetComponent<Renderer>().material.color;
        color.a = 0f;
        GetComponent<Renderer>().material.color = color;
        pulse = false;
    }
}
