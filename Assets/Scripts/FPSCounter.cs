using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//Script per il calcolo degli fps
public class FPSCounter : MonoBehaviour
{

    public TextMeshProUGUI fpsCounter;
    public GameObject fpsPanal;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (SettingsManager.fps)//se opzione fps attiva calcola gli fps e rendi visibile il pannello
        {
            fpsPanal.SetActive(true);
            int frame = (int)(1.0f / Time.deltaTime);
            fpsCounter.text = frame.ToString();
        }
        else//altrimenti nascondi il pannello
        {
            fpsPanal.SetActive(false);
        }
    }
}
