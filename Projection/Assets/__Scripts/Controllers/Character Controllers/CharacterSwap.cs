using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CharacterSwap : MonoBehaviour
{

    public List<GameObject> players;
    public GameObject currentPlayer;
    public GameObject previousPlayer;

    public CinemachineFreeLook cam;

    private bool keyOneLock = false;
    private bool keyTwoLock = false;
    private bool keyThreeLock = false;

    // Start is called before the first frame update
    void Start()
    {
        cam = FindObjectOfType<CinemachineFreeLook>();

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

        previousPlayer = currentPlayer;

        if (Input.GetKeyDown(KeyCode.Alpha2) && keyTwoLock == false)
        {
            currentPlayer.SetActive(false);
            currentPlayer = players[1];
            currentPlayer.transform.position = previousPlayer.transform.position;
            currentPlayer.SetActive(true);

            keyTwoLock = true;
            keyOneLock = false;
            keyThreeLock = false;
        }

        else if (Input.GetKeyDown(KeyCode.Alpha3) && keyThreeLock == false)
        {
            currentPlayer.SetActive(false);
            currentPlayer = players[2];
            currentPlayer.transform.position = previousPlayer.transform.position;
            currentPlayer.SetActive(true);

            keyTwoLock = false;
            keyOneLock = false;
            keyThreeLock = true;
        }

        else if (Input.GetKeyDown(KeyCode.Alpha1) && keyOneLock == false)
        {
            currentPlayer.SetActive(false);
            currentPlayer = players[0];
            currentPlayer.transform.position = previousPlayer.transform.position;
            currentPlayer.SetActive(true);

            keyTwoLock = false;
            keyOneLock = true;
            keyThreeLock = false;
        }

        Transform followObj = currentPlayer.transform.GetChild(1);
        cam.LookAt = followObj;
        cam.Follow = followObj;
    }
}
