using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectScript : MonoBehaviour
{

    public GameObject[] effect;
    public Transform[] effectTransform;
    public Transform[] effectRotation;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UseEffect(int number)
    {
        Instantiate(effect[number], effectTransform[number].position, effectRotation[number].rotation);
    }
}
