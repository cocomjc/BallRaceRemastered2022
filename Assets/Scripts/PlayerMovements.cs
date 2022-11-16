using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovements : MonoBehaviour
{
    private PlayerControls playerInput;
    private CharacterController controller;
    private float verticalVelocity;
    private float gravityValue = 9.81f;
    private bool isPaused = false;
    [SerializeField] private float forwardSpeed = 20f;
    [SerializeField] private float lateralSpeed = 20f;
    [SerializeField] private float mass = 1f;

    
    private void Awake()
    {
        playerInput = new PlayerControls();
        controller = GetComponent<CharacterController>();
    }
        
    private void OnEnable()
    {
        playerInput.Enable();
    }

    private void OnDisable()
    {
        playerInput.Disable();
    }

    public float GetNormalizedfDirection()
    {
        return playerInput.Player.Move.ReadValue<Vector2>().x / Screen.width;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isPaused)
        {
            if (controller.isGrounded && verticalVelocity < 0)
                verticalVelocity = 0f;
            verticalVelocity -= gravityValue * Time.deltaTime * mass;
            //Debug.Log(GetNormalizedfDirection());
            controller.Move(new Vector3(GetNormalizedfDirection() * lateralSpeed, verticalVelocity, 1 * forwardSpeed) * Time.deltaTime);
        }
    }

    public void Boost()
    {
        verticalVelocity = 10f;
    }

    public void SetPaused(bool paused)
    {      
        isPaused = paused;
        if (paused)
        {
            playerInput.Disable();
        }
        else
        {
            playerInput.Enable();
        }
    }
}
