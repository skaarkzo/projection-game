using System.Collections;
using UnityEngine;

public class FlickeringLight : MonoBehaviour
{

    public bool isFlickering = false; // Sets flickering variable to false
    private float timeDelay; // Adds time delay

    // Update is called once per frame
    void Update()
    {
        // Checks if flickering is off
        if (isFlickering == false)
        {
            // Enables flickering by calling flickering method
            StartCoroutine(FlickerLight());
        }

    }

    // Flickering method
    IEnumerator FlickerLight()
    {
        isFlickering = true; // Sets isFlickering variable on 

        this.gameObject.GetComponent<Light>().enabled = false; // Disable light
        
        timeDelay = 1; // Sets time delay 
        yield return new WaitForSeconds(timeDelay); // Adds time delay

        this.gameObject.GetComponent<Light>().enabled = true; // Enable light 

        timeDelay = 1; // Sets time delay 
        yield return new WaitForSeconds(timeDelay); // Adds time delay
        
        isFlickering = false; // Sets isFlickering variable off

    }

    
}