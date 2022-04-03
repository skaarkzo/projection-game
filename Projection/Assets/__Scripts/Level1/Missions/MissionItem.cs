//using UnityEngine;

//public class MissionItem : MonoBehaviour
//{

//    public int missionNumber; // Variable to store mission number

//    private MissionManager missionManager; // Referencing other scripts 

//    public string itemName; // String variable to store item name

//    // Start is called before the first frame update
//    void Start()
//    {
//        // Linking other scripts
//        missionManager = FindObjectOfType<MissionManager>();
//    }

//    // On trigger funtion
//    private void OnTriggerEnter(Collider other)
//    {
//        // Checks tag of object colliding to insure it is the player
//        if (other.tag == "Player")
//        {
//            // Checks to see if the mission is not completed and is active 
//            if(!missionManager.missionCompleted[missionNumber] && missionManager.missions[missionNumber].gameObject.activeSelf)
//            {
//                missionManager.itemCollected = itemName; // Sets item to be collected to item name set in unity
//                gameObject.SetActive(false); // Disables object upon collision
//            }
//        }
//    }

//}


using UnityEngine;

public class MissionItem : MonoBehaviour
{

    public int missionNumber; // Variable to store mission number

    private MissionManager missionManager; // Referencing other scripts 

    public string itemName; // String variable to store item name

    // Start is called before the first frame update
    void Start()
    {
        // Linking other scripts
        missionManager = FindObjectOfType<MissionManager>();
    }

    // On trigger funtion
    private void OnTriggerEnter(Collider other)
    {
        // Checks tag of object colliding to insure it is the player
        if (other.tag == "Player")
        {
            // Checks to see if the mission is not completed and is active 
            if (!missionManager.missionCompleted[missionNumber] && missionManager.missions[missionNumber].gameObject.activeSelf)
            {
                missionManager.itemCollected = itemName; // Sets item to be collected to item name set in unity
                gameObject.SetActive(false); // Disables object upon collision
            }
        }
    }

}
