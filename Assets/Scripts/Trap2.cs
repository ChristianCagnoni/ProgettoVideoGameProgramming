using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//script per la gestione della trappola 2
public class Trap2 : MonoBehaviour
{

    public Transform spawn;
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

    //se player entra nel trigger lancia la corutine
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player")
        {

            StartCoroutine("spawnEnemy");
        }
    }

    //metodo che attiva la trappola
    IEnumerator spawnEnemy()
    {
        yield return new WaitForSeconds(1);
        GameObject e1 = Instantiate(enemy, spawn);
        e1.GetComponent<Parasite>().transforms = new Transform[1];
        e1.GetComponent<Parasite>().transforms[0] = target;
        e1.GetComponent<Parasite>().numberT++;
        yield return null;
    }
}
