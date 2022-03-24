using UnityEngine;

public class MissionManager : MonoBehaviour
{

    public MissionObject[] missions; // Stores missions 
    public bool[] missionCompleted; // Variable to store if mission is completed or not

    // Referencing other scripts 
    public DialogueManager dialogueManager;
    public MissionObject missionObject;

    public string itemCollected; // String varianble to store item to be callected name

    // Start is called before the first frame update
    void Start()
    {
        // Linking other scripts
        missionObject = FindObjectOfType<MissionObject>();

        missionCompleted = new bool[missions.Length]; // Sets mission completed array to same length as mission length

    }

}
