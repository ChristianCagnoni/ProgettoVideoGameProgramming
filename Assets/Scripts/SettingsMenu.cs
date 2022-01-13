using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SettingsMenu : MonoBehaviour
{

    public Toggle inverseX;
    public Toggle inverseY;
    public Toggle shoadowEnabled;
    public Dropdown res;
    public Dropdown AA;
    public Dropdown screen;
    public Dropdown shadowRes;
    public Dropdown shadowDist;
    public Slider quality;
    public InputField mouseSensibility;

    private Button b;
    private Button b1;
    private GameObject generalPanel;
    public GameObject parent;
    private GameObject gameB;
    private GameObject gameB1;
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
        b = gameB.GetComponent<Button>();
        b1 = gameB1.GetComponent<Button>(); 
        actual = gameB;
        b.Select();
        b.onClick.AddListener(selectButton1);
        b1.onClick.AddListener(selectButton2);
        scrollView= transform.GetChild(1).gameObject;
        rect = scrollView.GetComponent<ScrollRect>();
        index = 0;
        rect.content = contents[index].GetComponent<RectTransform>();
        mouseSens = GameObject.Find("MouseSensibility");
        sensibility = false;
        quality.onValueChanged.AddListener(delegate { changeQuality(); });
        res.onValueChanged.AddListener(delegate { changeRes(); });
        AA.onValueChanged.AddListener(delegate { changeAA(); });
        screen.onValueChanged.AddListener(delegate { changeScreen(); });
        shadowRes.onValueChanged.AddListener(delegate { changeShadowRes(); });
        shadowDist.onValueChanged.AddListener(delegate { changeShadowDist(); });
    }

    private void changeShadowDist()
    {
        int value = shadowDist.value;
        if (value == 0)
        {
            SettingsManager.distanceShadow = 10;
        }
        else if (value == 1)
        {
            SettingsManager.distanceShadow = 30;
        }
        else if (value == 2)
        {
            SettingsManager.distanceShadow = 50;
        }
        else if (value == 3)
        {
            SettingsManager.distanceShadow = 80;
        }
        else
        {
            SettingsManager.distanceShadow = 100;
        }
    }

    private void changeShadowRes()
    {
        int value = shadowRes.value;
        if (value == 0)
        {
            SettingsManager.resShadow = 256;
        }
        else if (value == 1)
        {
            SettingsManager.resShadow = 512;
        }
        else if (value == 2)
        {
            SettingsManager.resShadow = 1024;
        }
        else if (value == 3)
        {
            SettingsManager.resShadow = 2048;
        }
        else
        {
            SettingsManager.resShadow = 4096;
        }
    }

    private void changeScreen()
    {
        int value = screen.value;
        if (value == 0)
        {
            SettingsManager.full = true;
        }
        else
        {
            SettingsManager.full = false;
        }
    }

    private void changeAA()
    {
        int value = AA.value;
        if (value == 0)
        {
            SettingsManager.antiA = 0;
        }
        else if (value == 1)
        {
            SettingsManager.antiA = 2;
        }
        else if (value == 2)
        {
            SettingsManager.antiA = 4;
        }
        else
        {
            SettingsManager.antiA = 8;
        }
    }

    private void changeRes()
    {
        int value = res.value;
        if (value == 0)
        {
            SettingsManager.resW = 3840;
            SettingsManager.resH = 2160;
        }else if (value==1)
        {
            SettingsManager.resW = 2560;
            SettingsManager.resH = 1440;
        }
        else if (value==2)
        {
            SettingsManager.resW = 1920;
            SettingsManager.resH = 1080;
        }
        else
        {
            SettingsManager.resW = 1280;
            SettingsManager.resH = 720;
        }
    }

    private void changeQuality()
    {
        SettingsManager.quality = quality.value*100;
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

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !sensibility)
        {
            if (inverseX.isOn)
            {
                SettingsManager.invertX = true;
            }
            else
            {
                SettingsManager.invertX = false;
            }
            if (inverseY.isOn)
            {
                SettingsManager.invertY = true;
            }
            else
            {
                SettingsManager.invertY = false;
            }
            if (shoadowEnabled.isOn)
            {
                SettingsManager.shadowEnabled = true;
            }
            else
            {
                SettingsManager.shadowEnabled = false;
            }
            if (mouseSensibility.text != "")
            {
                SettingsManager.sensibility = float.Parse(mouseSensibility.text);
            }
            else
            {
                SettingsManager.sensibility = 10;
            }
            Debug.Log(SettingsManager.sensibility);
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
