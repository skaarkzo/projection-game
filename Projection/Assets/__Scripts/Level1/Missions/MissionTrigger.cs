using UnityEngine;

public class MissionTrigger : MonoBehaviour
{
    
    private MissionManager missionManager; // Referencing other scripts 

    public int missionNumber; // Variable to store mission number

    public bool startMission; // Variable to start if the mission is a start mission 
    public bool endMission; // Variable to end if the mission is a start mission 

    // Start is called before the first frame update
    void Start()
    {
        // Linking other scripts
        missionManager = FindObjectOfType<MissionManager>();
    }

    // On trigger function
    private void OnTriggerEnter(Collider other)
    {
        // Checks tag of object colliding to insure it is the player
        if (other.tag == "Player")
        { 
            // Checks to see if the mission is not completed 
            if (!missionManager.missionCompleted[missionNumber])
            {

                // Checks to see if it is a start mission and if the mission has not been started
                if (startMission && !missionManager.missions[missionNumber].gameObject.activeSelf)
                {
                    missionManager.missions[missionNumber].gameObject.SetActive(true); // Activates game object
                    missionManager.missions[missionNumber].StartMission(); // Starts mission
                }

                // Checks to see if it is a end mission and if the mission has started
                if(endMission && missionManager.missions[missionNumber].gameObject.activeSelf)
                {
                    missionManager.missions[missionNumber].EndMission(); // Ends mission
                }

            }
        }
    }

}
