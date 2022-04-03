//using UnityEngine;

//public class DialogueHolder : MonoBehaviour
//{
//    // Variable to hold dialogue
//    public string dialogue;

//    // Referencing other scripts 
//    private DialogueManager dialogueManager;

//    public string[] dialogueLines; // Array to store dialogue lines

//    public float timeLeft = 5;

//    // Start is called before the first frame update
//    void Start()
//    {
//        dialogueManager = FindObjectOfType<DialogueManager>(); // Linking other scripts
//    }

//    // Update is called once per frame
//    void Update()
//    {
//    }

//    // On trigger function
//    private void OnTriggerStay(Collider other)
//    {
//        // Checks tag of object colliding to insure it is the player
//        if (other.gameObject.name == "Player")
//        {
//            // Checks to see if "X" is pressed
//            if (Input.GetKeyDown(KeyCode.X) || dialogueManager.timeLeft <= 0)
//            {
//                // Checks to see if dialogue is not active
//                if (!dialogueManager.dialogueActive)
//                {

//                    dialogueManager.dialogueLines = dialogueLines; // Sets dialogue lines to text that is edited in unity
//                    dialogueManager.currentLine = 0; // Resets current line
//                    dialogueManager.ShowDialogue(); // Shows dialogue

//                }

//            }
//        }
//    }

//}

using UnityEngine;

public class DialogueHolder : MonoBehaviour
{
    // Variable to hold dialogue
    public string dialogue;

    // Referencing other scripts 
    private DialogueManager dialogueManager;

    public string[] dialogueLines; // Array to store dialogue lines

    // Start is called before the first frame update
    void Start()
    {
        dialogueManager = FindObjectOfType<DialogueManager>(); // Linking other scripts
    }

    // On trigger function
    private void OnTriggerStay(Collider other)
    {
        // Checks tag of object colliding to insure it is the player
        if (other.gameObject.name == "Player")
        {
            // Checks to see if "X" is pressed
            if (Input.GetKeyDown(KeyCode.X))
            {
                // Checks to see if dialogue is not active
                if (!dialogueManager.dialogueActive)
                {

                    dialogueManager.dialogueLines = dialogueLines; // Sets dialogue lines to text that is edited in unity
                    dialogueManager.currentLine = 0; // Resets current line
                    dialogueManager.ShowDialogue(); // Shows dialogue

                }

            }
        }
    }

}
