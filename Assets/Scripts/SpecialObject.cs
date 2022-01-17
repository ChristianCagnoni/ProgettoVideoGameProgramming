using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//script che gestisce l'oggetto speciale
public class SpecialObject : MonoBehaviour
{

    private GameObject child;
    private GameObject child2;

    // Start is called before the first frame update
    void Start()
    {
        child=transform.GetChild(0).gameObject;
        child2 = transform.GetChild(1).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        //fai rutotare l'oggetto
        child.transform.localEulerAngles += new Vector3(0,45,0)*Time.deltaTime;
        child2.transform.localEulerAngles += new Vector3(0, 45, 0) * Time.deltaTime;
    }

    //se player entra nel trigger lancia la scena finale
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene("ExtendedChapter", LoadSceneMode.Single);
        }
    }
}
