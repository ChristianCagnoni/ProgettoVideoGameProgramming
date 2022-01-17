using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script che gestisce il boss del primo livello
public class BossManager : MonoBehaviour
{

    public enum BossZombieStatus { live, death };//stati del boss (live o death)

    //parametri per la gestione del personaggio
    private BossHealthBar bossBar;
    public int maxH;
    public static BossZombieStatus state;
    //parametri relativi alle azioni e allo stato del personaggio
    private GameObject gui;
    public GameObject portal;

    // Start is called before the first frame update
    void Start()
    {
        //in base alla difficoltà influenza i parametri
        if (SettingsManager.difficulty == "easy")
        {
        }
        else if (SettingsManager.difficulty == "medium")
        {
            maxH += 50;
        }
        else
        {
            maxH += 100;
        }
        gui = GameObject.Find("GUI");
        bossBar = GameObject.Find("GUI").transform.GetChild(4).GetComponent<BossHealthBar>();
        state = BossZombieStatus.live;
        bossBar.SetMaxHealth(maxH);
    }

    // Update is called once per frame
    void Update()
    {
        //esegui il seguente blocco se il gioco non è in pausa
        if (GameManagerLogic.state != GameManagerLogic.State.pause && GameManagerLogic.state != GameManagerLogic.State.death)
        {
            //se vita minore o uguale a 0 fai le seguenti azioni
            if (bossBar.GetHealth() <= 0)
            {
                GameObject p=Instantiate(portal, new Vector3(258, 2.51f, 55), Quaternion.identity);
                p.transform.GetComponent<Portal>().nextChapter = "SecondChapter";
                state = BossZombieStatus.death;
                Destroy(gameObject);
                gui.transform.GetChild(4).gameObject.SetActive(false);
            }
        }
    }
}
