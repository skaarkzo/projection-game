using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class swordAttack : MonoBehaviour
{

    private bool triggering;
    public int damage = 1;
    private GameObject enemyObject;
    // Update is called once per frame
    void Start()
    {
        enemyObject = GameObject.Find("Droid");
    }
    void Update()
    {

    }

    /*private void inflictDamage()
    {
        if (triggering)
        {
            enemyObject.GetComponent<EnemyController>().EnemyTakeDamage(damage);
        }
    }*/

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "RobotNPC")
        {
            enemyObject.GetComponent<EnemyController>().EnemyTakeDamage(damage);
            enemyObject.GetComponent<CapsuleCollider>().enabled = false;
            //GetComponent<BoxCollider2D>().enabled = false;
            StartCoroutine(EnableBox(2.0F));
            Debug.Log("Sword TRIGEER");
        }

        IEnumerator EnableBox(float waitTime)
        {
            yield return new WaitForSeconds(waitTime);
            enemyObject.GetComponent<CapsuleCollider>().enabled = true;
        }
    }

    /*private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == enemyObject)
        {
            triggering = false;
            Debug.Log("Sword GONE");
        }
    }*/
}
