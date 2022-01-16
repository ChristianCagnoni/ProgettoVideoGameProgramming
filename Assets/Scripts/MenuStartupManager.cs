using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
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

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        actualMenu = start;
        continueB = actualMenu.transform.GetChild(0).gameObject.GetComponent<Button>();
        newGameB = actualMenu.transform.GetChild(1).gameObject.GetComponent<Button>();
        settingsB = actualMenu.transform.GetChild(2).gameObject.GetComponent<Button>();
        exitB = actualMenu.transform.GetChild(3).gameObject.GetComponent<Button>();
        exitB.onClick.AddListener(CloseGame);
        settingsB.onClick.AddListener(startSetttings);
        newGameB.onClick.AddListener(newGameClick);
        if (!Directory.Exists(SettingsManager.savesPath))
        {
            if(!File.Exists(SettingsManager.saveFile))
                continueB.interactable = false;
        }
        else
        {
            if (!File.Exists(SettingsManager.saveFile))
                continueB.interactable = false;
            else
            {
                continueB.onClick.AddListener(continueGame);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    void continueGame()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        string scene;
        int character;
        string difficulty;
        using (StreamReader sw = new StreamReader(File.Open(SettingsManager.saveFile, System.IO.FileMode.Open)))
        {
            string tmp = sw.ReadToEnd();
            string[] sv = tmp.Split('\n');
            scene = sv[0];
            character = int.Parse(sv[1]);
            difficulty = sv[2];
            sw.Close();
        }
        SettingsManager.character = character;
        SettingsManager.difficulty = difficulty;
        SceneManager.LoadScene(scene.Substring(0,scene.Length-1),LoadSceneMode.Single);
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
        Application.Quit();
    }
}
