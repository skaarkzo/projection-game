using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class AyanSorcererController : MainController
{

    // Field instantiation.
    public float hoverSpeed = 3f;
    public float hoverHeight = 0.1f;

    public float walkSpeed;
    public float flySpeed;

    public static bool isWalking;
    public static bool isFlying;
    public static bool isAttacking;
    public bool allowHover;

    // Update is called once per frame
    void Update()
    {

        CursorLock();
        GroundedCheck();

        Move();
        Hover();
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

            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                Attack();
            }

            if (Input.GetKeyDown("q"))
            {
                Heal();
            }

            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                Attack2();
            }
        }

        controller.Move(direction.normalized * moveSpeed * Time.deltaTime);

    }

    private void Hover()
    {
        if (allowHover == true)
        {
            // Calculate new y-position of the player.
            float newY = Mathf.Sin(Time.time * hoverSpeed) * hoverHeight + pos.y;

            // Set the player's new y-position.
            transform.position = new Vector3(transform.position.x, newY, transform.position.z);
        } 
    }

    private void FloatIdle()
    {
        moveSpeed = 0;

        isIdle = true;
        isWalking = false;
        isFlying = false;

        if (isAttacking == true)
        {
            allowHover = false;
        }

        else
        {
            allowHover = true;
        }

        this.anim.SetLayerWeight(1, 1);
        this.anim.SetBool("Walk", false);
        this.anim.SetBool("IdleToFloatMove", false);
        this.anim.SetBool("WalkToFloatMove", false);

    }

    private new void Walk()
    {
        isIdle = false;
        isWalking = true;
        isFlying = false;
        allowHover = false;

        moveSpeed = walkSpeed;

        CameraMovement();

        this.anim.SetBool("Walk", true);
        this.anim.SetBool("WalkToFloatMove", false);
        this.anim.SetLayerWeight(1, 0);
    }

    private void Fly()
    {
        isIdle = false;
        isFlying = true;
        allowHover = true;

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

    private async void Attack()
    {
        isIdle = false;
        isAttacking = true;
        isFlying = false;
        allowHover = false;

        if (isWalking == false)
        {
            this.anim.SetTrigger("Attack1");
            this.anim.SetInteger("SkillNumber", 0);
            controller.enabled = false;
        }     

        await Task.Delay(2000);

        isAttacking = false;
        controller.enabled = true;

    }

    private async void Heal()
    {
        isIdle = false;
        isAttacking = false;
        isFlying = false;
        allowHover = false;

        if (isWalking == false)
        {
            this.anim.SetTrigger("Heal");
            this.anim.SetInteger("SkillNumber", 1);
            controller.enabled = false;
        }

        await Task.Delay(2000);

        controller.enabled = true;
    }

    private async void Attack2()
    {
        isIdle = false;
        isAttacking = true;
        isFlying = false;
        allowHover = false;

        if (isWalking == false)
        {
            this.anim.SetTrigger("Attack2");
            this.anim.SetInteger("SkillNumber", 2);
            controller.enabled = false;
        }

        await Task.Delay(2000);

        isAttacking = false;
        controller.enabled = true;
    }
}
