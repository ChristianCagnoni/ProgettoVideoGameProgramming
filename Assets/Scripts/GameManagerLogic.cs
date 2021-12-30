using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerLogic : MonoBehaviour
{

    public enum State{start,pause,game};
    public GameObject portal;
    public GameObject gui;
    public static State state;

    private Text text;
    private GameObject textBox;

    // Start is called before the first frame update
    void Start()
    {
        state=State.start;
        textBox = gui.transform.GetChild(4).gameObject;
        text =textBox.GetComponent<Text>();
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
            }
            else
            {
                state = State.game;
                Time.timeScale = 1f;
            }
        }
    }

    IEnumerator startGame()
    {
        yield return new WaitForSeconds(5);
        state = State.game;
        text.text = "Scappa e prova a nasconderti dai nemici";
        textBox.SetActive(true);
        yield return new WaitForSeconds(5);
        textBox.SetActive(false);
        yield return new WaitForSeconds(125);
        text.text = "Un portale è apparso, usalo per salvarti";
        textBox.SetActive(true);
        Instantiate(portal,new Vector3(258, 3.25f, 55), Quaternion.identity);
        yield return new WaitForSeconds(5);
        textBox.SetActive(false);
    }
}
