using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsManager : MonoBehaviour
{

    public static float sensibility;
    public static bool invertX;
    public static bool invertY;
    public static float resW;
    public static float resH;
    public static float antiA;
    public static bool full;
    public static float resShadow;
    public static float distanceShadow;
    public static bool shadowEnabled;
    public static float quality;
    public static string difficult;
    public static string characterName;
    public static int character;

    // Start is called before the first frame update
    void Start()
    {
        difficult = "easy";
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
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
