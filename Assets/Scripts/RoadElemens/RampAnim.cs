using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RampAnim : MonoBehaviour
{
    [SerializeField] private float speed = 1;
    [SerializeField] private GameObject[] rampArrows;
    private int index = 0;
    private float time = 0;

    void Update()
    {
        time = time + Time.deltaTime * speed;
        if (time >= 1)
        {
            time = 0;
            rampArrows[index].GetComponent<Renderer>().material.DisableKeyword("_EMISSION");
            index = (index + 1) % rampArrows.Length;
            rampArrows[index].GetComponent<Renderer>().material.EnableKeyword("_EMISSION");
        }

    }
}
