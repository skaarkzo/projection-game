using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class DialogueManager : MonoBehaviour
{

    public GameObject dialogueBox;
    public Text dialogueText;

    public bool dialogueActive;

    public string[] dialogueLines;
    public int currentLine;

    // Start is called before the first frame update
    void Start()
    {
        // Disables dialogue box 
        dialogueBox.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // Goes through the different lines of text
        if (dialogueActive && Input.GetKeyUp(KeyCode.X))
        {
            currentLine++;
        }

        // Ending the dialogue box and disabling it
        if (currentLine >= dialogueLines.Length)
        {
            dialogueBox.SetActive(false);
            dialogueActive = false;
            currentLine = 0;
        }

        // Setting text to current line text
        dialogueText.text = dialogueLines[currentLine];

    }

    // Show dialogue
    public void ShowDialogue()
    {
        dialogueActive = true;
        dialogueBox.SetActive(true);
    }

    // Pause game 
    void PauseGame()
    {
        Time.timeScale = 0;
    }

    // Resume game 
    void ResumeGame()
    {
        Time.timeScale = 1;
    }

}
