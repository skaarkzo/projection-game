using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class Ayan1ControllerShooting : MainController
{
    public float crouchSpeed;
    public GameObject sword;
    private bool isAttacking;
    private bool isCrouching;
    private bool isAiming;

    [Header("References")]
    public Transform playerTransform;
    public Transform attackPoint;
    public GameObject throwingObject;

    [Header("Throw Settings")]
    public int totalThrows;
    public float throwCooldown;
    public float throwForce;
    public float throwUpwardForce;

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

        PlayerCamera();

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

        PlayerCamera();

        this.anim.SetBool("CrouchWalk", false);
        this.anim.SetBool("Crouch", false);

        base.Run();
    }

    private void Crouch()
    {
        isIdle = false;
        isCrouching = true;

        moveSpeed = crouchSpeed;

        PlayerCamera();

        this.anim.SetBool("Crouch", false);
        this.anim.SetBool("CrouchWalk", true);

    }

    private void CrouchIdle()
    {
        isIdle = true;
        isCrouching = true;

        this.anim.SetBool("Crouch", true);
        this.anim.SetBool("CrouchWalk", false);

        PlayerCamera();

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
            isAiming = true;
        }

        else if (!Input.GetKey(KeyCode.Mouse1) && isAttacking == false)
        {
            mainCamera.SetActive(true);
            aimCamera.SetActive(false);
            sword.SetActive(true);

            this.anim.SetBool("Aim", false);
            controller.enabled = true;
            isAiming = false;
        }

        if (isAiming && readyToThrow && totalThrows > 0)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                Throw();
            }
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

    public void Throw()
    {
            readyToThrow = false;

            // Instantiates throwing object
            GameObject projectile = Instantiate(throwingObject, attackPoint.position, playerTransform.rotation * Quaternion.Euler(90, 0, 0));

            // Gets Rigidbody Component
            Rigidbody projectileRb = projectile.GetComponent<Rigidbody>();

            // Add Force
            Vector3 addForce = transform.forward * throwForce + transform.up * throwUpwardForce;

            projectileRb.AddForce(addForce, ForceMode.Impulse);

            totalThrows--;

            // Implement Throw Cooldown
            Invoke(nameof(ResetThrow), throwCooldown);
    }

    private void ResetThrow()
    {
        readyToThrow = true;
    }

    public void DuringThrow()
    {
        controller.Move(direction.normalized * 0.5f * Time.deltaTime);
        readyToThrow = false;
    }
}
