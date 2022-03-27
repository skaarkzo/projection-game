using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AyanSorcererController : MainController
{

    // Field instantiation.
    public float hoverSpeed = 3f;
    public float hoverHeight = 0.1f;

    public float walkSpeed;
    public float flySpeed;

    public static bool isWalking;

    // Update is called once per frame
    void Update()
    {

        CursorLock();
        GroundedCheck();

        Move();
    }

    private void Move()
    {
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        InputManager();

        if (isGrounded)
        {
            //if (moveDirection == Vector3.zero && groundIdle == false && Input.GetKeyDown(KeyCode.Space))
            //{
            //    Idle();
            //}

            if (moveDirection == Vector3.zero)
            {
                FloatIdle();
            }

            else if (moveDirection.magnitude >= 0.1f && !Input.GetKey(KeyCode.LeftShift))
            {
                Walk();
            }

            else if (moveDirection.magnitude >= 0.1f && Input.GetKey(KeyCode.LeftShift))
            {
                Fly();
            }
        }

        controller.Move(direction.normalized * moveSpeed * Time.deltaTime);
    }

    //private void Idle()
    //{
    //    moveSpeed = 0;

    //    isIdle = true;
    //    groundIdle = true;

    //    this.anim.SetBool("Move", false);
    //    this.anim.SetBool("FloatIdle", false);

    //}

    private void FloatIdle()
    {
        moveSpeed = 0;

        isIdle = true;
        isWalking = false;

        // Calculate new y-position of the collectible.
        float newY = Mathf.Sin(Time.time * hoverSpeed) * hoverHeight + pos.y;

        // Set the collectible's new y-position.
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);

        this.anim.SetLayerWeight(1, 1);
        this.anim.SetBool("Walk", false);
        this.anim.SetBool("IdleToFloatMove", false);
        this.anim.SetBool("WalkToFloatMove", false);
    }

    private void Walk()
    {
        isIdle = false;
        isWalking = true;

        moveSpeed = walkSpeed;

        CameraMovement();

        this.anim.SetBool("Walk", true);
        this.anim.SetBool("WalkToFloatMove", false);
        this.anim.SetLayerWeight(1, 0);
    }

    private void Fly()
    {
        isIdle = false;

        moveSpeed = flySpeed;

        CameraMovement();

        this.anim.SetLayerWeight(1, 0);
        this.anim.SetLayerWeight(2, 1);

        if (isIdle == false)
        {
            this.anim.SetBool("IdleToFloatMove", true);
        }

        if (isWalking == true)
        {
            this.anim.SetBool("WalkToFloatMove", true);
            this.anim.SetBool("Walk", false);
        }
        
        
    }
}
