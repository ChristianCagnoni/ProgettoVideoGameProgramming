using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossManager : MonoBehaviour
{

    public enum BossZombieStatus { live, death };

    private BossHealthBar bossBar;
    public int maxH;
    public static BossZombieStatus state;
    private GameObject gui;
    public GameObject portal;

    // Start is called before the first frame update
    void Start()
    {
        gui= GameObject.Find("GUI");
        bossBar = GameObject.Find("GUI").transform.GetChild(4).GetComponent<BossHealthBar>();
        state = BossZombieStatus.live;
        bossBar.SetMaxHealth(maxH);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManagerLogic.state != GameManagerLogic.State.pause && GameManagerLogic.state != GameManagerLogic.State.death)
        {
            if (bossBar.GetHealth() <= 0)
            {
                Instantiate(portal, new Vector3(258, 2.51f, 55), Quaternion.identity);
                state = BossZombieStatus.death;
                Destroy(gameObject);
                gui.transform.GetChild(4).gameObject.SetActive(false);
            }
        }
    }
}
