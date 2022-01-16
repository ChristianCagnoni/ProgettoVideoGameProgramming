using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{

    private AudioSource musicSource;

    // Start is called before the first frame update
    void Start()
    {
        musicSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (SettingsManager.music != musicSource.volume)
        {
            if (gameObject.CompareTag("Music"))
            {
                musicSource.volume = SettingsManager.music / 100;
            }
            else if (gameObject.CompareTag("Player"))
            {
                musicSource.volume = SettingsManager.playerSound / 100;
            }
            else if (gameObject.CompareTag("Enemy"))
            {
                musicSource.volume = SettingsManager.enemySound / 100;
            }
        }
    }
}
