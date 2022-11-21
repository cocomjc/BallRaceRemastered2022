using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PortalBehavior : MonoBehaviour
{
    private int value;
    [SerializeField] private TextMeshProUGUI valueText;
    [SerializeField] private AudioClip positiveSound;
    [SerializeField] private AudioClip negativeSound;
    [SerializeField] private Color positiveColor;
    [SerializeField] private Color negativeColor;
    [SerializeField] private Image portal;

    private void Start()
    {
        value = Random.Range(1, 2);
        value = Random.Range(0, 4) != 0 ? value : -value;
        if (Random.Range(0, 5) != 0)
            gameObject.SetActive(false);
        valueText.text = value > 0 ? "+" + value : value.ToString();
        portal.color = value > 0 ? positiveColor : negativeColor;
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
