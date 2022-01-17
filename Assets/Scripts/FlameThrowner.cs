using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//script che gestisce il danno da lanciafiamme
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

    //in base al tag del personaggio che entra nel trigger fai cose diverse
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")//nemico normale
        {
            Destroy(other.gameObject);
            EnemyGenerator.enemyCounter--;
        }
        else if (other.tag == "Boss")//boss
        {
            BossHealthBar bossBar = GameObject.Find("GUI").transform.GetChild(4).GetComponent<BossHealthBar>();
            bossBar.SetHealth((int)(bossBar.GetHealth() - 15));
        }
        else if (other.tag == "Player")//player
        {
            HealthBar healthBar = GameObject.Find("GUI").transform.GetChild(1).GetComponent<HealthBar>();
            healthBar.SetHealth((int)(healthBar.GetHealth() - 15));
        }
    }

}
