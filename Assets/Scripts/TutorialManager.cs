using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{

    public static int tutorialPhase;
    public GameObject tutorialCanvas;
    private GameObject panel;
    private GameObject child;
    private bool[] pressed;
    private int falses;

    // Start is called before the first frame update
    void Start()
    {
        pressed = new bool[] { false, false, false, false};
        falses = 4;
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
                child= panel.transform.GetChild(tutorialPhase).gameObject;
                child.SetActive(true);
            }
        }else if (tutorialPhase == 1)
        {
            if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow) || 
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
            StartCoroutine("waitEnemy");
        }
    }

    IEnumerator waitEnemy()
    {
        yield return new WaitForSeconds(5);
        tutorialPhase++;
        child.SetActive(false);
        child = panel.transform.GetChild(tutorialPhase).gameObject;
        child.SetActive(true);
        StopCoroutine("waitEnemy");
    }

}
