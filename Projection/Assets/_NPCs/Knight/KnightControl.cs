using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightControl : MonoBehaviour
{

    private GameObject triggeringNPC;
    private bool triggering;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        anim = triggeringNPC.GetComponent<Animator>();
        if (triggering)
        {
            Debug.Log("triggering");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "KnightNPC")
        {
            triggering = true;
            triggeringNPC = other.gameObject;
            anim = triggeringNPC.GetComponent<Animator>();
            anim.SetBool("Walk", true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "KnightNPC")
        {
            triggering = false;
            triggeringNPC = null;
            anim.SetBool("Walk", false);
        }
    }

}
