using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class MenuStartupManager : MonoBehaviour
{

    public static GameObject actualMenu;
    private Button continueB;
    private Button newGameB;
    private Button settingsB;
    private Button exitB;
    public GameObject start;
    public GameObject newGame;
    public GameObject settings;
    public GameObject bg;
    private string savesPath;

    // Start is called before the first frame update
    void Start()
    {
        actualMenu = start;
        continueB = actualMenu.transform.GetChild(0).gameObject.GetComponent<Button>();
        newGameB = actualMenu.transform.GetChild(1).gameObject.GetComponent<Button>();
        settingsB = actualMenu.transform.GetChild(2).gameObject.GetComponent<Button>();
        exitB = actualMenu.transform.GetChild(3).gameObject.GetComponent<Button>();
        exitB.onClick.AddListener(CloseGame);
        settingsB.onClick.AddListener(startSetttings);
        newGameB.onClick.AddListener(newGameClick);
        savesPath = Directory.GetCurrentDirectory() + "\\Saves";
        if (!Directory.Exists(savesPath))
        {
            continueB.interactable = false;
        }
        else
        {
            continueB.onClick.AddListener(continueGame);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    void continueGame()
    {

    }

    void newGameClick()
    {
        actualMenu.SetActive(false);
        actualMenu = newGame;
        actualMenu.SetActive(true);
    }

    void startSetttings()
    {
        actualMenu.SetActive(false);
        actualMenu = settings;
        actualMenu.SetActive(true);
    }

    void CloseGame()
    {
        Application.Quit();
    }
}
