using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawner : MonoBehaviour
{

    public GameObject gui;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.name == "Player")
        {
            transform.GetChild(0).gameObject.SetActive(true);
            gui.transform.GetChild(4).gameObject.SetActive(true);
        }
    }

}
