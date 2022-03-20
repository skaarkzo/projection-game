using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AyanMainController : MonoBehaviour
{

    public float moveSpeed;
    public float walkSpeed;
    public float runSpeed;
    public float rollSpeed;

    private Vector3 moveDirection;
    private Vector3 direction;

    public CharacterController controller;
    private Animator anim;
    public Transform cam;

    private float moveZ;
    private float moveX;

    private float targetAngle;
    private float angle;

    public float turnSmoothTime = 0.1f;
    private float turnSmoothVelocity;

    public Transform groundCheck;
    public float groundCheckDistance;
    public LayerMask groundMask;
    
    public float gravity = -9.81f;
    private Vector3 velocity;

    public bool isGrounded;

    public bool look = true;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponentInChildren<Animator>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (look)
        {
            Move();
        }

        if (look && Input.GetKeyDown(KeyCode.Escape))
        {
            look = false;
            anim.Rebind();
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

    public void Move()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        moveZ = Input.GetAxis("Vertical");
        moveX = Input.GetAxis("Horizontal");

        direction = new Vector3(moveX, 0, moveZ).normalized;

        if (isGrounded)
        {
            if (direction == Vector3.zero)
            {
                Idle();
            }

            else if (direction.magnitude >= 0.1f && !Input.GetKey(KeyCode.LeftShift))
            {
                Walk();
            }

            else if (direction.magnitude >= 0.1f && Input.GetKey(KeyCode.LeftShift))
            {
                Run();
            }

            if (Input.GetKeyDown("q") && (direction.magnitude == 0 || direction.magnitude >= 0.1f))
            {
                Roll();
            }

        }

        controller.Move(moveDirection.normalized * moveSpeed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

    }

    private void CameraMovement()
    {
        targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
        angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
        transform.rotation = Quaternion.Euler(0, angle, 0);

        moveDirection = Quaternion.Euler(0, targetAngle, 0) * Vector3.forward;
    }

    private void Idle()
    {
        this.anim.SetFloat("Vertical", 0, 0.1f, Time.deltaTime);
        this.anim.SetFloat("Horizontal", 0, 0.1f, Time.deltaTime);

        moveDirection = Vector3.zero;
    }

    private void Walk()
    {
        moveSpeed = walkSpeed;

        CameraMovement();

        this.anim.SetFloat("Vertical", moveZ / 2, 0.1f, Time.deltaTime);
        this.anim.SetFloat("Horizontal", moveX / 2, 0.1f, Time.deltaTime);
    }

    private void Run()
    {
        moveSpeed = runSpeed;

        CameraMovement();

        this.anim.SetFloat("Vertical", moveZ, 0.1f, Time.deltaTime);
        this.anim.SetFloat("Horizontal", moveX, 0.1f, Time.deltaTime);
    }

    private void Roll()
    {
        this.anim.SetTrigger("Roll");
    }
}
