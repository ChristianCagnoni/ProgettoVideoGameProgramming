using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script che gestisce l'arma di supporto nel secondo livello
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

    //se il boss entra nel trigger subisce un danno
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);
        if (other.tag == "Boss")
        {
            bossBar.SetHealth((int)(bossBar.GetHealth() - 35));
        }
    }

}
