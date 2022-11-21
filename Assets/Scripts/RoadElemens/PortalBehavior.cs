using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PortalBehavior : MonoBehaviour
{
    [SerializeField] private int value;
    [SerializeField] private TextMeshProUGUI valueText;
    [SerializeField] private AudioClip positiveSound;
    [SerializeField] private AudioClip negativeSound;

    private void Start()
    {
        valueText.text = value > 0 ? "+" + value : value.ToString();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerManager playerManager = other.GetComponent<PlayerManager>();
            playerManager.EditShields(value);
            GetComponent<AudioSource>().clip = value > 0 ? positiveSound : negativeSound;
            GetComponent<AudioSource>().Play();
        }
    }
}
