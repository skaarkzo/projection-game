using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCTrigger : MonoBehaviour
{

    // Initialize Fields.
    private GameObject triggeringNPC;
    [HideInInspector] public bool triggering;

    private static Animator anim;

    void Update()
    {
        // Get the animator component.
        anim = triggeringNPC.GetComponent<Animator>();

        if (triggering)
        {
            Debug.Log("Player is triggering with " + triggeringNPC.name);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // If the BlacksmithNPC is triggered, play the wave and set the triggeringNPC to the Blacksmith.
        if (other.tag == "BlacksmithNPC")
        {
            triggering = true;
            triggeringNPC = other.gameObject;
            anim = triggeringNPC.GetComponent<Animator>();
            anim.SetTrigger("Wave");

        }

        // If the MerchantNPC is triggered, play the wave and set the triggeringNPC to the Merchant.
        else if (other.tag == "MerchantNPC")
        {
            triggering = true;
            triggeringNPC = other.gameObject;
            anim = triggeringNPC.GetComponent<Animator>();
            anim.SetTrigger("StandTrigger");

            if (triggering)
            {
                anim.SetBool("Sit", false);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // If the no NPC is being triggered, set the triggeringNPC to null.
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
