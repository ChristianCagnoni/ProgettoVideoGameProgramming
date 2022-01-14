using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{

    public GameObject character1;
    public GameObject character2;

    // Start is called before the first frame update
    void Start()
    {
        if (SettingsManager.character == 0)
        {
            character1.SetActive(true);
            character2.SetActive(false);
        }
        else
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
