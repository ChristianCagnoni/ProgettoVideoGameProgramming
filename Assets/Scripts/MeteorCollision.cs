using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorCollision : MonoBehaviour
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
        if (other.tag == "Enemy")
        {
            Debug.Log("fatto");
            Destroy(other.gameObject);
        }else if (other.tag == "Boss")
        {
            BossHealthBar bossBar = GameObject.Find("GUI").transform.GetChild(4).GetComponent<BossHealthBar>();
            bossBar.SetHealth((int)(bossBar.GetHealth() - 5));
        }
    }
}
