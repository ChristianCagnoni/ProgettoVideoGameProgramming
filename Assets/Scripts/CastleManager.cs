using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script per l'apparizione del portale nel livello bonus
public class CastleManager : MonoBehaviour
{

    public GameObject portal;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //se il player entra nel trigger rendi visibile il portale
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player")
        {
            portal.SetActive(true);
        }
    }

}
