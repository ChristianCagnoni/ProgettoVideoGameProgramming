using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using System.IO;

public class inGameSettings : MonoBehaviour
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
    public Toggle vsync;
    public TextMeshProUGUI qualityText;
    public Toggle fps;
    public Slider music;
    public TextMeshProUGUI musicText;
    public Slider playerS;
    public TextMeshProUGUI playerSText;
    public Slider enemyS;
    public TextMeshProUGUI enemySText;
    public Button esc;

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
        loadSettings();
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
        esc.onClick.AddListener(CloseSettings);
        scrollView = transform.GetChild(1).gameObject;
        rect = scrollView.GetComponent<ScrollRect>();
        index = 0;
        rect.content = contents[index].GetComponent<RectTransform>();
        mouseSens = GameObject.Find("MouseSensibility");
        sensibility = false;
        qualityText.text = SettingsManager.quality.ToString()+"%";
        quality.onValueChanged.AddListener(delegate { changeQuality(); });
        musicText.text = SettingsManager.music.ToString() + "%";
        music.onValueChanged.AddListener(delegate { changeMusic(); });
        playerSText.text = SettingsManager.playerSound.ToString() + "%";
        playerS.onValueChanged.AddListener(delegate { changePlayerS(); });
        enemySText.text = SettingsManager.enemySound.ToString() + "%";
        enemyS.onValueChanged.AddListener(delegate { changeEnemyS(); });
        res.onValueChanged.AddListener(delegate { changeRes(); });
        AA.onValueChanged.AddListener(delegate { changeAA(); });
        screen.onValueChanged.AddListener(delegate { changeScreen(); });
        shadowRes.onValueChanged.AddListener(delegate { changeShadowRes(); });
        shadowDist.onValueChanged.AddListener(delegate { changeShadowDist(); });
    }

    private void CloseSettings()
    {
        if (fps.isOn)
        {
            SettingsManager.fps = true;
        }
        else
        {
            SettingsManager.fps = false;
        }
        if (vsync.isOn)
        {
            SettingsManager.vsync = true;
            QualitySettings.vSyncCount = 1;
        }
        else
        {
            SettingsManager.vsync = false;
            QualitySettings.vSyncCount = 0;
        }
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
        transform.gameObject.SetActive(false);
        parent.SetActive(true);
        InGameMenu.isInMenu = true;

        if (shoadowEnabled.isOn)
        {
            if (SettingsManager.resShadow == 256)
            {
                SettingsManager.changeUrp(0);
            }
            else if (SettingsManager.resShadow == 512)
            {
                SettingsManager.changeUrp(1);
            }
            else if (SettingsManager.resShadow == 1024)
            {
                SettingsManager.changeUrp(2);
            }
            else if (SettingsManager.resShadow == 2048)
            {
                SettingsManager.changeUrp(3);
            }
            else if (SettingsManager.resShadow == 4096)
            {
                SettingsManager.changeUrp(4);
            }
        }
        else
        {
            SettingsManager.changeUrp(5);
        }

        SettingsManager.changeConfig(SettingsManager.antiA, SettingsManager.quality / 100, (int)SettingsManager.distanceShadow);
        saveConfig();
    }

    private void changeMusic()
    {
        SettingsManager.music = music.value * 100;
        musicText.text = SettingsManager.music.ToString() + "%";
    }

    private void changeEnemyS()
    {
        SettingsManager.enemySound = enemyS.value * 100;
        enemySText.text = SettingsManager.enemySound.ToString() + "%";
    }

    private void changePlayerS()
    {
        SettingsManager.playerSound = playerS.value * 100;
        playerSText.text = SettingsManager.playerSound.ToString() + "%";
    }

    private void loadSettings()
    {
        inverseX.isOn = SettingsManager.invertX;
        inverseY.isOn = SettingsManager.invertY;
        shoadowEnabled.isOn = SettingsManager.shadowEnabled;
        if (SettingsManager.resH == 2160)
        {
            res.value = 0;
        }
        else if (SettingsManager.resH == 1440)
        {
            res.value = 1;
        }
        else if (SettingsManager.resH == 1080)
        {
            res.value = 2;
        }
        else
        {
            res.value = 3;
        }
        if (SettingsManager.antiA == 0)
        {
            AA.value = 0;
        }
        else if (SettingsManager.antiA == 2)
        {
            AA.value = 1;
        }
        else if (SettingsManager.antiA == 4)
        {
            AA.value = 2;
        }
        else
        {
            AA.value = 3;
        }
        if (SettingsManager.full)
        {
            screen.value = 0;
        }
        else
        {
            screen.value = 1;
        }
        if (SettingsManager.resShadow == 256)
        {
            shadowRes.value = 0;
        }
        else if (SettingsManager.resShadow == 512)
        {
            shadowRes.value = 1;
        }
        else if (SettingsManager.resShadow == 1024)
        {
            shadowRes.value = 2;
        }
        else if (SettingsManager.resShadow == 2048)
        {
            shadowRes.value = 3;
        }
        else
        {
            shadowRes.value = 4;
        }
        if (SettingsManager.distanceShadow == 10)
        {
            shadowDist.value = 0;
        }
        else if (SettingsManager.distanceShadow == 30)
        {
            shadowDist.value = 1;
        }
        else if (SettingsManager.distanceShadow == 50)
        {
            shadowDist.value = 2;
        }
        else if (SettingsManager.distanceShadow == 80)
        {
            shadowDist.value = 3;
        }
        else
        {
            shadowDist.value = 4;
        }
        quality.value = SettingsManager.quality / 100;
        qualityText.text = SettingsManager.quality.ToString()+"%";

        music.value = SettingsManager.music / 100;
        musicText.text = SettingsManager.music.ToString() + "%";
        playerS.value = SettingsManager.playerSound / 100;
        playerSText.text = SettingsManager.playerSound.ToString() + "%";
        enemyS.value = SettingsManager.enemySound / 100;
        enemySText.text = SettingsManager.enemySound.ToString() + "%";

        mouseSensibility.text = SettingsManager.sensibility.ToString();
        vsync.isOn = SettingsManager.vsync;
        fps.isOn=SettingsManager.fps;
        if (vsync.isOn)
        {
            QualitySettings.vSyncCount = 1;
        }
        else
        {
            QualitySettings.vSyncCount = 0;
        }
        Screen.SetResolution(SettingsManager.resW, SettingsManager.resH, SettingsManager.full);
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
        Screen.SetResolution(SettingsManager.resW, SettingsManager.resH, SettingsManager.full);
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
        }
        else if (value == 1)
        {
            SettingsManager.resW = 2560;
            SettingsManager.resH = 1440;
        }
        else if (value == 2)
        {
            SettingsManager.resW = 1920;
            SettingsManager.resH = 1080;
        }
        else
        {
            SettingsManager.resW = 1280;
            SettingsManager.resH = 720;
        }
        Screen.SetResolution(SettingsManager.resW, SettingsManager.resH, SettingsManager.full);
    }

    private void changeQuality()
    {
        SettingsManager.quality = quality.value * 100;
        qualityText.text = SettingsManager.quality.ToString()+"%";
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
            if (fps.isOn)
            {
                SettingsManager.fps = true;
            }
            else
            {
                SettingsManager.fps = false;
            }
            if (vsync.isOn)
            {
                SettingsManager.vsync = true;
                QualitySettings.vSyncCount = 1;
            }
            else
            {
                SettingsManager.vsync = false;
                QualitySettings.vSyncCount = 0;
            }
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
            transform.gameObject.SetActive(false);
            parent.SetActive(true);
            InGameMenu.isInMenu = true;

            if (shoadowEnabled.isOn)
            {
                if (SettingsManager.resShadow == 256)
                {
                    SettingsManager.changeUrp(0);
                }
                else if (SettingsManager.resShadow == 512)
                {
                    SettingsManager.changeUrp(1);
                }
                else if (SettingsManager.resShadow == 1024)
                {
                    SettingsManager.changeUrp(2);
                }
                else if (SettingsManager.resShadow == 2048)
                {
                    SettingsManager.changeUrp(3);
                }
                else if (SettingsManager.resShadow == 4096)
                {
                    SettingsManager.changeUrp(4);
                }
            }
            else
            {
                SettingsManager.changeUrp(5);
            }

            SettingsManager.changeConfig(SettingsManager.antiA, SettingsManager.quality / 100, (int)SettingsManager.distanceShadow);
            saveConfig();
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && sensibility)
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

    private void saveConfig()
    {
        if (!Directory.Exists(SettingsManager.configPath))
        {
            Directory.CreateDirectory(SettingsManager.configPath);
            using (StreamWriter sw = new StreamWriter(File.Open(SettingsManager.configFile, System.IO.FileMode.Open)))
            {
                sw.WriteLine(SettingsManager.character.ToString());
                sw.WriteLine(SettingsManager.sensibility.ToString());
                sw.WriteLine(SettingsManager.invertX.ToString());
                sw.WriteLine(SettingsManager.invertY.ToString());
                sw.WriteLine(SettingsManager.resH.ToString());
                sw.WriteLine(SettingsManager.resW.ToString());
                sw.WriteLine(SettingsManager.antiA.ToString());
                sw.WriteLine(SettingsManager.full.ToString());
                sw.WriteLine(SettingsManager.resShadow.ToString());
                sw.WriteLine(SettingsManager.distanceShadow.ToString());
                sw.WriteLine(SettingsManager.shadowEnabled.ToString());
                sw.WriteLine(SettingsManager.quality.ToString());
                sw.WriteLine(SettingsManager.vsync.ToString());
                sw.WriteLine(SettingsManager.fps.ToString());
                sw.WriteLine(SettingsManager.music.ToString());
                sw.WriteLine(SettingsManager.playerSound.ToString());
                sw.WriteLine(SettingsManager.enemySound.ToString());
                sw.WriteLine(SettingsManager.defURP.ToString());
                sw.Close();
            }
        }
        else
        {
            using (StreamWriter sw = new StreamWriter(File.Open(SettingsManager.configFile, System.IO.FileMode.Open)))
            {
                sw.WriteLine(SettingsManager.character.ToString());
                sw.WriteLine(SettingsManager.sensibility.ToString());
                sw.WriteLine(SettingsManager.invertX.ToString());
                sw.WriteLine(SettingsManager.invertY.ToString());
                sw.WriteLine(SettingsManager.resH.ToString());
                sw.WriteLine(SettingsManager.resW.ToString());
                sw.WriteLine(SettingsManager.antiA.ToString());
                sw.WriteLine(SettingsManager.full.ToString());
                sw.WriteLine(SettingsManager.resShadow.ToString());
                sw.WriteLine(SettingsManager.distanceShadow.ToString());
                sw.WriteLine(SettingsManager.shadowEnabled.ToString());
                sw.WriteLine(SettingsManager.quality.ToString());
                sw.WriteLine(SettingsManager.vsync.ToString());
                sw.WriteLine(SettingsManager.fps.ToString());
                sw.WriteLine(SettingsManager.music.ToString());
                sw.WriteLine(SettingsManager.playerSound.ToString());
                sw.WriteLine(SettingsManager.enemySound.ToString());
                sw.WriteLine(SettingsManager.defURP.ToString());
                sw.Close();
            }
        }
    }

}
