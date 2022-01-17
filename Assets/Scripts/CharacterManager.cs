using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Script che gestisce l'aspetto del personaggio
public class CharacterManager : MonoBehaviour
{

    public GameObject character1;
    public GameObject character2;

    // Start is called before the first frame update
    void Start()
    {
        if (SettingsManager.character == 0)//se il parametro character ha valore 0 seleziona il primo personaggio
        {
            character1.SetActive(true);
            character2.SetActive(false);
        }
        else//altrimenti seleziona il secondo personaggio
        {
            character1.SetActive(false);
            character2.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
