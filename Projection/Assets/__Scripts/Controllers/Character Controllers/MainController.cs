using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainController : MonoBehaviour
{
    [HideInInspector] public float moveSpeed;

    [HideInInspector] public Vector3 moveDirection;
    [HideInInspector] public Vector3 direction;

    public CharacterController controller;
    public Transform cam;
    [HideInInspector] public Animator anim;  

    public GameObject mainCamera;
    public GameObject aimCamera;

    [HideInInspector] public float moveZ;
    [HideInInspector] public float moveX;

    [HideInInspector] public Vector3 pos;

    private float targetAngle;
    private float angle;

    public float turnSmoothTime = 0.1f;
    private float turnSmoothVelocity;

    public Transform groundCheck;
    public float groundCheckDistance;
    public LayerMask groundMask;
    public bool isGrounded;

    [HideInInspector] public Vector3 velocity;

    public float gravity = -9.81f;

    public bool look = true;
    public bool isIdle;

    public static bool lookDirectionLock = false;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponentInChildren<Animator>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        pos = transform.position;
    }

    public void InputManager()
    {
        moveZ = Input.GetAxis("Vertical");
        moveX = Input.GetAxis("Horizontal");

        moveDirection = new Vector3(moveX, 0, moveZ).normalized;
    }

    public void CursorLock()
    {
        if (look && Input.GetKeyDown(KeyCode.Escape))
        {
            look = false;
            anim.Rebind();
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        else if (Input.GetMouseButtonDown(0) && !look)
        {
            look = true;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    public void GroundedCheck()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckDistance, groundMask);
    }

    public void CameraMovement()
    {
        targetAngle = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
        angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);

        if (lookDirectionLock == false)
        {
            transform.rotation = Quaternion.Euler(0, angle, 0);
            direction = Quaternion.Euler(0, targetAngle, 0) * Vector3.forward;
        }
        
    }
}
