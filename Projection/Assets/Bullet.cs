using UnityEngine;

public class Bullet : MonoBehaviour
{

    private Transform target;
    private GameObject playerObject;
    public float speed = 20f;

    public int damage = 1;

    private void Start()
    {
        playerObject = GameObject.Find("Player");
    }

    public void Seek(Transform _target)
    {
        target = _target;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
    }

    private void OnTriggerEnter(Collider other)
    {
        playerObject.GetComponent<Ayan1ControllerTest>().TakeDamage(damage);
        Destroy(gameObject);
    }
}
