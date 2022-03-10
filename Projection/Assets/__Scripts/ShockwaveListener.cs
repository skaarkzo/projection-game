using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ShockwaveListener : MonoBehaviour
{

    private CinemachineImpulseSource source;

    private void Awake()
    {
        source = GetComponent<CinemachineImpulseSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Shake", 3f, 4f);
    }

    // Update is called once per frame
    void Shake()
    {
        source.GenerateImpulse();
    }
}
