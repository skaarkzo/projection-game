using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ayan1ControllerTest : MonoBehaviour
{

    public Interactable focus;
    public float moveSpeed;
    public float walkSpeed;
    public float runSpeed;

    private Vector3 velocity;
    public float jumpHeight;

    public int maxHealth = 100;
    public int currentHealth;

    private Vector3 moveDirection;

    public bool isGrounded;

    public float groundCheckDistance;
    public LayerMask groundMask;
    public float gravity;

    private CharacterController controller;
    private Animator anim;

    private float moveZ;
    private float moveX;

    public bool isIdle;
    public bool isMoving;

    public HealthBar healthBar;

    Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        cam = Camera.main;
        controller = GetComponent<CharacterController>();
        anim = GetComponentInChildren<Animator>();

    }

    // Update is called once per frame
    void Update()
    {

        Move();

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                //
            }

            StabRight();
        }

        else if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                Interactable interactable = hit.collider.GetComponent<Interactable>();
                if (interactable != null)
                {
                    SetFocus(interactable);
                }
            }

            StabLeft();
        }

        if (Input.GetKeyDown(KeyCode.Mouse0) && isIdle == true)
        {
            StabRightIdle();
        }

        else if (Input.GetKeyDown(KeyCode.Mouse1) && isIdle == true)
        {
            StabLeftIdle();
        }

    }

    void SetFocus(Interactable newFocus)
    {
        focus = newFocus;
    }

    private void Move()
    {

        isGrounded = Physics.CheckSphere(transform.position, groundCheckDistance, groundMask);

        moveZ = Input.GetAxis("Vertical");
        moveX = Input.GetAxis("Horizontal");

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

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

            if (Input.GetKeyDown(KeyCode.Space))
            {
                Jump();

            }

            else if (!Input.GetKeyDown(KeyCode.Space))
            {
                anim.SetBool("Jump", false);
            }
        }


        controller.Move(moveDirection * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

    }

    private void Idle()
    {
        anim.SetFloat("Vertical", 0, 0.1f, Time.deltaTime);
        anim.SetFloat("Horizontal", 0, 0.1f, Time.deltaTime);
        isIdle = true;
        isMoving = false;

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

        isIdle = false;
        isMoving = true;

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

        isIdle = false;
        isMoving = true;

    }

    private void Jump()
    {

        anim.SetBool("Jump", true);
        velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);

    }

    private void StabRight()
    {

        anim.SetTrigger("StabRight");

    }

    private void StabLeft()
    {

        anim.SetTrigger("StabLeft");

    }

    private void StabRightIdle()
    {

        anim.SetTrigger("StabRightIdle");

    }

    private void StabLeftIdle()
    {

        anim.SetTrigger("StabLeftIdle");

    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);

        if(currentHealth <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    public void DamageEnemy()
    {
        Debug.Log("Damaged Enemy");
    }


}
