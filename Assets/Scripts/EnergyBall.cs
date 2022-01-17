using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//script che gestisce le sfere di energia del boss finale
public class EnergyBall : MonoBehaviour
{

    public enum Barrier{total,partial,none};//tipologia di barriera incontrata

    public float duration;//massima distanza entro la quale la sfera viaggia

    private HealthBar HealthBar;
    private Barrier barrier;

    // Start is called before the first frame update
    void Start()
    {
        barrier = Barrier.none;
        StartCoroutine("life");
        HealthBar = GameObject.Find("GUI").transform.GetChild(1).GetComponent<HealthBar>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //crea danno al personaggio in base alla barriera trovata se il player entra nel trigger
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 8)
        {
            if (barrier == Barrier.none)
            {
                HealthBar.SetHealth((int)(HealthBar.GetHealth() - 25));
            }
            else if (barrier == Barrier.partial)
            {
                HealthBar.SetHealth((int)(HealthBar.GetHealth() - 10));
            }
            else
            {
                HealthBar.SetHealth((int)(HealthBar.GetHealth() - 0));
            }
        } else if (other.gameObject.layer == 14)
        {
            barrier = Barrier.total;
        }
        else if (other.gameObject.layer == 15)
        {
            barrier = Barrier.partial;
        }
    }

    //distruggi la sfera dopo tot secondi
    IEnumerator life()
    {
        yield return new WaitForSeconds(duration);
        Destroy(gameObject);
    }

}
