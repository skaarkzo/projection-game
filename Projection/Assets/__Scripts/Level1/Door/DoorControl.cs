using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorControl : MonoBehaviour
{

    private Vector3 endpos;
    public float speed = 1.0f;

    private bool moving = false;
    private bool opening = true;
    private Vector3 startPos;
    private float delay = 0.0f;

    public bool inCollider = false;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Entered");

        inCollider = true;
        
    }

    private void OnTriggerExit(Collider other)
    {
        inCollider = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        endpos = transform.position + new Vector3(3, 0, 0);

    }

    // Update is called once per frame
    void Update()
    {
        if (inCollider == true)
        {
            if (Input.GetKeyDown("e"))
            {
                Debug.Log("Pressed E");
            }
        }


    }

    public void DoorOperator()
    {

    }
}
