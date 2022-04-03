using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{

    public GameObject dialogueBox;
    public Text dialogueText;

    public bool dialogueActive;

    public string[] dialogueLines;
    public int currentLine;

    public float timeLeft = 5;

    // Start is called before the first frame update
    void Start()
    {
        // Disables dialogue box 
        dialogueBox.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        if (dialogueActive)
        {
            timeLeft -= Time.deltaTime;
        }

        // Goes through the different lines of text
        if ((dialogueActive && Input.GetKeyUp(KeyCode.X)) || (dialogueActive && timeLeft <= 0))
        {
            currentLine++;
            timeLeft = 5;
        }

        // Ending the dialogue box and disabling it
        if (currentLine >= dialogueLines.Length)
        {
            Time.timeScale = 1;

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
        timeLeft = 5;
        dialogueActive = true;
        dialogueBox.SetActive(true);
    }

}
