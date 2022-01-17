using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


//script che gestisce le diverse fasi del gioco
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
    private Scene scene;


    // Start is called before the first frame update
    void Start()
    {
        spawn= false;
        state=State.start;
        StartCoroutine("startGame");
        scene= SceneManager.GetActiveScene();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))//se premuto esc
        {
            if (state != State.pause)//e non in pausa vai in pausa
            {
                state = State.pause;
                Time.timeScale = 0f;
                menu.SetActive(true);
                InGameMenu.isInMenu = true;
                Cursor.lockState = CursorLockMode.Confined;
                Cursor.visible = true;
            }
            else if(state==State.pause && InGameMenu.isInMenu)//altrimenti riprendi il gioco
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                state = State.game;
                Time.timeScale = 1f;
                menu.SetActive(false);
                InGameMenu.isInMenu = false;
            }
        }
        if (healthBar.GetHealth() <= 0)//se vita personaggio minore o uguale a 0 attiva la schermata di sconfitta
        {
            state = State.death;
            Time.timeScale = 0f;
            deathCanvas.SetActive(true);
        }
        if (scene.name == "FirstChapter")//se la scena è la prima gestisci anche lo spawn del boss
        {
            if (PortalBarrier.inPortalZone && !spawn)
            {
                spawn = true;
                StartCoroutine("PortalZone");
            }
        }
    }

    //metodo per lo spawn del boss
    IEnumerator PortalZone()
    {
        Instantiate(zombiePrefab, new Vector3(258, 3.25f, 55), Quaternion.identity);
        yield return null;
    }

    //metodo chiamato al lancio
    IEnumerator startGame()
    {
        state = State.game;
        Time.timeScale = 1f;
        yield return new WaitForSeconds(5);
    }
}
