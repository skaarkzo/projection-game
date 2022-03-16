using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCTrigger : MonoBehaviour
{

    private GameObject triggeringNPC = null;
    private bool triggering;

    private static Animator anim;

    private Vector3 playerPos;
    private Vector3 npcPos;

    private void Update()
    {
        anim = triggeringNPC.GetComponent<Animator>();

        playerPos = GameObject.Find("Player").transform.position;
        npcPos = GameObject.Find(triggeringNPC.name).transform.position;

        float distance = Mathf.Sqrt(Mathf.Pow((playerPos.x - npcPos.x), 2) + Mathf.Pow((playerPos.y - npcPos.y), 2) + Mathf.Pow((playerPos.z - npcPos.z), 2));

        if (triggering)
        {

            Debug.Log("Player is triggering with " + triggeringNPC.name);

            if (distance <= 3.5)
            {
                Debug.Log("Player is within distance");

                if (Input.GetKey("e"))
                {
                    Debug.Log("Player clicked E");
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "BlacksmithNPC")
        {
            triggering = true;
            triggeringNPC = other.gameObject;
            anim = triggeringNPC.GetComponent<Animator>();
            anim.SetTrigger("Wave");
        }

        else if (other.tag == "MerchantNPC")
        {
            triggering = true;
            triggeringNPC = other.gameObject;
            anim = triggeringNPC.GetComponent<Animator>();
            anim.SetTrigger("StandTrigger");
            //anim.SetBool("Sit", false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "BlacksmithNPC")
        {
            triggering = false;
            triggeringNPC = null;
        }

        else if (other.tag == "MerchantNPC")
        {
            triggering = false;
            triggeringNPC = null;
            anim.SetBool("Sit", true);
        }
    }
}
