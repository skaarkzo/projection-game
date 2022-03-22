using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{

    public float lookRadius = 10f;

    public int damage = 1;

    Transform target;
    private GameObject triggeringNPC;
    private bool triggering;
    NavMeshAgent agent;

    private Animator anim;


    // Start is called before the first frame update
    void Start()
    {
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
            Walk();
            if(distance <= agent.stoppingDistance)
            {
                Idle();
                FaceTarget();
                Attack();
            }
        }
    }
    
    void FaceTarget()
        {
            Vector3 direction = (target.position - transform.position).normalized; // Get direction to the player
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z)); // Get a rotation to the player
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
        }

    public void Walk()
    {
        anim.SetBool("Attack", false);
        anim.SetBool("Walk", true);       
    }

    public void Idle()
    {
        anim.SetBool("Walk", false);
    }

    public void Attack()
    {
        anim.SetBool("Attack", true);
    }

    private void OnTriggerEnter(Collider other)
    {
        other.gameObject.GetComponent<Ayan1ControllerTest>().TakeDamage(damage);
        Debug.Log("Take DMG");
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}
