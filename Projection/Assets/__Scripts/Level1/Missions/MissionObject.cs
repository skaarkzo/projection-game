using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionObject : MonoBehaviour
{

    public int missionNumber;
    public MissionManager missionManager;

    public string startText;
    public string endText;

    public bool isItemQuest;
    public string targetItem;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isItemQuest)
        {
            if(missionManager.itemCollected == targetItem)
            {
                missionManager.itemCollected = null;
                EndMission();
            }
        }
    }

    public void StartMission()
    {
        missionManager.ShowMissionText(startText);
    }

    public void EndMission()
    {
        missionManager.ShowMissionText(endText);
        missionManager.missionCompleted[missionNumber] = true;
        gameObject.SetActive(false);
    }


}
