using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndBonus : MonoBehaviour
{
    [SerializeField] private ParticleSystem confettisR;
    [SerializeField] private ParticleSystem confettisL;
    [SerializeField] private TextMeshProUGUI bonusText;
    [SerializeField] private float bonus = 1f;
    [SerializeField] [Range(0f, 1f)] float lerpTime;
    [SerializeField] private Color[] allColors;
    private int colorIndex = 0;
    private bool endTrigger = false;
    private float t = 0f;

    private void Start()
    {
        Renderer rend = GetComponent<Renderer>();

        //rend.material = new Material(Shader.Find("Specular"));
        int computedIndex = Mathf.RoundToInt((bonus - 1) / 0.4f);
        while (computedIndex >= allColors.Length)
        {
            computedIndex -= allColors.Length;
        }
        rend.material.color = allColors[computedIndex];
        bonusText.text = "x" + bonus.ToString("0.0");
        GetComponent<AudioSource>().pitch = 0.5f * (bonus - .5f);
    }

    public void TriggerBonus()
    {
        endTrigger = true;
        confettisR.Play();
        confettisL.Play();
    }

    private void Update()
    {
        if (endTrigger)
        {
            GetComponent<Renderer>().material.color = Color.Lerp(GetComponent<Renderer>().material.color, allColors[colorIndex], lerpTime);

            t = Mathf.Lerp(t, 1f, lerpTime*Time.deltaTime);
            if (t > 0.1f)
            {
                t = 0f;
                colorIndex++;
                colorIndex = (colorIndex >= allColors.Length) ? 0 : colorIndex;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GetComponent<AudioSource>().Play();
        }
    }

    public float GetBonus()
    {
        return bonus;
    }
}
