using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using System.Threading.Tasks;

public class Attack : MonoBehaviour
{

    private CharacterController controller;
    private Animator anim;

    public bool isGrounded;
    public float groundCheckDistance;
    public LayerMask groundMask;
    public float gravity;

    private float moveZ;
    private float moveX;

    private Vector3 rollDirection;
    private Vector3 moveDirection;

    public float moveSpeed;
    public float crouchSpeed;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(transform.position, groundCheckDistance, groundMask);
        moveZ = Input.GetAxis("Vertical");
        moveX = Input.GetAxis("Horizontal");

        Roll();
    }

    private async void Roll()
    {
        rollDirection = new Vector3(moveX, 0, moveZ);

            if (Input.GetKey("q"))
            {

                anim.SetTrigger("Roll");
                await Task.Delay(500);

                controller.height = 0.5f;
                controller.center = new Vector3(0, 0.5f, 0);

                controller.Move(rollDirection * Time.deltaTime);

                await Task.Delay(1600);

                controller.height = 1.79f;
                controller.center = new Vector3(0, 1, 0);

            }


    }

    private void Attack3()
    {
        if (Input.GetKey("u"))
        {
            anim.SetTrigger("Attack_3Combo_1");
        }
    }

}
