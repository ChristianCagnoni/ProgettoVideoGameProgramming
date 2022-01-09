using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerLogic : MonoBehaviour
{

    public enum State{start,pause,game,death};
    public GameObject portal;
    public static State state;
    public HealthBar healthBar;
    public GameObject deathCanvas;
    public GameObject zombiePrefab;
    public GameObject menu;

    private bool spawn;


    // Start is called before the first frame update
    void Start()
    {
        spawn= false;
        state=State.start;
        StartCoroutine("startGame");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (state != State.pause)
            {
                state = State.pause;
                Time.timeScale = 0f;
                menu.SetActive(true);
                InGameMenu.isInMenu = true;
            }
            else if(state==State.pause && InGameMenu.isInMenu)
            {
                state = State.game;
                Time.timeScale = 1f;
                menu.SetActive(false);
                InGameMenu.isInMenu = false;
            }
        }
        if (healthBar.GetHealth() <= 0)
        {
            state = State.death;
            Time.timeScale = 0f;
            deathCanvas.SetActive(true);
        }
        if (PortalBarrier.inPortalZone && !spawn)
        {
            spawn = true;
            StartCoroutine("PortalZone");
        }
    }

    IEnumerator PortalZone()
    {
        Instantiate(zombiePrefab, new Vector3(258, 3.25f, 55), Quaternion.identity);
        //Instantiate(portal, new Vector3(258, 3.25f, 55), Quaternion.identity);
        yield return null;
    }

    IEnumerator startGame()
    {
        state = State.game;
        Time.timeScale = 1f;
        yield return new WaitForSeconds(5);
    }
}
