using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class AyanMainController : MainController
{
    public float crouchSpeed;

    public GameObject sword;

    private bool isAttacking;
    private bool isCrouching;

    private bool isRolling = false;

    void Update()
    {
        CursorLock();
        GroundedCheck();

        if (look)
        {
            Move();
            Aim();
        }
    }

    public void Move()
    {
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        InputManager();

        if (isGrounded)
        {
            if (moveDirection == Vector3.zero && !Input.GetKey("z"))
            {
                Idle();
            }

            else if (moveDirection == Vector3.zero && Input.GetKey("z"))
            {
                CrouchIdle();
            }

            else if (moveDirection.magnitude >= 0.1f && !Input.GetKey(KeyCode.LeftShift) && !Input.GetKey("z"))
            {
                Walk();
            }

            else if (moveDirection.magnitude >= 0.1f && Input.GetKey(KeyCode.LeftShift))
            {
                Run();
            }

            else if (moveDirection.magnitude >= 0.1f && !Input.GetKey(KeyCode.LeftShift) && Input.GetKey("z"))
            {
                Crouch();
            }

            if (moveDirection.magnitude >= 0 && Input.GetKeyDown(KeyCode.Space))
            {
                Jump();
            }

            if (Input.GetKeyDown("q"))
            {
                Roll();
            }

            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                Attack();
            }
        }

        controller.Move(direction.normalized * moveSpeed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    public override void Idle()
    {
        isIdle = true;
        isCrouching = false;

        moveSpeed = 0;

        this.anim.SetBool("CrouchWalk", false);
        this.anim.SetBool("Crouch", false);

        base.Idle();
    }

    public override void Walk()
    {
        isIdle = false;
        isCrouching = false;

        moveSpeed = walkSpeed;

        CameraMovement();

        this.anim.SetBool("CrouchWalk", false);
        this.anim.SetBool("Crouch", false);
        
        base.Walk();

        if (isRolling == true)
        {
            moveSpeed = 2;
        }
    }

    public override void Run()
    {
        isIdle = false;
        isCrouching = false;

        moveSpeed = runSpeed;

        CameraMovement();

        this.anim.SetBool("CrouchWalk", false);
        this.anim.SetBool("Crouch", false);
        
        base.Run();
    }

    private void Crouch()
    {
        isIdle = false;
        isCrouching = true;

        moveSpeed = crouchSpeed;

        CameraMovement();

        this.anim.SetBool("Crouch", false);
        this.anim.SetBool("CrouchWalk", true); 

    }

    private void CrouchIdle()
    {
        isIdle = true;
        isCrouching = true;

        this.anim.SetBool("Crouch", true);
        this.anim.SetBool("CrouchWalk", false);

        CameraMovement();

        direction = Vector3.zero;
    }

    public override void Jump()
    {
        if (isCrouching == false)
        {
            base.Jump();
        }
    }

    private async void Attack()
    {
        await Task.Delay(300);

        isIdle = false;
        isAttacking = true;

        this.anim.SetInteger("AttackIndex", Random.Range(0, 2));
        this.anim.SetTrigger("Attack");

        controller.enabled = false;

        await Task.Delay(2000);

        controller.enabled = true;
        isAttacking = false;

    }

    private void Aim()
    {
        if (Input.GetKey(KeyCode.Mouse1) && isAttacking == false)
        {
            mainCamera.SetActive(false);
            aimCamera.SetActive(true);
            sword.SetActive(false);

            this.anim.SetBool("Aim", true);
            controller.enabled = false;

        }

        else if (!Input.GetKey(KeyCode.Mouse1) && isAttacking == false)
        {
            mainCamera.SetActive(true);
            aimCamera.SetActive(false);
            sword.SetActive(true);

            this.anim.SetBool("Aim", false);
            controller.enabled = true;
        }
    }

    private void Roll()
    {
        if (isRolling == false)
        {
            this.anim.SetTrigger("Roll");
            isRolling = true;
        }
        
    }

    public void RollingComplete()
    {
        controller.Move(direction.normalized * Time.deltaTime);
        isRolling = false;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);

        if (currentHealth <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
