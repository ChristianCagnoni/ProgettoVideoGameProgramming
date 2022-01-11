using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBossSpawner : MonoBehaviour
{
    public GameObject boss;

    private GameObject child;

    // Start is called before the first frame update
    void Start()
    {
        child=transform.GetChild(0).gameObject;
        child.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player")
        {
            child.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.name == "Player")
        {
            boss.SetActive(true);
        }
    }

}
