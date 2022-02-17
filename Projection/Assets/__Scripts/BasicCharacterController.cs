using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicCharacterController : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Level1LDoor")
        {
            if(other.GetComponent<SlidingDoor>().Moving == false)
            {
                other.GetComponent<SlidingDoor>().Moving = true;
            }
        }
        if (other.tag == "Level1RDoor")
        {
            if (other.GetComponent<SlidingDoor>().Moving == false)
            {
                other.GetComponent<SlidingDoor>().Moving = true;
            }
        }
    }

}
