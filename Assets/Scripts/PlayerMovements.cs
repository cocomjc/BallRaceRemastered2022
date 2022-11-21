using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovements : MonoBehaviour
{
    private PlayerControls playerInput;
    private CharacterController controller;
    private float verticalVelocity;
    private float gravityValue = 9.81f;
    private MovementState movementState = MovementState.Playing;
    private float slowFactor = 0f;
    private bool flying = false;
    [SerializeField] private AudioSource ballHitGround;
    [SerializeField] private AudioSource ballRoll;
    [SerializeField] private float forwardSpeed = 20f;
    [SerializeField] private float lateralSpeed = 20f;
    [SerializeField] private float jumpForce = 15f;
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

    // Update is called once per frame
    void Update()
    {
        switch (movementState)
        {
            case MovementState.Playing:
                Move();
                break;
            case MovementState.End:
                EndBehavior();
                break;
        }
    }

    private void Move()
    {
        slowFactor = slowFactor > 0 ? slowFactor - Time.deltaTime * 10 : 0;
        if (controller.isGrounded && verticalVelocity < 0)
        {
            if (flying)
            {
                ballHitGround.Play();
                flying = false;
            }
            verticalVelocity = 0f;
            ToggleRollSound(true);
        }
        verticalVelocity -= gravityValue * Time.deltaTime * mass;
        float horizontalVelocity = Mathf.Abs(playerInput.Player.Move.ReadValue<Vector2>().x) < .1 ? 0 : playerInput.Player.Move.ReadValue<Vector2>().x * lateralSpeed;
        controller.Move(new Vector3(horizontalVelocity, verticalVelocity, forwardSpeed - slowFactor) * Time.deltaTime);
    }

    public void SlowPlayer() {
        slowFactor = forwardSpeed - 4f;
    }

    public void Boost()
    {
        verticalVelocity = jumpForce;
        flying = true;
        ToggleRollSound(false);
    }

    public void SetPaused(bool paused)
    {      
        if (paused)
        {
            movementState = MovementState.Paused;
            playerInput.Disable();
            ToggleRollSound(false);
        }
        else
        {
            movementState = MovementState.Playing;
            playerInput.Enable();
            ToggleRollSound(true);
        }
    }

    public void TriggerEnd()
    {
        movementState = MovementState.End;
        playerInput.Disable();
    }

    private void EndBehavior()
    {
        controller.Move(new Vector3(Mathf.Abs(transform.position.x) > .3 ? -transform.position.x * .05f : 0, 0, 1 * forwardSpeed * Time.deltaTime));
    }

    private Vector2 CameraSizePortrait()
    {
        if (-4.99f < Mathf.Abs(Screen.height / 16 - Screen.width / 9) && Mathf.Abs(Screen.height / 16 - Screen.width / 9) < 4.99f)
        {
            //Debug.Log("16:9");
            return new Vector2(Camera.main.orthographicSize * 2 / 16 * 9, Camera.main.orthographicSize * 2);
        }
        else if (-4.99f < Mathf.Abs(Screen.height / 18 - Screen.width / 9) && Mathf.Abs(Screen.height / 18 - Screen.width / 9) < 4.99f)
        {
            //Debug.Log("18:9");
            return new Vector2(Camera.main.orthographicSize * 2 / 18 * 9, Camera.main.orthographicSize * 2);
        }
        else if (-4.99f < Mathf.Abs(Screen.height / 19 - Screen.width / 9) && Mathf.Abs(Screen.height / 19 - Screen.width / 9) < 4.99f)
        {
            //Debug.Log("19:9");
            return new Vector2(Camera.main.orthographicSize * 2 / 19 * 9, Camera.main.orthographicSize * 2);
        }
        else /*Debug.Log("Screen proportions not recognized");*/ return new Vector2(Screen.width, Screen.height);
    }

    private void ToggleRollSound(bool toggle)
    {
        if (toggle)
        {
            if (!ballRoll.isPlaying)
            {
                ballRoll.Play();
            }
        }
        else
        {
            ballRoll.Stop();
        }
    }

    private enum MovementState
    {
        Paused,
        Playing,
        End
    }
}
