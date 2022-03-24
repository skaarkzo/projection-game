using UnityEngine;

public class RandomPositionGenerator : MonoBehaviour
{

    // Reference to the Prefab
    public GameObject part1;
    public GameObject part2;
    public GameObject part3;
    public GameObject part4;

    // Creating variables to hold random numbers that will be generated
    private int randomNum1;
    private int randomNum2;
    private int randomNum3;

    // Instantiate the Prefab when the game starts
    void Start()
    {
        // System chooses a random number in range of each function
        randomNum1 = Random.Range(1, 12);
        randomNum2 = Random.Range(1, 9);
        randomNum3 = Random.Range(1,10);


        // Instantiate at position set depending on random 
        
        // Part 1 location spawner
        if (part1)
        {
            if (randomNum1 == 1 || randomNum1 == 7)
            {
                Instantiate(part1, new Vector3(-110f, 4.75f, -45.873f), Quaternion.identity);
            }
            else if (randomNum1 == 2 || randomNum1 == 8)
            {
                Instantiate(part1, new Vector3(-110f, 4.75f, -35.873f), Quaternion.identity);
            }
            else if (randomNum1 == 3 || randomNum1 == 9)
            {
                Instantiate(part1, new Vector3(-110f, 4.75f, -25.873f), Quaternion.identity);
            }
            else if (randomNum1 == 4 || randomNum1 == 10)
            {
                Instantiate(part1, new Vector3(-110f, 4.75f, -15.873f), Quaternion.identity);
            }
            else if (randomNum1 == 5 || randomNum1 == 11)
            {
                Instantiate(part1, new Vector3(-110f, 4.75f, -5.873f), Quaternion.identity);
            }
            else if (randomNum1 == 6 || randomNum1 == 12)
            {
                Instantiate(part1, new Vector3(-110f, 4.75f, 4.123f), Quaternion.identity);
            }

        }

        // Part 2 location spawner
        if (part2)
        {
            if (randomNum2 == 1 || randomNum2 == 4 || randomNum2 == 7)
            {
                Instantiate(part2, new Vector3(123.5f, 1.321064f, -5.874f), Quaternion.identity);
            }
            else if (randomNum2 == 2 || randomNum2 == 5 || randomNum2 == 8)
            {
                Instantiate(part2, new Vector3(101f, 1.321064f, -38.374f), Quaternion.identity);
            }
            else if (randomNum2 == 3 || randomNum2 == 6 || randomNum2 == 79)
            {
                Instantiate(part2, new Vector3(116.75f, 1.321064f, -25.874f), Quaternion.identity);
            }

        }

        // Part 3 location spawner
        if (part3)
        {
            if (randomNum3 % 2 == 0)
            {
                Instantiate(part3, new Vector3(-40.5f, 3.597008f, 18.5f), Quaternion.identity);
            }
            else if (randomNum3 % 2 == 1)
            {
                Instantiate(part3, new Vector3(-46.5f, 3.597008f, 55.5f), Quaternion.identity);
            }
        }

        // Part 4 location spawner
        if (part4)
        {
            Instantiate(part4, new Vector3(0f, 9f, -111.75f), Quaternion.identity);

        }

    }

    // Disable collectible when collected
    public void OnTriggerEnter(Collider other)
    {
        if (tag == "Collectable")
        {
            other.gameObject.SetActive(false);
        }
    }
}
