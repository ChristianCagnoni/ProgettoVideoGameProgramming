using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class LightNingSpawner : MonoBehaviour
{

    public GameObject[] prefab;
    private GameObject[] lightnings;

    // Start is called before the first frame update
    void Start()
    {
        lightnings = new GameObject[prefab.Length];
        lightnings[0] = Instantiate(prefab[0], new Vector3(-60.4f, 3.99000001f, -64.4f), Quaternion.identity);
        lightnings[1] = Instantiate(prefab[1], new Vector3(0, 3.99000001f, 0.0500000007f), Quaternion.identity);
        lightnings[2] = Instantiate(prefab[2], new Vector3(0, 3.99000001f, 0.0500000007f), Quaternion.identity);
        lightnings[3] = Instantiate(prefab[3], new Vector3(0, 3.99000001f, 0.0500000007f), Quaternion.identity);
        StartCoroutine("spawner");
        Debug.Log(lightnings[0].name);
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator spawner()
    {
        while (true)
        {
            
            lightnings[0].GetComponent<VisualEffect>().Play();
            yield return new WaitForSeconds(5);
            lightnings[0].GetComponent<VisualEffect>().Stop();
        }
    }
}
