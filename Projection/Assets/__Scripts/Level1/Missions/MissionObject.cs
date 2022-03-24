using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionObject : MonoBehaviour
{

    public int missionNumber;
    public MissionManager missionManager;

    public DialogueManager dialogueManager;
    public DialogueHolder dialogueHolder;

    public string[] startText;
    public string[] endText;

    public bool isItemQuest;
    public string targetItem;

    public int lineCount;


    // Start is called before the first frame update
    void Start()
    {
        dialogueManager = FindObjectOfType<DialogueManager>();
        dialogueHolder = FindObjectOfType<DialogueHolder>();

        lineCount = startText.Length;

    }

    // Update is called once per frame
    void Update()
    {

        if (dialogueManager.dialogueActive && Input.GetKeyUp(KeyCode.X))
        {
            //dialogueManager.currentLine++;
        }

        // Ending the dialogue box and disabling it
        if (dialogueManager.currentLine >= dialogueManager.dialogueLines.Length)
        {
            dialogueManager.dialogueBox.SetActive(false);
            dialogueManager.dialogueActive = false;
            dialogueManager.currentLine = 0;
        }

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

        dialogueManager.dialogueLines = startText;
        dialogueManager.currentLine = 0;
        dialogueManager.ShowDialogue();

    }

    public void EndMission()
    {

        dialogueManager.dialogueLines = endText;
        dialogueManager.currentLine = 0;
        dialogueManager.ShowDialogue();

        gameObject.SetActive(false);
        missionManager.missionCompleted[missionNumber] = true;

    }


}
