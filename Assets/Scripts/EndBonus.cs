using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.ProBuilder.Shapes;

public class EndBonus : MonoBehaviour
{
    [SerializeField] private float bonus = 1f;
    [SerializeField] private TextMeshProUGUI bonusText;
    [SerializeField] private Color color;

    private void Start()
    {
        Renderer rend = GetComponent<Renderer>();

        rend.material = new Material(Shader.Find("Specular"));
        rend.material.color = color;
        bonusText.text = "x" + bonus.ToString("0.0");
        GetComponent<AudioSource>().pitch = 0.5f * (bonus - .5f);
    }

    public void TriggerBonus()
    {
        //GetComponent<ParticleSystem>().Play();
        GetComponent<AudioSource>().Play();
        GetComponent<MeshRenderer>().enabled = false;
    }

    public float GetBonus()
    {
        return bonus;
    }
}
