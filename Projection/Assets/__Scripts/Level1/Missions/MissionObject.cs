using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionObject : MonoBehaviour
{

    public int missionNumber;
    public MissionManager missionManager;

    public string startText;
    public string endText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
