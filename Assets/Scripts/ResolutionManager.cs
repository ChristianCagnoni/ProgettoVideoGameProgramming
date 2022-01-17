using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//script che gestisce il cambio di risoluzione cambiando la risoluzione di riferimento dei canvas
public class ResolutionManager : MonoBehaviour
{

    public CanvasScaler scaler;
    public CanvasScaler scaler1;
    public CanvasScaler scaler2;
    public CanvasScaler scaler3;

    // Start is called before the first frame update
    void Start()
    {
        if(scaler)
            scaler.referenceResolution = new Vector2(SettingsManager.resW, SettingsManager.resH);
        if (scaler1)
            scaler1.referenceResolution = new Vector2(SettingsManager.resW, SettingsManager.resH);
        if (scaler2)
            scaler2.referenceResolution = new Vector2(SettingsManager.resW, SettingsManager.resH);
        if (scaler3)
            scaler3.referenceResolution = new Vector2(SettingsManager.resW, SettingsManager.resH);
    }

    // Update is called once per frame
    void Update()
    {
        if (scaler)
            if (!scaler.referenceResolution.Equals(new Vector2(SettingsManager.resW, SettingsManager.resH)))
            {
                scaler.referenceResolution = new Vector2(SettingsManager.resW, SettingsManager.resH);
            }
        if (scaler1)
            if (!scaler1.referenceResolution.Equals(new Vector2(SettingsManager.resW, SettingsManager.resH)))
            {
                scaler1.referenceResolution = new Vector2(SettingsManager.resW, SettingsManager.resH);
            }
        if (scaler2)
            if (!scaler2.referenceResolution.Equals(new Vector2(SettingsManager.resW, SettingsManager.resH)))
            {
                scaler2.referenceResolution = new Vector2(SettingsManager.resW, SettingsManager.resH);
            }
        if (scaler3)
            if (!scaler3.referenceResolution.Equals(new Vector2(SettingsManager.resW, SettingsManager.resH)))
            {
                scaler3.referenceResolution = new Vector2(SettingsManager.resW, SettingsManager.resH);
            }
    }
}
