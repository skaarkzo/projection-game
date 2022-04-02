using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class MainController : MonoBehaviour
{
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

    [HideInInspector] public Vector3 pos;

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
    void Start()
    {
        readyToThrow = true;

        controller = GetComponent<CharacterController>();
        anim = GetComponentInChildren<Animator>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);

        pos = transform.position;
    }

    public void InputManager()
    {
        moveZ = Input.GetAxis("Vertical");
        moveX = Input.GetAxis("Horizontal");

        moveDirection = new Vector3(moveX, 0, moveZ).normalized;
    }

    public void CursorLock()
    {
        if (look && Input.GetKeyDown(KeyCode.Escape))
        {
            look = false;
            //anim.Rebind();
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        else if (Input.GetMouseButtonDown(0) && !look)
        {
            look = true;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    public void GroundedCheck()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckDistance, groundMask);
    }

    public void CameraMovement()
    {
        targetAngle = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
        angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);

        if (lookDirectionLock == false)
        {
            transform.rotation = Quaternion.Euler(0, angle, 0);
            direction = Quaternion.Euler(0, targetAngle, 0) * Vector3.forward;
        }  
    }

    public virtual void Idle()
    {
        this.anim.SetFloat("Vertical", 0, 0.1f, Time.deltaTime);
        this.anim.SetFloat("Horizontal", 0, 0.1f, Time.deltaTime);
    }

    public virtual void Walk()
    {
        this.anim.SetFloat("Vertical", moveZ / 2, 0.1f, Time.deltaTime);
        this.anim.SetFloat("Horizontal", moveX / 2, 0.1f, Time.deltaTime);
    }

    public virtual void Run()
    {
        this.anim.SetFloat("Vertical", moveZ, 0.1f, Time.deltaTime);
        this.anim.SetFloat("Horizontal", moveX, 0.1f, Time.deltaTime);
    }

    public virtual async void Jump()
    {

        if (isIdle)
        {
            this.anim.SetTrigger("JumpIdle");
            await Task.Delay(500);
        }

        else
        {
            this.anim.SetTrigger("JumpMove");
        }

        velocity.y = Mathf.Sqrt(jumpHeight * -6 * gravity);

    }
}
