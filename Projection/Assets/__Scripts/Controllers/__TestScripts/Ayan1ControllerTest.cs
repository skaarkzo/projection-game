using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using System.Threading.Tasks;

public class Ayan1ControllerTest : MonoBehaviour
{

    public float moveSpeed;
    public float walkSpeed;
    public float runSpeed;

    public float ySpeed;

    private Vector3 moveDirection;

    public bool isGrounded;

    public float groundCheckDistance;
    public LayerMask groundMask;
    public float gravity;

    private CharacterController controller;
    private Animator anim;

    private float moveZ;
    private float moveX;

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

        moveZ = Input.GetAxis("Vertical");
        moveX = Input.GetAxis("Horizontal");

        //if (isGrounded && velocity.y < 0)
        //{
        //    velocity.y = -2f;
        //}

        moveDirection = new Vector3(moveX, 0, moveZ);
        moveDirection = transform.TransformDirection(moveDirection);

        if (isGrounded)
        {
            // Walk
            if (moveDirection != Vector3.zero && !Input.GetKey(KeyCode.LeftShift))
            {
                Walk();
            }

            // Run
            else if (moveDirection != Vector3.zero && Input.GetKey(KeyCode.LeftShift))
            {
                Run();
            }

            // Idle
            else if (moveDirection == Vector3.zero)
            {
                Idle();
            }

            moveDirection *= moveSpeed;
        }


        controller.Move(moveDirection * Time.deltaTime);

        //ySpeed += gravity * Time.deltaTime;

        //if (isGrounded)
        //{

        //    ySpeed = -2f;

        //    if (Input.GetKey(KeyCode.Space))
        //    {
        //        ySpeed = jumpSpeed;
        //    }
        //}

        //Jump();

        //controller.Move(velocity * Time.deltaTime);
    }

    private void Idle()
    {
        anim.SetFloat("Vertical", 0, 0.1f, Time.deltaTime);
        anim.SetFloat("Horizontal", 0, 0.1f, Time.deltaTime);
    }



    private void Walk()
    {

        moveSpeed = walkSpeed;

        this.anim.SetFloat("Vertical", moveZ / 2, 0.1f, Time.deltaTime);
        this.anim.SetFloat("Horizontal", moveX / 2, 0.1f, Time.deltaTime);

        if (Input.GetKey("w") && (Input.GetKey("d") || Input.GetKey("a")))
        {
            moveSpeed = walkSpeed - 1;
        }

        if (Input.GetKey("s") && (Input.GetKey("d") || Input.GetKey("a")))
        {
            moveSpeed = walkSpeed - 3;
        }
    }

    private void Run()
    {
        moveSpeed = runSpeed;

        this.anim.SetFloat("Vertical", moveZ, 0.1f, Time.deltaTime);
        this.anim.SetFloat("Horizontal", moveX, 0.1f, Time.deltaTime);

        if ((Input.GetKey("w") && Input.GetKey(KeyCode.LeftShift)) && (Input.GetKey("d") || Input.GetKey("a")))
        {
            moveSpeed = runSpeed - 2;
        }

        if ((Input.GetKey("s") && Input.GetKey(KeyCode.LeftShift)) && (Input.GetKey("d") || Input.GetKey("a")))
        {
            moveSpeed = runSpeed - 4;
        }
    }

    //private void Jump()
    //{
    //    velocity = moveDirection * moveSpeed;

    //    velocity.y = ySpeed;
    //    anim.SetBool("Jump", !isGrounded);
    //}
}
