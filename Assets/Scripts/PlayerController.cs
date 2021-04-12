using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rigidbodyPlayer;

    private float horizontal;
    private bool jump;
    public bool shift;
    public float zoomIn;
    public float zoomOut;
    public float zoomCamera = 10f;

    public float jumpHeight;
    public float movementspeed;
    private float sprintMovementSpeed = 7.5f;
    private float walkMovementSpeed = 5f;


    public LayerMask layerCanJump;
    public Transform characterBody;
    private float jumpFromFeet = 3f;
    public bool isOnFuel = false;

    public Text errorMessage;
    public Text fuelMessage;
    public int fuelPercentage;

    public AudioSource soundBox;
    public AudioClip jumpSound;
    public AudioClip rocketSprintSound;
    public AudioClip jumpLandingSound;
    private bool fallSound = true;
    public float fuel = 0;

    // Start is called before the first frame update
    void Start()
    {
        jumpFromFeet = characterBody.localScale.y / 2 + 0.05f;
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        jump = Input.GetButtonDown("Jump");
        shift = Input.GetButton("Shift");
        zoomIn = Input.GetAxis("ZoomIn");
        zoomOut = Input.GetAxis("ZoomOut");
        Vector2 horizontalspeed = horizontal * movementspeed * Vector2.right;
        rigidbodyPlayer.velocity = new Vector2(horizontalspeed.x, rigidbodyPlayer.velocity.y);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, jumpFromFeet, layerCanJump);

        //Resets error message on fuelpad
        if (fuel >= 1)
        {
            errorMessage.text = "";
        }

        //Sprint

        // SprintBoost
        if (shift && fuel > 0)
        {
            movementspeed = sprintMovementSpeed;
            fuel -= 0.25f * Time.deltaTime;
        }
        // Try to Sprint
        else if (shift && fuel <= 0)
        {
            errorMessage.text = "no fuel";
        }
        // Walk normal
        else
        {
            movementspeed = walkMovementSpeed;
        }

        //Zoom
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

            //Jump

            // Jump with boost
            if (jump && shift && fuel >= 1)
            {
                soundBox.clip = rocketSprintSound;
                soundBox.Play();
                rigidbodyPlayer.AddForce(Vector2.up * jumpHeight * 2);
                fuel -= 1;
            }
            // Try to jump with boost
            else if (jump && shift && fuel < 1)
            {
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
        if (hit.collider == null)
        {
            fallSound = true;
        }
        fuelPercentage = (int)Mathf.Floor(fuel);
        fuelMessage.text = fuelPercentage.ToString("00");
        if (isOnFuel && fuel < 10)
        {
            fuel += Time.deltaTime;
        }
    }
}