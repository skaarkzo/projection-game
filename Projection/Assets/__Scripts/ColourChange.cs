using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColourChange : MonoBehaviour
{
    public Material[] material;
    public int x;
    Renderer rend;

    Renderer[] children;

    public bool lightOn = false;
    private float timeDelay;

    private void Start()
    {

        x = 0;
        rend = GetComponent<Renderer>();
        rend.enabled = true;
        rend.sharedMaterial = material[x];

    }

    // Update is called once per frame

    void Update()
    {

        if (lightOn == false)
        {
            StartCoroutine(LightOn());
        }

        rend.sharedMaterial = material[x];

    }


    IEnumerator LightOn()
    {
        lightOn = true;
        x = 0;
        timeDelay = 1;
        yield return new WaitForSeconds(timeDelay);
        x = 1;
        timeDelay = 1;
        yield return new WaitForSeconds(timeDelay);
        lightOn = false;

    }


}
