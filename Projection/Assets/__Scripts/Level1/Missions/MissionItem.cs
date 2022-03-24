using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionItem : MonoBehaviour
{

    public int missionNumber;

    private MissionManager missionManager;

    public string itemName;

    // Start is called before the first frame update
    void Start()
    {
        missionManager = FindObjectOfType<MissionManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            if(!missionManager.missionCompleted[missionNumber] && missionManager.missions[missionNumber].gameObject.activeSelf)
            {
                missionManager.itemCollected = itemName;
                gameObject.SetActive(false);
            }
        }
    }

}
