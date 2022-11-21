using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gatherable : MonoBehaviour
{
    [SerializeField] private GatherableType type;
    [SerializeField] private GameObject body;

    public GatherableType Type { get { return type; } }

    public void TriggerGathered()
    {
        GetComponent<ParticleSystem>().Play();
        GetComponent<AudioSource>().Play();
        body.GetComponent<MeshRenderer>().enabled = false;
    }
}

public enum GatherableType
{
    Diamond,
    Life
}
