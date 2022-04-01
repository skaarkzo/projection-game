using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissionWaypoint : MonoBehaviour
{

    private MissionTrigger missionTrigger; // Referencing other scripts 

    public RawImage img; // Icon

    public Transform target; // Target 

    public Text meter; // Text for distance

    public Vector3 offset; // Adjust the position of the icon

    void Start()
    {
        // Linking other scripts
        missionTrigger = FindObjectOfType<MissionTrigger>();
    }

    private void Update()
    {
        // Giving limits to the icon so it sticks on the screen
        float minX = img.GetPixelAdjustedRect().width / 2; // Minimum X position (half of the icon width)
        float maxX = Screen.width - minX; // Maximum X position

        float minY = img.GetPixelAdjustedRect().height / 2; // Minimum Y position (half of the height)
        float maxY = Screen.height - minY; // Maximum Y position

        // Temporary variable to store the converted position from 3D world point to 2D screen point
        Vector2 pos = Camera.main.WorldToScreenPoint(target.position + offset);

        // Check if the target is behind us, to only show the icon once the target is in front
        if (Vector3.Dot((target.position - transform.position), transform.forward) < 0)
        {
            // Check if the target is on the left side of the screen
            if (pos.x < Screen.width / 2)
            {
                // Place it on the right (Since it's behind the player, it's the opposite)
                pos.x = maxX;
            }
            else
            {
                // Place it on the left side
                pos.x = minX;
            }
        }

        // Limit the X and Y positions
        pos.x = Mathf.Clamp(pos.x, minX, maxX);
        pos.y = Mathf.Clamp(pos.y, minY, maxY);

        // Update the marker's position
        img.transform.position = pos;

        // Change the meter text to the distance with the meter unit 'm'
        meter.text = ((int)Vector3.Distance(target.position, transform.position)).ToString() + "m";
    }
}