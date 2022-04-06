//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.AI;

//public class DroneController : MonoBehaviour
//{

//    public float lookRadius = 10f;

//    Transform target;
//    NavMeshAgent agent;

//    public float fireRate = 1f;
//    private float fireCountdown = 0f;

//    public GameObject bulletPrefab;
//    public Transform firePoint;

//    private Animator anim;

//    private GameObject playerObject;

//    public int maxHealth = 100;
//    public int currentHealth;
//    public int pointsValue = 5;

//    // Start is called before the first frame update
//    void Start()
//    {
//        playerObject = GameObject.Find("Player");
//        target = PlayerManager.instance.player.transform;
//        agent = GetComponent<NavMeshAgent>();
//        anim = GetComponent<Animator>();
//    }

//    // Update is called once per frame
//    void Update()
//    {
//        float distance = Vector3.Distance(target.position, transform.position);

//        if (distance <= lookRadius)
//        {
//            agent.SetDestination(target.position);
//            if (distance <= agent.stoppingDistance + 6)
//            {
//                FaceTarget();
//                if (fireCountdown <= 0)
//                {
//                    Shoot();
//                    fireCountdown = 1f / fireRate;
//                }
//            }
//        }

//        fireCountdown -= Time.deltaTime;

//    }

//    void Shoot()
//    {
//        GameObject bulletGO = (GameObject) Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
//        Bullet bullet = bulletGO.GetComponent<Bullet>();

//        if (bullet != null)
//        {
//            bullet.Seek(target);
//        }
//    }

//    void FaceTarget()
//    {
//        Vector3 direction = (target.position - transform.position).normalized; // Get direction to the player
//        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z)); // Get a rotation to the player
//        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
//    }

//    public void EnemyTakeDamage()
//    {

//        currentHealth -= 10;

//        if (currentHealth <= 0)
//        {
//            Destroy(gameObject);
//        }
//    }

//    void OnDrawGizmosSelected()
//    {
//        Gizmos.color = Color.red;
//        Gizmos.DrawWireSphere(transform.position, lookRadius);
//    }
//}
