using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldBehavior : MonoBehaviour
{
    [SerializeField] private float scrollSpeed = 0.5f;
    [SerializeField] private float shieldTimer = 1f;
    private Renderer rend;
    private bool isShieldActive = false;
    private float timer = 0f;

    void Start()
    {
        rend = GetComponent<Renderer>();
        DisableShield();
    }

    // Update is called once per frame
    void Update()
    {
        if (isShieldActive && timer > 0)
        {
            timer -= Time.deltaTime;
            float offset = Time.time * scrollSpeed;
            rend.material.SetTextureOffset("_MainTex", new Vector2(0, offset));
        }
        else
        {
            DisableShield();
        }
    }

    public void EnableShield()
    {
        isShieldActive = true;
        rend.enabled = true;
        GetComponent<BlinkPulse>().StartPulse();
        GetComponent<AudioSource>().Play();
        timer = shieldTimer;
    }

    public void DisableShield()
    {
        isShieldActive = false;
        GetComponent<BlinkPulse>().StopPulse();
        rend.material.SetTextureOffset("_MainTex", new Vector2(0, 0));
        rend.enabled = false;

    }
}
