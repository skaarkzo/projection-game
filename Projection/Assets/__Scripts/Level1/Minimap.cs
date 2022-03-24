using UnityEngine;

public class Minimap : MonoBehaviour
{

    public Transform player; // Variable to store player 

    private void LateUpdate()
    {
        Vector3 newPosition = player.position; // Sets new position as player position
        newPosition.y = transform.position.y; // Sets y axis to current transform position to stay the same
        transform.position = newPosition; // Sets the transform position to new position
    }

}
