using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player")
        {
            HealthBar healthBar = GameObject.Find("GUI").transform.GetChild(1).GetComponent<HealthBar>();
            healthBar.SetHealth(0);
        }
    }
}
