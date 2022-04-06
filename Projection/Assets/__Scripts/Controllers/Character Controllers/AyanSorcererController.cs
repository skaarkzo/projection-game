using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class AyanSorcererController : MainController
{

    // Field instantiation.
    public float hoverSpeed = 3f;
    public float hoverHeight = 0.1f;
    public float flySpeed;

    public static bool isWalking;
    public static bool isFlying;
    public static bool isAttacking;
    private bool allowHover;

    [HideInInspector] public Vector3 pos;
    public override void Start()
    {
        pos = transform.position;
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        // Call the CursorLock and GroundedCheck functions from the base class.
        CursorLock();
        GroundedCheck();

        // Allow the player to move and hover up and down
        Move();
        Hover();
    }

    // Control all of the player movements
    private void Move()
    {
        // Set the y-velocity to -2 so their y-value doesn't fluctuate from the ground check.
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        // Call the InputManager function from the base class.
        InputManager();

        // Control all player movements
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

        // Move the player controller in the direction specified from the inputs and according the move speed.
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

    // Character aniimation while idling
    private void FloatIdle()
    {
        moveSpeed = 0;

        isIdle = true;
        isWalking = false;
        isFlying = false;

        // Only hover when not attacking
        if (isAttacking == true)
        {
            allowHover = false;
        }

        else
        {
            allowHover = true;
        }

        // Chang animation to idling
        this.anim.SetLayerWeight(1, 1);
        this.anim.SetBool("Walk", false);
        this.anim.SetBool("IdleToFloatMove", false);
        this.anim.SetBool("WalkToFloatMove", false);

    }

    // Character animation while walking
    private new void Walk()
    {
        // Set booleans
        isIdle = false;
        isWalking = true;
        isFlying = false;
        allowHover = false;

        // Set the movement speed to walking speed
        moveSpeed = walkSpeed;

        PlayerCamera();

        // Set animations
        this.anim.SetBool("Walk", true);
        this.anim.SetBool("WalkToFloatMove", false);
        this.anim.SetLayerWeight(1, 0);
    }

    // Character animation while sprinting (flying)
    private void Fly()
    {
        // Set booleans
        isIdle = false;
        isFlying = true;
        allowHover = true;

        // Set the movement speed to the faster flying speed
        moveSpeed = flySpeed;

        PlayerCamera();

        this.anim.SetLayerWeight(1, 0);
        this.anim.SetLayerWeight(2, 1);

        // Set animations
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

    // Attack animation
    private async void Attack()
    {
        // Set booleans
        isIdle = false;
        isAttacking = true;
        isFlying = false;
        allowHover = false;

        // Set animations
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

    // Healing ability
    private async void Heal()
    {
        // Set booleans
        isIdle = false;
        isAttacking = false;
        isFlying = false;
        allowHover = false;

        // Set animations
        if (isWalking == false)
        {
            this.anim.SetTrigger("Heal");
            this.anim.SetInteger("SkillNumber", 1);
            controller.enabled = false;
        }

        await Task.Delay(2000);

        controller.enabled = true;
    }

    // Secondary attack
    private async void Attack2()
    {

        // Set booleans
        isIdle = false;
        isAttacking = true;
        isFlying = false;
        allowHover = false;

        // Set animation
        if (isWalking == false)
        {
            this.anim.SetTrigger("Attack2");
            this.anim.SetInteger("SkillNumber", 2);
            controller.enabled = false;
        }

        await Task.Delay(2000);

        isAttacking = false;

        // Disable controller while attacking
        controller.enabled = true;
    }
}
