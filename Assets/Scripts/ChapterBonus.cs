using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChapterBonus : MonoBehaviour
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
            Player.secret = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.name == "Player")
        {
            Player.secret = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.name == "Player")
        {
            if (Player.isPower && Player.isPowerUsed)
            {
                Debug.Log("segreto");
            }
        }
    }

}
