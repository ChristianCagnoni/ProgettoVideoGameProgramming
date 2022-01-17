using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//script che gestisce il god attack del boss finale
public class godAttack : MonoBehaviour
{

    private HealthBar healthBar;

    // Start is called before the first frame update
    void Start()
    {
        healthBar = GameObject.Find("GUI").transform.GetChild(1).GetComponent<HealthBar>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //se player entra subisce danno di 5
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            healthBar.SetHealth((int)(healthBar.GetHealth() - 5));
        }
    }

    //finchè player rimane subisce danno di 1
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            healthBar.SetHealth((int)(healthBar.GetHealth() - 1));
        }
    }
}
