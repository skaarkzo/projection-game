using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class MainController : MonoBehaviour
{

    // Initialize Fields.

    [HideInInspector] public float moveSpeed;

    [HideInInspector] public Vector3 moveDirection;
    [HideInInspector] public Vector3 direction;

    [Header("Main Reference Settings")]
    public CharacterController controller;
    public Transform cam;
    [HideInInspector] public Animator anim;  

    public GameObject mainCamera;
    public GameObject aimCamera;

    [HideInInspector] public float moveZ;
    [HideInInspector] public float moveX;

    private float targetAngle;
    private float angle;

    [Header("Player Turning Smooth Time Setting")]
    public float turnSmoothTime = 0.1f;
    private float turnSmoothVelocity;

    [Header("Ground Check Settings")]
    public Transform groundCheck;
    public float groundCheckDistance;
    public LayerMask groundMask;
    [HideInInspector] public bool isGrounded;

    [HideInInspector] public Vector3 velocity;

    [HideInInspector] public bool look = true;
    [HideInInspector] public bool isIdle;

    [HideInInspector] public bool readyToThrow;

    [Header("Player Health Settings")]
    public int maxHealth = 100;
    [HideInInspector] public int currentHealth;
    public HealthBar healthBar;

    [Header("Movement and Gravity Settings")]
    public float walkSpeed;
    public float runSpeed;
    public float jumpHeight;
    public float gravity = -9.81f;

    [HideInInspector] public static bool lookDirectionLock = false;

    // Start is called before the first frame update
    public virtual void Start()
    {
        // Set the readyToThrow to true for the third character to be able to throw knives.
        readyToThrow = true;

        // Initialize controller and animator.
        controller = GetComponent<CharacterController>();
        anim = GetComponentInChildren<Animator>();

        // Hide the cursor when the game starts.
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Set the player's health.
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    // Function to get the x and z axes for the to control the movement.
    public void InputManager()
    {
        moveZ = Input.GetAxis("Vertical");
        moveX = Input.GetAxis("Horizontal");

        // To move the player in the in the direction according to the key pressed.
        moveDirection = new Vector3(moveX, 0, moveZ).normalized;
    }

    // Function to unlock or lock the cursor.
    public void CursorLock()
    {
        // Unlocks the cursor when the escape key is pressed.
        if (look && Input.GetKeyDown(KeyCode.Escape))
        {
            look = false;
            //anim.Rebind();
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        // Lock the cursor when the mouse is clicked inside the game.
        else if (Input.GetMouseButtonDown(0) && !look)
        {
            look = true;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    // Function for ground detection that creates a sphere at the player's feet and can be edited with the ground mask and ground check distance (diameter).
    public void GroundedCheck()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckDistance, groundMask);
    }

    // Function to move the player in the direction the camera is facing.
    public void PlayerCamera()
    {
        // Find how much the player should rotate on the y-axis according to the camera.
        targetAngle = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg + cam.eulerAngles.y;

        // Make the rotation of the player smooth by setting a smooth time to the target angle that can be edited.
        angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);

        if (lookDirectionLock == false)
        {
            // Rotate the player along the y-axis using the angle found.
            transform.rotation = Quaternion.Euler(0, angle, 0);

            // Apply the direction the player must move in after rotating the player.
            direction = Quaternion.Euler(0, targetAngle, 0) * Vector3.forward;
        }  
    }

    // Function to make the player idle.
    public virtual void Idle()
    {
        // Set the animator float values to 0 to make the player idle.
        this.anim.SetFloat("Vertical", 0, 0.1f, Time.deltaTime);
        this.anim.SetFloat("Horizontal", 0, 0.1f, Time.deltaTime);
    }

    // Function to make the player walk.
    public virtual void Walk()
    {
        // Set the animator float values to half the input values to make the player walk.
        this.anim.SetFloat("Vertical", moveZ / 2, 0.1f, Time.deltaTime);
        this.anim.SetFloat("Horizontal", moveX / 2, 0.1f, Time.deltaTime);
    }

    // Function to make the player run (character 1 and 3).
    public virtual void Run()
    {
        // Set the animator float values to the input values to make the player run.
        this.anim.SetFloat("Vertical", moveZ, 0.1f, Time.deltaTime);
        this.anim.SetFloat("Horizontal", moveX, 0.1f, Time.deltaTime);
    }

    // Function to make the player jump (character 1 and 3).
    public virtual async void Jump()
    {
        // If the player is idle, play the idle jump animation, otherwise play the moving jump animation. Wait 0.5s to match the delay with the controller.
        if (isIdle)
        {
            this.anim.SetTrigger("JumpIdle");
            await Task.Delay(500);
        }

        else
        {
            this.anim.SetTrigger("JumpMove");
        }

        // To make the player jump, set the y velocity of the player to the following equation.
        velocity.y = Mathf.Sqrt(jumpHeight * -6 * gravity);
    }
}
