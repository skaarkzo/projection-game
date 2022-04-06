using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachGun : MonoBehaviour
{

    public GameObject gun;

    private AyanMainController ayanMainController; // Referencing other scripts 


    // Start is called before the first frame update
    void Start()
    {

        ayanMainController.gun.SetActive(false);

        //gun = GameObject.Find("sm_wep_shotgun_02");

        //gun.SetActive(false);


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        // Checks tag of object colliding to insure it is the player
        if (other.tag == "Player")
        {

            ayanMainController.gun.SetActive(true);

        }
    }

}
