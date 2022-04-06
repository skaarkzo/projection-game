using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using TMPro;

public class AyanMainController : MainController
{
    // Initialize Fields.
    public float crouchSpeed;

    public GameObject sword;

    private bool isAttacking;
    private bool isCrouching;
    private bool isAiming;

    private bool isRolling = false;

    [Header("References")]
    public Transform playerTransform;
    public Transform shootingPoint;
    public GameObject shootingObject;

    [Header("Throw Settings")]
    public int totalBullets;
    public float shootCooldown;
    public float shootForce;
    public float shootUpwardForce;

    public TextMeshProUGUI missionGUI;

    void Update()
    {
        // Call the CursorLock and GroundedCheck functions from the base class.
        CursorLock();
        GroundedCheck();

        // Allow the player to move and aim if they are inside the game.
        if (look)
        {
            Move();
            Aim();
        }
    }

    // Control all of the player movements.
    public void Move()
    {
        // Set the y-velocity to -2 so their y-value doesn't fluctuate from the ground check.
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        // Call the InputManager function from the base class.
        InputManager();

        // Control all inputs to move the player.
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

        // Move the player controller in the direction specified from the inputs and according the move speed.
        controller.Move(direction.normalized * moveSpeed * Time.deltaTime);

        // Add gravity to the y-velocity and add gravity to the player.
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    public override void Idle()
    {
        isIdle = true;
        isCrouching = false;

        moveSpeed = 0;

        // Disable crouch animations on idle.
        this.anim.SetBool("CrouchWalk", false);
        this.anim.SetBool("Crouch", false);

        // Call Idle function from the base class.
        base.Idle();
    }

    public override void Walk()
    {
        isIdle = false;
        isCrouching = false;

        // Set the moveSpeed to the walkSpeed.
        moveSpeed = walkSpeed;

        // Call the PlayerCamera function from the base class.
        PlayerCamera();

        // Disable crouch animations on normal walk.
        this.anim.SetBool("CrouchWalk", false);
        this.anim.SetBool("Crouch", false);
        
        // Call Walk function from the base class.
        base.Walk();
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

        // Set the moveSpeed to the crouchSpeed.
        moveSpeed = crouchSpeed;

        PlayerCamera();

        // Set the Idle crouch to false but the crouch walk to true.
        this.anim.SetBool("Crouch", false);
        this.anim.SetBool("CrouchWalk", true); 
    }

    private void CrouchIdle()
    {
        isIdle = true;
        isCrouching = true;

        // Set the Idle crouch to true but the crouch walk to false.
        this.anim.SetBool("Crouch", true);
        this.anim.SetBool("CrouchWalk", false);

        PlayerCamera();

        // Prevents player from transforming position when Idle.
        direction = Vector3.zero;
    }

    public override void Jump()
    {
        // If the player is not crouching, allow the player to jump by calling the Jump function from the base class.
        if (isCrouching == false)
        {
            base.Jump();
        }
    }

    private async void Attack()
    {
        // Delay the attack by 0.3s.
        await Task.Delay(300);

        isIdle = false;
        isAttacking = true;

        // Pick a random attack and then trigger that attack.
        this.anim.SetInteger("AttackIndex", Random.Range(0, 2));
        this.anim.SetTrigger("Attack");

        // Disable the controller while the animation is playing, then reenable it.
        controller.enabled = false;

        await Task.Delay(2000);

        controller.enabled = true;
        isAttacking = false;

    }

    private void Roll()
    {
        // If the player is not already rolling, allow the player to roll.
        if (isRolling == false)
        {
            this.anim.SetTrigger("Roll");
            isRolling = true;
        }
    }


    public void DuringRoll()
    {
        // Move the controller in the direction the player rolls and set isRolling to false.
        controller.Move(direction.normalized * Time.deltaTime);
        isRolling = false;
    }

    public void TakeDamage(int damage)
    {
        // Decrease the damage of the character and set the health bar.
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);

        // Destroy the player if the health is below 0.
        if (currentHealth <= 0)
        {
            Destroy(this.gameObject);
        }

    }

    private void Aim()
    {
        // Is the player holds right click, the camera aims in and aims out when right click is released.
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

        // If the player is aiming in, ready to shoot, and has sufficient bullets, he's allowed to shoot with left click.
        if (isAiming && readyToShoot && totalBullets > 0)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                Shoot();
            }
        }
    }

    public void Shoot()
    {
        readyToShoot = false;

        // Instantiates shooting object
        GameObject projectile = Instantiate(shootingObject, shootingPoint.position, playerTransform.rotation * Quaternion.Euler(90, 0, 0));

        // Gets Rigidbody Component
        Rigidbody projectileRb = projectile.GetComponent<Rigidbody>();

        // Add Force
        Vector3 addForce = transform.forward * shootForce + transform.up * shootUpwardForce;

        projectileRb.AddForce(addForce, ForceMode.Impulse);

        totalBullets--;

        // Implement shoot Cooldown
        Invoke(nameof(ResetShoot), shootCooldown);
    }

    private void ResetShoot()
    {
        // Set readyToShoot to true.
        readyToShoot = true;
    }

    public void incrementMission(int i)
    {
        missionGUI.SetText("Missions Completed: " + i + "/4");
    }
}