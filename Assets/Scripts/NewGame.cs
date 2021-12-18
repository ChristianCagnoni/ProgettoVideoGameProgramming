using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewGame : MonoBehaviour
{

    public GameObject parent;
    private Button backB;
    private Button confirmB;


    // Start is called before the first frame update
    void Start()
    {
        backB = transform.GetChild(4).gameObject.GetComponent<Button>();
        confirmB = transform.GetChild(5).gameObject.GetComponent<Button>();
        backB.onClick.AddListener(backButton);
    }

    private void backButton()
    {
        MenuStartupManager.actualMenu.SetActive(false);
        MenuStartupManager.actualMenu = parent;
        MenuStartupManager.actualMenu.SetActive(true);
    }


    // Update is called once per frame
    void Update()
    {
        

    }
}
