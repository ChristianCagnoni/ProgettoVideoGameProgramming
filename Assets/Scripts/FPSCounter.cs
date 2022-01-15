using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FPSCounter : MonoBehaviour
{

    public TextMeshProUGUI fpsCounter;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (SettingsManager.fps)
        {
            gameObject.SetActive(true);
            int frame = (int)(1.0f / Time.deltaTime);
            Debug.Log(frame);
            fpsCounter.text = frame.ToString();
        }
        else
        {
            gameObject.SetActive(true);
        }
    }
}
