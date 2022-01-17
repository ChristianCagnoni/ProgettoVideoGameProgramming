using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


//script che gestisce i portali
public class Portal : MonoBehaviour
{

    public string nextChapter;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //se player entra nel trigger passa alla prossima scena
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player")
        {
            SceneManager.LoadScene(nextChapter,LoadSceneMode.Single);
        }
    }

}
