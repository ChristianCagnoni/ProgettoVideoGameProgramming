using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//script per la gestione della scena finale
public class Extended : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))//se esc premuto torna alla schermata principale
        {
            SceneManager.LoadScene("StartMenu", LoadSceneMode.Single);
        }
    }
}
