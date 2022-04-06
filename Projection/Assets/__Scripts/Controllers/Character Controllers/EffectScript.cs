using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectScript : MonoBehaviour
{
    // Initialize Fields.
    public GameObject[] effect;
    public Transform[] effectTransform;
    public Transform[] effectRotation;

    public void UseEffect(int number)
    {
        // Instantiate the effect.
        Instantiate(effect[number], effectTransform[number].position, effectRotation[number].rotation);
    }
}
