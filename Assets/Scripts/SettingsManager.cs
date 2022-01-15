using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SettingsManager : MonoBehaviour
{

    public static float sensibility;
    public static bool invertX;
    public static bool invertY;
    public static int resW;
    public static int resH;
    public static int antiA;
    public static bool full;
    public static int resShadow;
    public static float distanceShadow;
    public static bool shadowEnabled;
    public static float quality;
    public static string difficulty;
    public static string characterName;
    public static int character;
    public static bool vsync;
    public static bool fps;
    public static float music;
    public static float playerSound;
    public static float enemySound;

    public static string configPath;
    public static string configFile;
    public static string savesPath;
    public static string saveFile;

    private void Awake()
    {
        savesPath = Directory.GetCurrentDirectory() + "\\Saves";
        saveFile = savesPath + "\\save.sv";
        configPath = Directory.GetCurrentDirectory() + "\\Config";
        if (!Directory.Exists(configPath))
        {
            Directory.CreateDirectory(configPath);
            configFile = configPath + "\\config.cfg";
            using (StreamWriter sw = new StreamWriter(File.Open(SettingsManager.configFile, System.IO.FileMode.Create)))
            {
                sw.WriteLine(0);
                sw.WriteLine(10);
                sw.WriteLine(false);
                sw.WriteLine(false);
                sw.WriteLine(1080);
                sw.WriteLine(1920);
                sw.WriteLine(0);
                sw.WriteLine(true);
                sw.WriteLine(256);
                sw.WriteLine(10);
                sw.WriteLine(true);
                sw.WriteLine(100);
                sw.WriteLine(true);
                sw.WriteLine(true);
                sw.WriteLine(false);
                sw.WriteLine(100);
                sw.WriteLine(100);
                sw.WriteLine(100);
                sw.Close();
            }
            using (StreamReader sw = new StreamReader(File.Open(configFile, System.IO.FileMode.Open)))
            {
                string tmp = sw.ReadToEnd();
                string[] cfg = tmp.Split('\n');
                difficulty = "easy";
                characterName = "";
                character = int.Parse(cfg[0]);
                sensibility = int.Parse(cfg[1]);
                invertX = cfg[2][0] != 'F';
                invertY = cfg[3][0] != 'F';
                resH = int.Parse(cfg[4]);
                resW = int.Parse(cfg[5]);
                antiA = int.Parse(cfg[6]);
                full = cfg[7][0] != 'F';
                resShadow = int.Parse(cfg[8]);
                distanceShadow = int.Parse(cfg[9]);
                shadowEnabled = cfg[10][0] != 'F';
                quality = float.Parse(cfg[11]);
                vsync = cfg[12][0] != 'F';
                fps= cfg[13][0] != 'F';
                music = float.Parse(cfg[14]);
                playerSound = float.Parse(cfg[15]);
                enemySound = float.Parse(cfg[16]);
            }
        }
        else
        {
            configFile = configPath + "\\config.cfg";
            if (File.Exists(configFile))
            {
                using (StreamReader sw = new StreamReader(File.Open(configFile, System.IO.FileMode.Open)))
                {
                    string tmp = sw.ReadToEnd();
                    string[] cfg = tmp.Split('\n');
                    difficulty = "easy";
                    characterName = "";
                    character = int.Parse(cfg[0]);
                    sensibility = int.Parse(cfg[1]);
                    invertX = cfg[2][0] != 'F';
                    invertY = cfg[3][0] != 'F';
                    resH = int.Parse(cfg[4]);
                    resW = int.Parse(cfg[5]);
                    antiA = int.Parse(cfg[6]);
                    full = cfg[7][0] != 'F';
                    resShadow = int.Parse(cfg[8]);
                    distanceShadow = int.Parse(cfg[9]);
                    shadowEnabled = cfg[10][0] != 'F';
                    quality = float.Parse(cfg[11]);
                    vsync = cfg[12][0] != 'F';
                    fps = cfg[13][0] != 'F';
                    fps = cfg[13][0] != 'F';
                    music = float.Parse(cfg[14]);
                    playerSound = float.Parse(cfg[15]);
                    enemySound = float.Parse(cfg[16]);
                    sw.Close();
                }
            }
            else
            {
                using (StreamWriter sw = new StreamWriter(File.Open(SettingsManager.configFile, System.IO.FileMode.Create)))
                {
                    sw.WriteLine(0);
                    sw.WriteLine(10);
                    sw.WriteLine(false);
                    sw.WriteLine(false);
                    sw.WriteLine(1080);
                    sw.WriteLine(1920);
                    sw.WriteLine(0);
                    sw.WriteLine(true);
                    sw.WriteLine(256);
                    sw.WriteLine(10);
                    sw.WriteLine(true);
                    sw.WriteLine(100);
                    sw.WriteLine(true);
                    sw.WriteLine(false);
                    sw.WriteLine(100);
                    sw.WriteLine(100);
                    sw.WriteLine(100);
                    sw.Close();
                }
                using (StreamReader sw = new StreamReader(File.Open(configFile, System.IO.FileMode.Open)))
                {
                    string tmp = sw.ReadToEnd();
                    string[] cfg = tmp.Split('\n');
                    difficulty = "easy";
                    characterName = "";
                    character = int.Parse(cfg[0]);
                    sensibility = int.Parse(cfg[1]);
                    invertX = cfg[2][0] != 'F';
                    invertY = cfg[3][0] != 'F';
                    resH = int.Parse(cfg[4]);
                    resW = int.Parse(cfg[5]);
                    antiA = int.Parse(cfg[6]);
                    full = cfg[7][0] != 'F';
                    resShadow = int.Parse(cfg[8]);
                    distanceShadow = int.Parse(cfg[9]);
                    shadowEnabled = cfg[10][0] != 'F';
                    quality = float.Parse(cfg[11]);
                    vsync = cfg[12][0] != 'F';
                    fps = cfg[13][0] != 'F';
                    music = float.Parse(cfg[14]);
                    playerSound = float.Parse(cfg[15]);
                    enemySound = float.Parse(cfg[16]);
                    sw.Close();
                }
            }
        }
        if (Display.main.renderingWidth != resW)
        {
            resW = Display.main.renderingWidth;
            resH = Display.main.renderingHeight;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        /*difficulty = "easy";
        characterName = "";
        character = 0;
        sensibility = 10;
        invertX = false;
        invertY = false;
        resH = 1080;
        resW = 1920;
        antiA = 0;
        full = true;
        resShadow = 256;
        distanceShadow = 10;
        shadowEnabled = true;
        quality = 100;
        vsync = true;*/
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
