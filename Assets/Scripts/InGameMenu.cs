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

    // Start is called before the first frame update
    void Start()
    {
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
        transform.gameObject.SetActive(false);
        Time.timeScale = 1f;
    }

    private void Settings()
    {

    }

    private void Exit()
    {
        GameManagerLogic.state = GameManagerLogic.State.start;
        Time.timeScale = 1f;
        SceneManager.LoadScene("StartMenu", LoadSceneMode.Single);
    }

    private void Reload()
    {
        Scene scene = SceneManager.GetActiveScene();
        GameManagerLogic.state = GameManagerLogic.State.start;
        Time.timeScale = 1f;
        SceneManager.LoadScene(scene.name, LoadSceneMode.Single);
    }

}
