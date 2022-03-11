using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColourChange : MonoBehaviour
{
    public Material brightRed;
    public Material darkRed;


    private void Start()
    {

        Invoke("m1", 1.0f);

    }

    // Update is called once per frame
    void Update()
    {

    }


    void m1()
    {
        GetComponent<Renderer>().material = brightRed;
        Invoke("m2", 1.0f);
    }

    void m2()
    {
        GetComponent<Renderer>().material = darkRed;
        Invoke("m1", 1.0f);
    }

}
