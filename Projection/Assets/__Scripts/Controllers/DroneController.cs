using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DroneController : MonoBehaviour
{

    public float lookRadius = 10f;

    public int damage = 1;

    Transform target;
    private GameObject triggeringNPC;
    private bool triggering;
    NavMeshAgent agent;

    private Animator anim;

    private GameObject playerObject;


    // Start is called before the first frame update
    void Start()
    {
        playerObject = GameObject.Find("Player");
        target = PlayerManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(target.position, transform.position);

        if (distance <= lookRadius)
        {
            agent.SetDestination(target.position);
            if (distance <= agent.stoppingDistance)
            {
                FaceTarget();
            }
        }
    }

    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized; // Get direction to the player
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z)); // Get a rotation to the player
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    private void inflictDamage()
    {
        if (triggering)
        {
            playerObject.GetComponent<Ayan1ControllerTest>().TakeDamage(damage);
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

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}
