using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionTrigger : MonoBehaviour
{

    private MissionManager missionManager;

    public int missionNumber;

    public bool startMission;
    public bool endMission;

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
        if (other.tag == "Player")
        {
            if (!missionManager.missionCompleted[missionNumber])
            {

                if (startMission && !missionManager.missions[missionNumber].gameObject.activeSelf)
                {
                    missionManager.missions[missionNumber].gameObject.SetActive(true);
                    missionManager.missions[missionNumber].StartMission();
                }

                if(endMission && missionManager.missions[missionNumber].gameObject.activeSelf)
                {
                    missionManager.missions[missionNumber].EndMission();
                }

            }
        }
    }

}
