using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//script per la gestione della trappola 2
public class Trap : MonoBehaviour
{

    public Transform spawn1;
    public Transform spawn2;
    public GameObject enemy;
    public Transform target;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //se player entra nel trigger lancia la trappola
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player")
        {
            GameObject e1=Instantiate(enemy, spawn1);
            e1.GetComponent<Parasite>().transforms=new Transform[1];
            e1.GetComponent<Parasite>().transforms[0] = target;
            e1.GetComponent<Parasite>().numberT++;
            GameObject e2 = Instantiate(enemy, spawn2);
            e2.GetComponent<Parasite>().transforms = new Transform[1];
            e2.GetComponent<Parasite>().transforms[0] = target;
            e2.GetComponent<Parasite>().numberT++;
        }
    }
}
