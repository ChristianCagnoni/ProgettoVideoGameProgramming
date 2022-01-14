using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InGameMenu : MonoBehaviour
{

    public Button resume;
    public Button reload;
    public Button settings;
    public Button close;
    public GameObject SettingsPage;
    public static bool isInMenu=false;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        resume.onClick.AddListener(Resume);
        reload.onClick.AddListener(Reload);
        settings.onClick.AddListener(Settings);
        close.onClick.AddListener(Exit);
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void Resume()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        transform.gameObject.SetActive(false);
        Time.timeScale = 1f;
        GameManagerLogic.state = GameManagerLogic.State.game;
        isInMenu = false;
    }

    private void Settings()
    {
        transform.gameObject.SetActive(false);
        SettingsPage.SetActive(true);
        isInMenu = false;
    }

    private void Exit()
    {
        isInMenu = false;
        GameManagerLogic.state = GameManagerLogic.State.start;
        Time.timeScale = 1f;
        SceneManager.LoadScene("StartMenu", LoadSceneMode.Single);
    }

    private void Reload()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        isInMenu = false;
        Scene scene = SceneManager.GetActiveScene();
        GameManagerLogic.state = GameManagerLogic.State.start;
        Time.timeScale = 1f;
        SceneManager.LoadScene(scene.name, LoadSceneMode.Single);
    }

}
