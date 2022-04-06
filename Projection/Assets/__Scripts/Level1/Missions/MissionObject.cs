using UnityEngine;

public class MissionObject : MonoBehaviour
{

    public int missionNumber;   // Variable to store mission number 
    public GameObject playerObj;

    // Referencing other scripts 
    public MissionManager missionManager;
    public DialogueManager dialogueManager;
    public DialogueHolder dialogueHolder;
    public MissionWaypoint missionWaypoint;
    public MissionTrigger missionTrigger;

    // Arrays to store start and end text
    public string[] startText;
    public string[] endText;

    public bool isItemMission; // Variable to check if mission is to collect an item

    public string targetItem; // Name of item that is to be collected

    public int lineCount; // Line number of dialogue


    // Start is called before the first frame update
    void Start()
    {
        // Linking other scripts
        dialogueManager = FindObjectOfType<DialogueManager>();
        missionWaypoint = FindObjectOfType<MissionWaypoint>();
        missionTrigger = FindObjectOfType<MissionTrigger>();
    }

    // Update is called once per frame
    void Update()
    {

        // Ending the dialogue box and disabling it
        if (dialogueManager.currentLine >= dialogueManager.dialogueLines.Length)
        {

            dialogueManager.dialogueBox.SetActive(false); // Disabling dialogue box
            dialogueManager.dialogueActive = false; // Disabling dialogue 
            dialogueManager.currentLine = 0; // Reseting current line
        }

        // Function for item mission
        if (isItemMission)
        {

            if (missionManager.itemCollected == targetItem)
            {
                missionManager.itemCollected = null;
                EndMission(); // Ends mission
            }
        }

    }

    // Start mission method
    public void StartMission()
    {
        dialogueManager.dialogueLines = startText; // Sets dialogue lines to start text set in unity
        dialogueManager.currentLine = 0; // Reseting current line
        dialogueManager.ShowDialogue(); // Shows dialogue
    }

    // End mission method
    public void EndMission()
    {
        dialogueManager.dialogueLines = endText; // Sets dialogue lines to end text set in unity
        dialogueManager.currentLine = 0; // Reseting current line
        dialogueManager.ShowDialogue(); // Shows dialogue

        gameObject.SetActive(false); // Disables missions so it does not repeat
        missionManager.missionCompleted[missionNumber] = true; // Sets misson as completed 
        missionManager.missionsCompleted++;
        playerObj.GetComponent<AyanMainController>().incrementMission(missionManager.missionsCompleted);
    }



}
