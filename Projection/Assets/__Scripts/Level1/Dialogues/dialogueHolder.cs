using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueHolder : MonoBehaviour
{

    public string dialogue;
    private DialogueManager dialogueManager;

    private MissionObject missionObject;

    public string[] dialogueLines;

    // Start is called before the first frame update
    void Start()
    {
        dialogueManager = FindObjectOfType<DialogueManager>();
        missionObject = FindObjectOfType<MissionObject>();

    }

    // Update is called once per frame
    void Update()
    {
           
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            if (Input.GetKeyDown(KeyCode.X))
            {

                if (!dialogueManager.dialogueActive)
                {
                    dialogueManager.dialogueLines = dialogueLines;
                    dialogueManager.currentLine = 0;
                    dialogueManager.ShowDialogue();
                }

            }
        }
    }

}
