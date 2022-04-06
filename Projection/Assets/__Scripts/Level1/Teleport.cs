using UnityEngine;
using UnityEngine.SceneManagement;

public class Teleport : MonoBehaviour
{

    // On trigger funtion
    void OnTriggerEnter(Collider other)
    {
        // Checks tag of object colliding to insure it is the player
        if (other.tag == "Player")
        {
            SceneManager.LoadScene(1); // teleptors player to scene 2
        }
    }
}