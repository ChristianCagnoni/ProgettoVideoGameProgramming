using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script per la comparsa di blocchi nel gioco
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
        if(block.transform.position.y> 1.73000002)//muovi verso il basso l'oggetto finchè la condizione non è rispettata
            block.transform.position=block.transform.position-new Vector3(0,8f,0)*Time.deltaTime;
    }

    //se il player entra nel trigger rendi visibile l'oggetto e lancia la corutine
    private void OnTriggerExit(Collider other)
    {
        if (other.name == "Player")
        {
            block.SetActive(true);
            if(gameObject.name== "Block" || gameObject.name== "Block1")
            {
                StartCoroutine("dead");
            }
        }
    }

    //metodo che attende 2 secondi e poi azzera la vita
    IEnumerator dead()
    {
        yield return new WaitForSeconds(2);
        HealthBar healthBar = GameObject.Find("GUI").transform.GetChild(1).GetComponent<HealthBar>();
        healthBar.SetHealth(0);
    }

}
