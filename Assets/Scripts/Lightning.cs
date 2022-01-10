using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class Lightning : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator player()
    {
        while (true)
        {

            GetComponent<VisualEffect>().Play();
            yield return new WaitForSeconds(10);
            GetComponent<VisualEffect>().Stop();
        }
    }

}
