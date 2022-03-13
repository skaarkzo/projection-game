using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelOneScript : MonoBehaviour
{
    public string popUp;
    public float timer = 3;
    // Start is called before the first frame update
    void Start()
    {
        PopUpSystem pop = GameObject.FindGameObjectWithTag("Game Manager").GetComponent<PopUpSystem>();
        pop.PopUp(popUp);
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if(timer <= 0)
        {
            PopUpSystem pop = GameObject.FindGameObjectWithTag("Game Manager").GetComponent<PopUpSystem>();
            pop.PopUp(popUp);
        }
    }
}
