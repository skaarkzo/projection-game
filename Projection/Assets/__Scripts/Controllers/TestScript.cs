using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    public Animator anim;
    private Rigidbody rb;
    public LayerMask layerMask;
    public bool grounded;

    public float walkSpeed;


    // Start is called before the first frame update
    void Start()
    {
        this.rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        //Grounded();
        //Jump();
        Move();
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && this.grounded)
        {
            this.rb.AddForce(Vector3.up * 4, ForceMode.Impulse);
        }
    }

    private void Grounded()
    {
        this.grounded = Physics.CheckSphere(this.transform.position, 0, layerMask);
        this.anim.SetBool("Jump", !this.grounded);
    }

    private void Move()
    {
        bool isGrounded = Physics.CheckSphere(transform.position, 0.5f, layerMask);

        if (isGrounded)
        {
            float verticalAxis = Input.GetAxis("Vertical");
            float horizontalAxis = Input.GetAxis("Horizontal");

            Vector3 movement = this.transform.forward * verticalAxis + this.transform.right * horizontalAxis;
            movement.Normalize();

            this.transform.position += movement * walkSpeed;

            this.anim.SetFloat("Vertical", verticalAxis, 0.1f, Time.deltaTime);
            this.anim.SetFloat("Horizontal", horizontalAxis, 0.1f, Time.deltaTime);
        } 
    }
}
