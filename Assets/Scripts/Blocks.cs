using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blocks : MonoBehaviour
{

    private GameObject block;

    // Start is called before the first frame update
    void Start()
    {
        block= transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if(block.transform.position.y> 1.73000002)
            block.transform.position=block.transform.position-new Vector3(0,8f,0)*Time.deltaTime;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.name == "Player")
        {
            block.SetActive(true);
        }
    }

}