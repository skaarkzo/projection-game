using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidingDoor : MonoBehaviour
{

    private Vector3 endpos;
    public float speed = 1.0f;

    private bool moving = false;
    private bool opening = true;
    private Vector3 startPos;
    private float delay = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;

        if (transform.eulerAngles.y == 0)
        {
            if (tag == "Level1LDoor")
            {
                endpos = transform.position + new Vector3(1, 0, 0);
            }
            else if (tag == "Level1RDoor")
            {
                endpos = transform.position + new Vector3(-1, 0, 0);
            }
        }

        if (transform.eulerAngles.y == 90)
        {
            if (tag == "Level1LDoor")
            {
                endpos = transform.position + new Vector3(0, 0, -1);
            }
            else if (tag == "Level1RDoor")
            {
                endpos = transform.position + new Vector3(0, 0, 1);
            }
        }

        startPos = transform.position;

    }

    // Update is called once per frame
    void Update()
    {

        if (moving)
        {
            if (opening)
            {
                MoveDoor(endpos);
            }
            else
            {
                MoveDoor(startPos);
            }
        }
    }

    void MoveDoor(Vector3 goalPos)
    {
        float dist = Vector3.Distance(transform.position, goalPos);
        if(dist > .1f){
            transform.position = Vector3.Lerp(transform.position, goalPos, speed * Time.deltaTime);
        }
        else
        {
            if (opening)
            {
                delay += Time.deltaTime;
                if(delay > 1.5f)
                {
                    opening = false;
                }
            }
            else
            {
                moving = false;
                opening = true;
            }
        }
    }

    public bool Moving
    {
        get { return moving; }
        set { moving = value;}
    }

}
