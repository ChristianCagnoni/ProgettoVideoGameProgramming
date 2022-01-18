using DigitalRuby.PyroParticles;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//script per la gestione del tutorial
public class TutorialManager : MonoBehaviour
{

    public enum tutState {pause,on};//stato del tutorial

    public static int tutorialPhase;
    public GameObject portal;
    public GameObject tutorialCanvas;
    public GameObject menu;
    private GameObject panel;
    private GameObject child;
    private bool[] pressed;
    private int falses;
    private Button repeat;
    private Button gioca;
    private bool wheel;
    public static tutState tut;

    // Start is called before the first frame update
    void Start()
    {
        tut = tutState.on;
        pressed = new bool[] { false, false, false, false};
        falses = 4;
        wheel = false;
        tutorialPhase = 0;
        tutorialCanvas.SetActive(true);
        panel= tutorialCanvas.transform.GetChild(0).gameObject;
        panel.SetActive(true);
        child=panel.transform.GetChild(tutorialPhase).gameObject;
        child.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        //se premi esc metti in pausa
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (tut != tutState.pause)
            {
                GameManagerLogic.state = GameManagerLogic.State.pause;
                tut = tutState.pause;
                Time.timeScale = 0f;
                menu.SetActive(true);
                InGameMenu.isInMenu = true;
                Cursor.lockState = CursorLockMode.Confined;
                Cursor.visible = true;
            }
            else if (tut == tutState.pause && InGameMenu.isInMenu)
            {
                GameManagerLogic.state = GameManagerLogic.State.game;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                tut = tutState.on;
                Time.timeScale = 1f;
                menu.SetActive(false);
                InGameMenu.isInMenu = false;
            }
        }
        //in base alla fase del tutorial fai cose diverse
        if (tutorialPhase == 0)
        {
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            {
                if (!pressed[0])
                    falses--;
                pressed[0] = true;
            }
            if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            {
                if (!pressed[1])
                    falses--;
                pressed[1] = true;
            }
            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                if (!pressed[2])
                    falses--;
                pressed[2] = true;
            }
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                if (!pressed[3])
                    falses--;
                pressed[3] = true;
            }
            if (falses == 0)
            {
                tutorialPhase++;
                child.SetActive(false);
                child = panel.transform.GetChild(tutorialPhase).gameObject;
                child.SetActive(true);
            }
        }
        else if (tutorialPhase == 1)
        {
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow) ||
                Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow) ||
                Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow) ||
                Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    tutorialPhase++;
                    child.SetActive(false);
                    child = panel.transform.GetChild(tutorialPhase).gameObject;
                    child.SetActive(true);
                }
            }
        }
        else if (tutorialPhase == 2)
        {
            StartCoroutine("waitEnemy");
        }
        else if (tutorialPhase == 3)
        {
            StartCoroutine("waitEnemy");
        }
        else if (tutorialPhase == 4)
        {
            if (Input.GetKey(KeyCode.Mouse0))
            {
                tutorialPhase++;
                child.SetActive(false);
                child = panel.transform.GetChild(tutorialPhase).gameObject;
                child.SetActive(true);
            }
        }
        else if (tutorialPhase == 5)
        {
            if (Input.GetAxis("Mouse ScrollWheel") != 0f)
            {
                wheel = true;
            }
            if(wheel && Input.GetKey(KeyCode.Mouse0))
            {
                tutorialPhase++;
                child.SetActive(false);
                child = panel.transform.GetChild(tutorialPhase).gameObject;
                child.SetActive(true);
                wheel = false;
            }
        }
        else if (tutorialPhase == 6)
        {
            if (Input.GetAxis("Mouse ScrollWheel") != 0f)
            {
                wheel = true;
            }
            if (wheel && Input.GetKey(KeyCode.Mouse0))
            {
                tutorialPhase++;
                child.SetActive(false);
                child = panel.transform.GetChild(tutorialPhase).gameObject;
                child.SetActive(true);
                wheel = false;
            }
        }
        else if (tutorialPhase == 7)
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                tutorialPhase++;
                child.SetActive(false);
                child = panel.transform.GetChild(tutorialPhase).gameObject;
                child.SetActive(true);
            }
        }else if (tutorialPhase == 8)
        {
            StartCoroutine("waitTime");
        }
        else if (tutorialPhase == 9)
        {
            StartCoroutine("waitTime");
        }
        else if (tutorialPhase == 10)
        {
            StartCoroutine("waitTime");
        }
        else if (tutorialPhase == 11)
        {
            portal.SetActive(true);
            if (Input.GetKey(KeyCode.Space))
            {
                SceneManager.LoadScene("Tutorial", LoadSceneMode.Single);
            }
        }
    }

    //passa al primo livello
    private void startGame()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        SceneManager.LoadScene("FirstChapter", LoadSceneMode.Single);
    }

    //ricomincia il tutorial
    void repeatTutorial()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        tutorialPhase = 0;
        child.SetActive(false);
        child = panel.transform.GetChild(tutorialPhase).gameObject;
        child.SetActive(true);
        pressed = new bool[] { false, false, false, false };
        falses = 4;
        Player.MouseLookToggle = true;
        wheel = false;
        repeat.onClick.RemoveListener(repeatTutorial);
    }

    //attendi 20 secondi per l'azione del nemico
    IEnumerator waitEnemy()
    {
        yield return new WaitForSeconds(20);
        tutorialPhase++;
        child.SetActive(false);
        child = panel.transform.GetChild(tutorialPhase).gameObject;
        child.SetActive(true);
        StopCoroutine("waitEnemy");
    }

    //attendi 3 secondi per leggere il testo
    IEnumerator waitTime()
    {
        yield return new WaitForSeconds(3);
        tutorialPhase++;
        Debug.Log(tutorialPhase);
        child.SetActive(false);
        child = panel.transform.GetChild(tutorialPhase).gameObject;
        child.SetActive(true);
        StopCoroutine("waitTime");
    }

}
