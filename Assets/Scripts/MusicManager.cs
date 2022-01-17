using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//script per la gestione della musica
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
        //in base al tag cambia parametri diversi
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
