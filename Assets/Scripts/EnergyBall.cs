using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyBall : MonoBehaviour
{

    public float duration;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("life");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator life()
    {
        yield return new WaitForSeconds(duration);
        Destroy(gameObject);
    }
}
