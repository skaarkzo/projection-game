using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CharacterSwap : MonoBehaviour
{

    public List<GameObject> players;
    public static GameObject currentPlayer;
    public static GameObject previousPlayer;

    public CinemachineFreeLook cam;

    private bool keyOneLock = false;
    private bool keyTwoLock = false;
    private bool keyThreeLock = false;

    // Start is called before the first frame update
    void Start()
    {
        cam = FindObjectOfType<CinemachineFreeLook>();

        // Set the currentPlayer to the first character at the beginning of the game and toggle the rest off.
        currentPlayer = players[0];

        for (int i = 0; i < players.Count; i++)
        {           
            if (players[i] != currentPlayer)
            {
                players[i].SetActive(false);
                keyOneLock = true;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Set the currentPlayer to previousPlayer before the character is switched.
        previousPlayer = currentPlayer;

        if (Input.GetKeyDown(KeyCode.Alpha1) && keyOneLock == false)
        {
            // Sets the currentPlayer off and changes the currentPlayer to the first player.
            currentPlayer.SetActive(false);
            currentPlayer = players[0];
            currentPlayer.transform.position = previousPlayer.transform.position;
            currentPlayer.SetActive(true);

            // Lock the number one key but keep the others on to prevent glitches.
            keyTwoLock = false;
            keyOneLock = true;
            keyThreeLock = false;
        }

        else if (Input.GetKeyDown(KeyCode.Alpha2) && keyTwoLock == false)
        {
            // Sets the currentPlayer off and changes the currentPlayer to the second player.
            currentPlayer.SetActive(false);
            currentPlayer = players[1];
            currentPlayer.transform.position = previousPlayer.transform.position;
            currentPlayer.SetActive(true);

            // Lock the number two key but keep the others on to prevent glitches.
            keyTwoLock = true;
            keyOneLock = false;
            keyThreeLock = false;
        }

        else if (Input.GetKeyDown(KeyCode.Alpha3) && keyThreeLock == false)
        {
            // Sets the currentPlayer off and changes the currentPlayer to the third player.
            currentPlayer.SetActive(false);
            currentPlayer = players[2];
            currentPlayer.transform.position = previousPlayer.transform.position;
            currentPlayer.SetActive(true);

            // Lock the number three key but keep the others on to prevent glitches.
            keyTwoLock = false;
            keyOneLock = false;
            keyThreeLock = true;
        }

        // Get the follow object of the current player and set the camera's follow and look at to that player's follow object.
        Transform followObj = currentPlayer.transform.GetChild(1);
        cam.LookAt = followObj;
        cam.Follow = followObj;
    }
}
