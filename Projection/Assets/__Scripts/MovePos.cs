using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePos : MonoBehaviour
{

    private Vector3 startPos;
    private Vector3 endPos;

    private GameObject triggeringNPC;
    private bool triggering;

    private bool standTrigger;

    private Animator anim;

    private Vector3 playerPos;
    private Vector3 npcPos;

    private float delay;


    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        endPos = startPos + new Vector3(0.2f, 0, 0);

        anim = triggeringNPC.GetComponent<Animator>();

        playerPos = GameObject.Find("Player").transform.position;
        npcPos = GameObject.Find(triggeringNPC.name).transform.position;

        float distance = Mathf.Sqrt(Mathf.Pow((playerPos.x - npcPos.x), 2) + Mathf.Pow((playerPos.y - npcPos.y), 2) + Mathf.Pow((playerPos.z - npcPos.z), 2));

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Move()
    {
        yield return new WaitForSeconds(2.1f);
        transform.position = transform.position - new Vector3(0.04f, 0, 0);
    }

    IEnumerator Return()
    {
        yield return new WaitForSeconds(2.1f);
        transform.position = transform.position + new Vector3(0.04f, 0, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        StartCoroutine(Move());
        //transform.position = transform.position + new Vector3(1, 0, 0);
        //StartCoroutine(ExampleCoroutine());
    }

    private void OnTriggerExit(Collider other)
    {
        StartCoroutine(Return());
        //StartCoroutine(ExampleCoroutine());
        //transform.position = transform.position - new Vector3(1, 0, 0);
        //StartCoroutine(ExampleCoroutine());

    }

}
