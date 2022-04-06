using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class AyanTankController : MainController
{
    // Initialize variables
    private bool isSliding;

    [Header("References")]
    public Transform playerTransform;
    public Transform attackPoint;
    public GameObject throwingObject;

    [Header("Throw Settings")]
    public int totalThrows;
    public float throwCooldown;
    public float throwForce;
    public float throwUpwardForce;

    // Update is called once per frame
    void Update()
    {
        // Call the CursorLock and GroundedCheck functions from the base class.
        CursorLock();
        GroundedCheck();

        Move();
    }

    // Allow the player to move
    private void Move()
    {
        // Set the y-velocity to -2 so their y-value doesn't fluctuate from the ground check.
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        // Call the InputManager function from the base class.
        InputManager();

        // Move the player controller in the direction specified from the inputs and according the move speed.
        if (isGrounded)
        {
            if (moveDirection == Vector3.zero)
            {
                Idle();
            }

            else if (moveDirection.magnitude >= 0.1f && !Input.GetKey(KeyCode.LeftShift))
            {
                Walk();
            }

            else if (moveDirection.magnitude >= 0.1f && Input.GetKey(KeyCode.LeftShift))
            {
                Run();
            }

            if (moveDirection.magnitude >= 0 && Input.GetKeyDown(KeyCode.Space))
            {
                Jump();
            }

            if (Input.GetKeyDown("q") && moveDirection.magnitude >= 0.1f && Input.GetKey(KeyCode.LeftShift))
            {
                Slide();
            }

            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                HandCombat();
            }

            if (Input.GetKeyDown(KeyCode.Mouse1) && readyToThrow && totalThrows > 0)
            {
                Throw();
            }
        }

        controller.Move(direction.normalized * moveSpeed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    // Idling animations
    public override void Idle()
    {
        // Set boooleans
        moveSpeed = 0;
        isIdle = true;

        // Set animation
        this.anim.SetBool("Move", false);
    }

    // Walking animation
    public override void Walk()
    {
        // Set booleans
        moveSpeed = walkSpeed;
        isIdle = false;

        PlayerCamera();

        // Set animation
        this.anim.SetBool("Move", true);
        base.Walk();
    }

    // Running animation
    public override void Run()
    {
        // Set booleans
        moveSpeed = runSpeed;
        isIdle = false;

        PlayerCamera();

        // Set animation
        this.anim.SetBool("Move", true);
        base.Run();
    }

    // Sliding animation
    private void Slide()
    {
        isIdle = false;

        // Set animation
        if (isSliding == false)
        {
            this.anim.SetTrigger("Slide");
            isSliding = true;
        }

    }

    // Translate player while sliding
    public void DuringSlide()
    {
        isIdle = false;

        controller.Move(direction.normalized * 2 * Time.deltaTime);
        isSliding = false;
    }

    // Melee combat animation
    private async void HandCombat()
    {
        if (isIdle == true)
        {
            this.anim.SetInteger("HandCombatIndex", Random.Range(0, 7));
            this.anim.SetTrigger("HandCombat");

            controller.enabled = false;
        }
       
        await Task.Delay(1500);

        controller.enabled = true;
    }

    // Method used to generate throwing knives
    public async void Throw()
    {
        if (isIdle == true)
        {

            this.anim.SetTrigger("KnifeThrow");

            await Task.Delay(1000);

            readyToThrow = false;

            // Instantiates throwing object
            GameObject projectile = Instantiate(throwingObject, attackPoint.position, playerTransform.rotation * Quaternion.Euler(90, 0, 0));

            // Gets Rigidbody Component
            Rigidbody projectileRb = projectile.GetComponent<Rigidbody>();

            // Calculate Direction

            Vector3 forceDirection = playerTransform.transform.forward;

            RaycastHit hit;

            if (Physics.Raycast(playerTransform.position, playerTransform.forward, out hit, 500f))
            {
                forceDirection = (hit.point - attackPoint.position).normalized;
            }

            // Add Force
            Vector3 addForce = transform.forward * throwForce + transform.up * throwUpwardForce;

            projectileRb.AddForce(addForce, ForceMode.Impulse);

            totalThrows--;

            // Implement Throw Cooldown
            Invoke(nameof(ResetThrow), throwCooldown);
        }   
    }

    // Ready another knife for throwing
    private void ResetThrow()
    {
        readyToThrow = true;
    }

    // Character moves slightly, cooldown reset
    public void DuringThrow()
    {
        controller.Move(direction.normalized * 0.5f * Time.deltaTime);
        readyToThrow = false;
    }
}
