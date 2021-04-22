using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    //Components
    public Rigidbody2D rigidbodyPlayer;
    public LayerMask layerCanJump;
    public SpriteRenderer spriteRenderer;
    public ParticleSystem particleSystem;
    public Canvas canvas0;
    public Canvas canvas1;
    public Clock timer;

    //Raycast
    public RaycastHit2D hit;

    //Variables

    //Active
    //Floats/ints
    private int fuelPercentage;
    private float fuel = 0f;
    private float animTimer = 0f;
    private float spriteSprintModifier = 0f;
    //Bools
    public bool isOnFuel = false;
    public bool fallSound = true;
    private bool escaped = false;

    //Static
    private float jumpHeight = 500f;
    private float zoomCamera = 10.5f;
    private float sprintMovementSpeed = 7.5f;
    private float walkMovementSpeed = 5f;
    private float movementspeed = 5f;
    private float jumpFromFeet = 3f;


    //Input
    //Axis
    private float horizontal;
    private float zoomIn;
    private float zoomOut;
    //Buttons
    private bool jump;
    private bool shift;



    //Strings
    public Text errorMessage;
    public Text fuelMessage;

    //Audio
    //Scource
    public AudioSource soundBox;
    //Clips
    public AudioClip jumpSound;
    public AudioClip rocketJumpSound;
    public AudioClip jumpLandingSound;


    //Sprites
    public Sprite sprite0;
    public Sprite sprite1;
    public Sprite sprite2;
    public Sprite sprite3;
    public Sprite sprite4;
    public Sprite sprite5;
    public Sprite sprite6;


    // Start is called before the first frame update
    void Start()
    {
        jumpFromFeet = 2 / 2 + 0.05f;
        canvas1.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Gets all axis/buttons
        Inputs();
        //checks if there is ground beneath player
        CheckForGround();
        //adds movement
        MovePlayer();
        // Animations for the player character
        AnimateCharacter();
        //Resets error message on fuelpad
        ErrorMessage();
        //Checks if the player pressed the escape button
        ToggleEscapeUI();
    }

    //Located in Update()
    private void Inputs()
    {
        horizontal = Input.GetAxis("Horizontal");
        jump = Input.GetButtonDown("Jump");
        shift = Input.GetButton("Shift");
        zoomIn = Input.GetAxis("ZoomIn");
        zoomOut = Input.GetAxis("ZoomOut");
    }

    private void CheckForGround()
    {
        hit = Physics2D.Raycast(transform.position, Vector2.down, jumpFromFeet, layerCanJump);
    }
    //Located in Update()
    private void MovePlayer()
    {
        Vector2 horizontalspeed = horizontal * movementspeed * Vector2.right;
        rigidbodyPlayer.velocity = new Vector2(horizontalspeed.x, rigidbodyPlayer.velocity.y);

    }

    //Located in Update()
    private void AnimateCharacter()
    {
        animTimer += Time.deltaTime * spriteSprintModifier;
        // Is Not moving
        if (horizontal == 0)
        {
            spriteRenderer.sprite = sprite0;
        }
        else if (horizontal > 0)
        {
            //Goes right
            if (hit.collider == null)
            {
                spriteRenderer.sprite = sprite3;
            }
            else if (animTimer % 0.5f < 0.25f)
            {
                spriteRenderer.sprite = sprite1;
            }
            else if (animTimer % 0.5f >= 0.25f)
            {
                spriteRenderer.sprite = sprite2;
            }
        }
        else if (horizontal < 0)
        {
            //Goes left
            if (hit.collider == null)
            {
                spriteRenderer.sprite = sprite6;
            }
            else if (animTimer % 0.5f < 0.25f)
            {
                spriteRenderer.sprite = sprite4;
            }
            else if (animTimer % 0.5f >= 0.25f)
            {
                spriteRenderer.sprite = sprite5;
            }
        }
    }

    //Located in Update()
    private void ErrorMessage()
    {
        if (fuel >= 1)
        {
            errorMessage.text = "";
        }
    }

    //Located in Update()
    private void ToggleEscapeUI()
    {
        if (Input.GetButtonUp("Escape"))
        {
            canvas0.enabled = !canvas0.enabled;
            canvas1.enabled = !canvas0.enabled;
            timer.enabled = !timer.enabled;
            isOnFuel = !isOnFuel;
            escaped = !escaped;
        }

        if (!escaped)
        {
            //Sprint
            MovementSpeed();

            //Zoom
            Zoom();

            // Checks if you are on ground
            if (hit.collider != null)
            {
                // fallsound
                if (fallSound)
                {
                    soundBox.clip = jumpLandingSound;
                    soundBox.Play();
                    fallSound = false;
                }

                Jump();

            }
            else
            {
                fallSound = true;
            }

            //Rounds down the fuel level
            fuelPercentage = (int)Mathf.Floor(fuel);
            fuelMessage.text = fuelPercentage.ToString("00");
            
            // Adds fuel if you are on a fuelpad
            if (isOnFuel && fuel < 10)
            {
                fuel += Time.deltaTime;
            }
        }
    }
    private void MovementSpeed()
    {
        //Particles
        if (Input.GetButtonDown("Shift") && fuel > 0)
        {
            particleSystem.Play();
        }
        else if (Input.GetButtonUp("Shift") || fuel <= 0)
        {
            particleSystem.Stop();
        }

        // Try to Sprint
        if (shift && fuel > 0)
        {   
            // SprintBoost
            movementspeed = sprintMovementSpeed;
            spriteSprintModifier = 1.5f;
            if (fuel > 0.25f * Time.deltaTime)
            {
                fuel -= 0.25f * Time.deltaTime;
            }
            else
            {
                fuel = 0;
            }
        }
        else if (shift && fuel <= 0)
        {
            //Give warning / no sprint boost
            errorMessage.text = "no fuel";
        }
        // Walk normal
        else
        {
            movementspeed = walkMovementSpeed;
            spriteSprintModifier = 1f;
        }
    }
    private void Zoom()
    {
        if (zoomOut == 1 && zoomCamera <= 10.5f)
        {
            zoomCamera += 2.5f * Time.deltaTime;
            Camera.main.orthographicSize = zoomCamera;
        }
        else if (zoomIn == 1 && zoomCamera >= -10.5f)
        {
            zoomCamera -= 2.5f * Time.deltaTime;
            Camera.main.orthographicSize = zoomCamera;
        }
    }
    private void Jump()
    {
        // Try to jump with boost
        if (jump && shift && fuel >= 1)
        {
            // Jump with boost
            soundBox.clip = rocketJumpSound;
            soundBox.Play();
            rigidbodyPlayer.AddForce(Vector2.up * jumpHeight * 2);
            fuel -= 1;
        }
        else if (jump && shift && fuel < 1)
        {
            //Give warning / no boost jump
            errorMessage.text = "no fuel";
        }
        // Jump without boost
        else if (jump && !shift)
        {
            soundBox.clip = jumpSound;
            soundBox.Play();
            rigidbodyPlayer.AddForce(Vector2.up * jumpHeight);
        }
    }
}