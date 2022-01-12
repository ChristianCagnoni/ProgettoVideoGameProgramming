using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBossSpawner : MonoBehaviour
{
    public GameObject boss;
    public GameObject bossHealt;
    public GameObject poisonSphere;
    public float speed;

    private GameObject child;
    private bool start;
    private bool scaled;

    // Start is called before the first frame update
    void Start()
    {
        start = false;
        scaled = false;
        child=transform.GetChild(0).gameObject;
        child.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (start)
        {
            float step = speed * Time.deltaTime;
            if(poisonSphere.transform.localScale.x< 64.2166748)
            {
                poisonSphere.transform.localScale += new Vector3(10, 10, 10) * step;
            }
            else
            {
                scaled = true;
                start = false;
            }
        }
        if (scaled && poisonSphere.transform.position.y< 580.1)
        {
            float step = speed * Time.deltaTime;
            poisonSphere.transform.position += new Vector3(0, 2, 0) * step;
        }
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
            start = true;
            boss.SetActive(true);
            bossHealt.SetActive(true);
        }
    }

}
