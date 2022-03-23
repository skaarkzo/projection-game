using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotControl : MonoBehaviour
{
    private GameObject playerObject;
    private GameObject robotObject;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        playerObject = GameObject.Find("PlayerChar");
        robotObject = this.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(playerObject.transform.position, robotObject.transform.position);
        Debug.Log(distance);
        anim = robotObject.GetComponent<Animator>();

        if (distance > 10)
        {
            anim.SetBool("Walk", false);
        }
        else if (distance <= 10 && distance > 1.5)
        {
            anim.SetBool("Attack", false);
            anim.SetBool("Walk", true);
        }
        else if (distance <= 1.5)
        {
            anim.SetBool("Attack", true);
        }
    }
}
