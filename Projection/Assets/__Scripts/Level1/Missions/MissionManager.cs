using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionManager : MonoBehaviour
{

    public MissionObject[] missions;
    public bool[] missionCompleted;

    public DialogueManager dialogueManager;

    public MissionObject missionObject;

    public string itemCollected;

    


    // Start is called before the first frame update
    void Start()
    {
        missionObject = FindObjectOfType<MissionObject>();

        missionCompleted = new bool[missions.Length];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowMissionText(string missionText)
    {

        dialogueManager.dialogueLines = new string[1];
        dialogueManager.dialogueLines[0] = missionText;

        dialogueManager.currentLine = 0;
        dialogueManager.ShowDialogue();

    }
}
