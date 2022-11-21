using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISwipeAnim : MonoBehaviour
{
    private int goalCoord;
    private int maximum = 180;
    private int minimum = -180;
    private bool increasing = true;
    private float x;
    [SerializeField] private float animationSpeed = 200f;

    void Update()
    {
        float t = Time.deltaTime;
        if (x >= maximum) increasing = false;
        if (x <= minimum) increasing = true;

        x = increasing ? x += t * animationSpeed * 2 : x -= t * animationSpeed * 2;
        transform.localPosition = new Vector3(x, transform.localPosition.y, transform.localPosition.z);
    }
}
