using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script non utilizzato(prova per fare corrispondere lo sguardo)
public class Billboard : MonoBehaviour
{

    public Transform cam;

    // Update is called once per frame
    void LateUpdate()
    {
        transform.LookAt(transform.position + cam.forward); 
    }
}
