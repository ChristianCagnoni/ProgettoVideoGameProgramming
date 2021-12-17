using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SettingsMenu : MonoBehaviour
{

    private Button b;
    private Button b1;
    private Button b2;
    private GameObject generalPanel;
    public GameObject parent;
    private GameObject gameB;
    private GameObject gameB1;
    private GameObject gameB2;
    private GameObject actual;
    private GameObject scrollView;
    private ScrollRect rect;
    public GameObject[] contents;
    private int index;
    private GameObject mouseSens;
    private bool sensibility;

    // Start is called before the first frame update
    void Start()
    {
        generalPanel = transform.GetChild(0).gameObject;
        gameB = generalPanel.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject;
        gameB1 = generalPanel.transform.GetChild(0).gameObject.transform.GetChild(1).gameObject;
        gameB2 = generalPanel.transform.GetChild(0).gameObject.transform.GetChild(2).gameObject;
        b = gameB.GetComponent<Button>();
        b1 = gameB1.GetComponent<Button>(); 
        b2 = gameB2.GetComponent<Button>();
        actual = gameB;
        b.Select();
        b.onClick.AddListener(selectButton1);
        b1.onClick.AddListener(selectButton2);
        b2.onClick.AddListener(selectButton3);
        scrollView= transform.GetChild(1).gameObject;
        rect = scrollView.GetComponent<ScrollRect>();
        index = 0;
        rect.content = contents[index].GetComponent<RectTransform>();
        mouseSens = GameObject.Find("MouseSensibility");
        sensibility = false;
    }

    private void selectButton1()
    {
        contents[index].SetActive(false);
        index = 0;
        rect.content = contents[index].GetComponent<RectTransform>();
        contents[index].SetActive(true);
        actual = gameB;
        b.Select();
    }

    private void selectButton2()
    {
        contents[index].SetActive(false);
        index = 1;
        rect.content = contents[index].GetComponent<RectTransform>();
        contents[index].SetActive(true);
        actual = gameB1;
        b1.Select();
    }

    private void selectButton3()
    {
        contents[index].SetActive(false);
        index = 2;
        rect.content = contents[index].GetComponent<RectTransform>();
        contents[index].SetActive(true);
        actual = gameB2;
        b2.Select();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !sensibility)
        {
            MenuStartupManager.actualMenu.SetActive(false);
            MenuStartupManager.actualMenu = parent;
            MenuStartupManager.actualMenu.SetActive(true);
        }else if(Input.GetKeyDown(KeyCode.Escape) && sensibility)
        {
            EventSystem.current.SetSelectedGameObject(actual);
            sensibility = false;
        }
        else if (Input.GetKeyDown(KeyCode.Return) && sensibility)
        {
            EventSystem.current.SetSelectedGameObject(actual);
            sensibility = false;
        }

        if (EventSystem.current.currentSelectedGameObject == mouseSens)
        {
            sensibility = true;
        }
        else if (EventSystem.current.currentSelectedGameObject != actual)
        {
            EventSystem.current.SetSelectedGameObject(actual);
            sensibility = false;
        }

    }

}
