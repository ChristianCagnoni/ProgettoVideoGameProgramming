using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//script che gestisce il movimento delle frecce nel secondo livello
public class LaunchArrow : MonoBehaviour
{

    public GameObject arrow;

    private float cooldown;
    private bool ready;
    private Vector3 originalP;

    // Start is called before the first frame update
    void Start()
    {
        originalP = arrow.transform.position;
        ready = true;
        cooldown = 30;
    }

    // Update is called once per frame
    void Update()
    {
        if (!ready)//finchè la condizione è soddisfatta muovi le frecce
        {
            arrow.transform.position = arrow.transform.position + new Vector3(-8f, 0,0)*Time.deltaTime;
        }
    }

    //se player entra nel trigger attiva il lancio
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player" && ready)
        {
            StartCoroutine("launch");
        }   
    }

    //metodo che gestisce la logica di lancio e movimento
    IEnumerator launch()
    {
        ready = false;
        yield return new WaitForSeconds(cooldown);
        arrow.transform.position = originalP;
        ready = true;
    }
}
