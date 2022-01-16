using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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
        if (SettingsManager.fps)
        {
            fpsPanal.SetActive(true);
            int frame = (int)(1.0f / Time.deltaTime);
            fpsCounter.text = frame.ToString();
        }
        else
        {
            fpsPanal.SetActive(false);
        }
    }
}
