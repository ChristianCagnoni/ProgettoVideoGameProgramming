using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NewGame : MonoBehaviour
{

    public GameObject parent;
    public Toggle toggle;

    private Button backB;
    private Button confirmB;


    // Start is called before the first frame update
    void Start()
    {
        backB = transform.GetChild(4).gameObject.GetComponent<Button>();
        confirmB = transform.GetChild(5).gameObject.GetComponent<Button>();
        backB.onClick.AddListener(backButton);
        confirmB.onClick.AddListener(confirmButton);
    }

    private void backButton()
    {
        MenuStartupManager.actualMenu.SetActive(false);
        MenuStartupManager.actualMenu = parent;
        MenuStartupManager.actualMenu.SetActive(true);
    }

    private void confirmButton()
    {
        MenuStartupManager.actualMenu.SetActive(false);
        if(toggle.isOn)
            SceneManager.LoadScene("Tutorial", LoadSceneMode.Single);
        else
        {
            SceneManager.LoadScene("FirstChapter", LoadSceneMode.Single);
        }
    }


    // Update is called once per frame
    void Update()
    {
        

    }
}
