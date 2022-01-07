using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckArrowAttack : MonoBehaviour
{

    private BossHealthBar bossBar;

    // Start is called before the first frame update
    void Start()
    {
        bossBar = GameObject.Find("GUI").transform.GetChild(4).GetComponent<BossHealthBar>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("ffwfwfwe");
        Debug.Log(other.name);
        if (other.tag == "Boss")
        {
            Debug.Log("colpèito");
            bossBar.SetHealth((int)(bossBar.GetHealth() - 35));
        }
    }

}
