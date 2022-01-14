using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalBarrier : MonoBehaviour
{

    private SphereCollider sphere;
    private GameObject child;
    private GameObject child2;
    static public bool inPortalZone;
    public GameObject gui;
    public GameObject oldS;
    public GameObject newS;
    public AudioSource oldSource;
    public AudioSource newSource;

    // Start is called before the first frame update
    void Start()
    {
        sphere = transform.GetComponent<SphereCollider>();
        inPortalZone = false;
        child=transform.GetChild(0).gameObject;
        child2 = transform.GetChild(1).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player")
        {
            oldSource.Stop();
            oldS.SetActive(false);
            newSource.Play();
            newS.SetActive(true);
            child.GetComponent<BoxCollider>().isTrigger = false;
            child2.GetComponent<BoxCollider>().isTrigger = false;
            inPortalZone =true;
            gui.transform.GetChild(4).gameObject.SetActive(true);
        }
    }
}
