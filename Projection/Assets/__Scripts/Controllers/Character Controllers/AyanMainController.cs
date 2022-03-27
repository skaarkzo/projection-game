using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class AyanMainController : MainController
{
    public float walkSpeed;
    public float runSpeed;
    public float crouchSpeed;
    public float jumpHeight;

    public GameObject sword;

    private bool isAttacking;

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

    private void Idle()
    {
        moveSpeed = 0;

        isIdle = true;

        this.anim.SetBool("CrouchWalk", false);
        this.anim.SetBool("Crouch", false);

        this.anim.SetFloat("Vertical", 0, 0.1f, Time.deltaTime);
        this.anim.SetFloat("Horizontal", 0, 0.1f, Time.deltaTime);
    }

    private void Walk()
    {
        isIdle = false;

        moveSpeed = walkSpeed;

        CameraMovement();

        this.anim.SetBool("CrouchWalk", false);
        this.anim.SetBool("Crouch", false);
        this.anim.SetFloat("Vertical", moveZ / 2, 0.1f, Time.deltaTime);
        this.anim.SetFloat("Horizontal", moveX / 2, 0.1f, Time.deltaTime);

        if (isRolling == true)
        {
            moveSpeed = 2;
        }
    }

    private void Run()
    {
        isIdle = false;

        moveSpeed = runSpeed;

        CameraMovement();

        this.anim.SetBool("CrouchWalk", false);
        this.anim.SetBool("Crouch", false);
        this.anim.SetFloat("Vertical", moveZ, 0.1f, Time.deltaTime);
        this.anim.SetFloat("Horizontal", moveX, 0.1f, Time.deltaTime);

        if (isRolling == true)
        {
            moveSpeed = 2;
        }
    }

    private void Crouch()
    {
        isIdle = false;

        moveSpeed = crouchSpeed;

        CameraMovement();

        this.anim.SetBool("Crouch", false);
        this.anim.SetBool("CrouchWalk", true); 

    }

    private void CrouchIdle()
    {
        isIdle = true;

        this.anim.SetBool("Crouch", true);
        this.anim.SetBool("CrouchWalk", false);

        CameraMovement();

        direction = Vector3.zero;
    }

    private async void Jump()
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
}
