using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPositionGenerator : MonoBehaviour
{

    // Reference to the Prefab
    public GameObject part1;
    public GameObject part2;
    public GameObject part3;

    private int randomNum1;
    private int randomNum2;
    private int randomNum3;

    // Instantiate the Prefab when the game starts
    void Start()
    {
        randomNum1 = Random.Range(1,10);
        randomNum2 = Random.Range(1, 12);
        randomNum3 = Random.Range(1, 9);

        Debug.Log(randomNum1);
        Debug.Log(randomNum2);
        Debug.Log(randomNum3);

        // Instantiate at position set depending on random 
        if (part1)
        {
            if (randomNum1 % 2 == 0)
            {
                Instantiate(part1, new Vector3(-40.5f, 3.597008f, 18.5f), Quaternion.identity);
            }
            else if (randomNum1 % 2 == 1)
            {
                Instantiate(part1, new Vector3(-46.5f, 3.597008f, 55.5f), Quaternion.identity);
            }
        }

        if (part2)
        {
            if (randomNum2 == 1 || randomNum2 == 7)
            {
                Instantiate(part2, new Vector3(-110f, 4.75f, -45.873f), Quaternion.identity);
            }
            else if (randomNum2 == 2 || randomNum2 == 8)
            {
                Instantiate(part2, new Vector3(-110f, 4.75f, -35.873f), Quaternion.identity);
            }
            else if (randomNum2 == 3 || randomNum2 == 9)
            {
                Instantiate(part2, new Vector3(-110f, 4.75f, -25.873f), Quaternion.identity);
            }
            else if (randomNum2 == 4 || randomNum2 == 10)
            {
                Instantiate(part2, new Vector3(-110f, 4.75f, -15.873f), Quaternion.identity);
            }
            else if (randomNum2 == 5 || randomNum2 == 11)
            {
                Instantiate(part2, new Vector3(-110f, 4.75f, -5.873f), Quaternion.identity);
            }
            else if (randomNum2 == 6 || randomNum2 == 12)
            {
                Instantiate(part2, new Vector3(-110f, 4.75f, 4.123f), Quaternion.identity);
            }

        }

        if (part3)
        {
            if (randomNum3 == 1 || randomNum2 == 4 || randomNum2 == 7)
            {
                Instantiate(part3, new Vector3(123.5f, 1.321064f, -5.874f), Quaternion.identity);
            }
            else if (randomNum3 == 2 || randomNum2 == 5 || randomNum2 == 8)
            {
                Instantiate(part3, new Vector3(101f, 1.321064f, -38.374f), Quaternion.identity);
            }
            else if (randomNum3 == 3 || randomNum2 == 6 || randomNum2 == 79)
            {
                Instantiate(part3, new Vector3(116.75f, 1.321064f, -25.874f), Quaternion.identity);
            }

        }

    }

    public void OnTriggerEnter(Collider other)
    {
        if (tag == "Collectable_Part1")
        {
        other.gameObject.SetActive(false);
        }
        else if (tag == "Collectable_Part2")
        {
            other.gameObject.SetActive(false);
        }
        else if (tag == "Collectable_Part3")
        {
            other.gameObject.SetActive(false);
        }
    }
}
