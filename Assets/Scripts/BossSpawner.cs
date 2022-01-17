using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawner : MonoBehaviour
{

    public GameObject gui;
    public GameObject oldS;
    public GameObject newS;
    public AudioSource oldSource;
    public AudioSource newSource;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //se il player esce dal trigger cambia la musica e rende visibile il boss
    private void OnTriggerExit(Collider other)
    {
        if (other.name == "Player")
        {
            oldSource.Stop();
            oldS.SetActive(false);
            newS.SetActive(true);
            newSource.Play();
            transform.GetChild(0).gameObject.SetActive(true);
            gui.transform.GetChild(4).gameObject.SetActive(true);
        }
    }

}
