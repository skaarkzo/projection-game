using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicCharacterController : MonoBehaviour
{

    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 Movement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        transform.position += Movement * speed * Time.deltaTime;
    }

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
