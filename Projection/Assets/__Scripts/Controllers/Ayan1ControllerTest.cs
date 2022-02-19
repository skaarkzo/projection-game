using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ayan1ControllerTest : MonoBehaviour
{

    public float moveSpeed;
    public float walkSpeed;
    public float runSpeed;

    public float jumpHeight;

    private Vector3 moveDirection;
    private Vector3 velocity;

    public bool isGrounded;
    public float groundCheckDistance;
    public LayerMask groundMask;
    public float gravity;

    private CharacterController controller;
    private Animator anim;

    public bool jumping;
    public bool falling;
    //public bool grounded;
    

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        isGrounded = Physics.CheckSphere(transform.position, groundCheckDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float moveZ = Input.GetAxis("Vertical");
        float moveX = Input.GetAxis("Horizontal");

        moveDirection = new Vector3(moveX, 0, moveZ);
        moveDirection = transform.TransformDirection(moveDirection);

        if (isGrounded)
        {
            // Walk
            if (moveDirection != Vector3.zero && !Input.GetKey(KeyCode.LeftShift))
            {
                moveSpeed = walkSpeed;

                this.anim.SetFloat("Vertical", moveZ, 0.1f, Time.deltaTime);
                this.anim.SetFloat("Horizontal", moveX, 0.1f, Time.deltaTime);
            }


            // Run
            //else if (moveDirection != Vector3.zero && Input.GetKey(KeyCode.LeftShift))
            //{
            //    Run();
            //}


            // Idle
            else if (moveDirection == Vector3.zero)
            {
                anim.SetFloat("Vertical", 0, 0.1f, Time.deltaTime);
                anim.SetFloat("Horizontal", 0, 0.1f, Time.deltaTime);
            }

            moveDirection *= moveSpeed;
        }

        controller.Move(moveDirection * Time.deltaTime);

        //velocity.y += gravity * Time.deltaTime;
        //controller.Move(velocity * Time.deltaTime);
    }

    private void Idle()
    {
        anim.SetFloat("Speed", 0, 0.1f, Time.deltaTime);
    }

    private void Walk()
    {
        moveSpeed = walkSpeed;

        float horizontalAxis = Input.GetAxis("Horizontal");
        this.anim.SetFloat("Horizontal", horizontalAxis, 0.1f, Time.deltaTime);
    }

    private void Run()
    {
        moveSpeed = runSpeed;
        anim.SetFloat("Speed", 1, 0.1f, Time.deltaTime);
    }
}
