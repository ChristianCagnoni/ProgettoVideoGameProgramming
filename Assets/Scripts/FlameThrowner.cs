using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameThrowner : MonoBehaviour
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
            Destroy(other.gameObject);
            EnemyGenerator.enemyCounter--;
        }
        else if (other.tag == "Boss")
        {
            BossHealthBar bossBar = GameObject.Find("GUI").transform.GetChild(4).GetComponent<BossHealthBar>();
            bossBar.SetHealth((int)(bossBar.GetHealth() - 15));
        }
        else if (other.tag == "Player")
        {
            HealthBar healthBar = GameObject.Find("GUI").transform.GetChild(1).GetComponent<HealthBar>();
            healthBar.SetHealth((int)(healthBar.GetHealth() - 15));
        }
    }

}
