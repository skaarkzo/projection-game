using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpTest : MonoBehaviour
{

    private Animator anim;
    private Rigidbody rb;

    public bool isGrounded;
    public float groundCheckDistance;
    public LayerMask groundMask;

    private Vector3 velocity;

    private CharacterController controller;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody>();
        controller = GetComponent<CharacterController>();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Grounded();
        Jump();
    }

    void Jump()
    {
        if ((Input.GetKeyDown(KeyCode.Space)) && isGrounded)
        {
            this.rb.AddForce(Vector3.up * 3, ForceMode.Impulse);
        }
    }

    void Grounded()
    {
        if (Physics.CheckSphere(transform.position, groundCheckDistance, groundMask))
        {
            isGrounded = true;
        }

        else
        {
            isGrounded = false;
        }

        anim.SetBool("Jump", !isGrounded);
    }
}
