using UnityEngine;
using UnityEngine.SceneManagement;

public class Teleport : MonoBehaviour
{

    // On trigger funtion
    private void OnTriggerEnter(Collider other)
    {
        // Checks tag of object colliding to insure it is the player
        if (other.tag == "Player")
        {

            SceneManager.LoadScene("_Level2"); // teleptors player to scene 2
            
        }
    }
}
