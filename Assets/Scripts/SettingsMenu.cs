using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{

    private Button b;
    private Button b1;
    private Button b2;
    public GameObject parent;

    // Start is called before the first frame update
    void Start()
    {
        //b = transform.GetChild(0).gameObject.GetComponent<Button>();
        //b1 = transform.GetChild(1).gameObject.GetComponent<Button>();
        //b2 = transform.GetChild(2).gameObject.GetComponent<Button>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            MenuStartupManager.actualMenu.SetActive(false);
            MenuStartupManager.actualMenu = parent;
            MenuStartupManager.actualMenu.SetActive(true);
        }
    }

}
