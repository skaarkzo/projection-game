using UnityEngine;

public class MissionTrigger : MonoBehaviour
{

    // Referencing other scripts 
    private MissionManager missionManager;
    private MissionWaypoint missionWaypoint;

    public int missionNumber; // Variable to store mission number

    public bool startMission; // Variable to start if the mission is a start mission 
    public bool endMission; // Variable to end if the mission is a start mission 

    public bool enablePause; // Variable 

    public Transform target; // Target 

    //public Transform target; // Target 

    // Start is called before the first frame update
    void Start()
    {
        // Linking other scripts
        missionManager = FindObjectOfType<MissionManager>();
        missionWaypoint = FindObjectOfType<MissionWaypoint>();
    }

    private void Update()
    {

    }

    
    // On trigger function
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Entered");


        //Checks tag of object colliding to insure it is the player
        if (other.tag == "Player")
        {
            //Checks to see if the mission is not completed
            if (!missionManager.missionCompleted[missionNumber])
            {

                //Checks to see if it is a start mission and if the mission has not been started
                if (!missionManager.missionStarted && startMission && !missionManager.missions[missionNumber].gameObject.activeSelf)
                {

                    if (target == null)
                    {

                    }
                    else
                    {
                        missionWaypoint.target = target;
                        missionWaypoint.img.gameObject.SetActive(true);
                    }


                    missionManager.missions[missionNumber].gameObject.SetActive(true); // Activates game object
                    missionManager.missions[missionNumber].StartMission(); // Starts mission
                    missionManager.missionStarted = true;
                }

                // Checks to see if it is a end mission and if the mission has started
                if (missionManager.missionStarted && endMission && missionManager.missions[missionNumber].gameObject.activeSelf)
                {
                    missionManager.missions[missionNumber].EndMission(); // Ends mission
                    missionWaypoint.img.gameObject.SetActive(false);
                    missionManager.missionStarted = false;
                }
            }
        }

    }
}