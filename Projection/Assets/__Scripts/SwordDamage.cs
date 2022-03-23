using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordDamage : MonoBehaviour
{

    private GameObject enemyObject;
    private bool triggering;

    int damage = 50;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        inflictDamage();
    }

    private void inflictDamage()
    {
        if (triggering)
        {
            enemyObject.GetComponent<EnemyController>().EnemyTakeDamage(damage);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        triggering = true;
        Debug.Log("trigerring");
    }

    private void OnTriggerExit(Collider other)
    {
        triggering = false;
    }
}
