using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//script che gestisce la poison sphere
public class PoisonSphere : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //se player entra nel trigger azzera la vita
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player")
        {
            GameObject.Find("GUI").transform.GetChild(1).GetComponent<HealthBar>().SetHealth(0);
        }
    }

}
