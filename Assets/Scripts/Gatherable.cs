using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gatherable : MonoBehaviour
{
    [SerializeField] private GatherableType type;

    public GatherableType Type { get { return type; } }
}

public enum GatherableType
{
    Diamond,
    Life
}
