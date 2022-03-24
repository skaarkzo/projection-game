using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColourChange : MonoBehaviour
{
    public Material[] material; // Array to store materials
    public int x; // integer variable
    Renderer rend; // Renderer variable

    public bool lightOn = false; // light on variable set to false
    private float timeDelay; // time delay variable

    // Start is called before the first frame update
    private void Start()
    {
        x = 0; // Sets x to 0

        rend = GetComponent<Renderer>(); // Get renderer component
        rend.enabled = true; // Enables renderer
        rend.sharedMaterial = material[x]; // sets renderer material to x variab;e

    }

    // Update is called once per frame

    void Update()
    {

        // Checks if light is off
        if (lightOn == false)
        {
            // Calls LightOn method
            StartCoroutine(LightOn());
        }

        rend.sharedMaterial = material[x]; // Sets renderer material to x

    }

    // Method for light on
    IEnumerator LightOn()
    {

        lightOn = true; // Sets lighton variable to true
        
        x = 0; // Sets x to 0
        timeDelay = 1; // Sets time delay value
        yield return new WaitForSeconds(timeDelay); // Delays code

        x = 1; // Set x to 1
        timeDelay = 1; // Set time delay value
        yield return new WaitForSeconds(timeDelay); // Delays code

        lightOn = false; // Sets lighton variable to false

    }


}
