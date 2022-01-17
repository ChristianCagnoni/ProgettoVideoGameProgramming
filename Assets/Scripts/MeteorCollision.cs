using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//script che gestisce la collisione delle meteore
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

    //in base al personaggio quando entra nel trigger fai cose diverse
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            Destroy(other.gameObject);
        }else if (other.tag == "Boss")
        {
            BossHealthBar bossBar = GameObject.Find("GUI").transform.GetChild(4).GetComponent<BossHealthBar>();
            bossBar.SetHealth((int)(bossBar.GetHealth() - 5));
        }
    }
}
