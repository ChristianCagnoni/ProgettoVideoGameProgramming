using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DeathPlayer : MonoBehaviour
{

    public Button restart;
    public Button close;

    // Start is called before the first frame update
    void Start()
    {
        restart.onClick.AddListener(Reload);
        close.onClick.AddListener(Exit);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Reload()
    {
        Scene scene = SceneManager.GetActiveScene();
        GameManagerLogic.state = GameManagerLogic.State.start;
        Time.timeScale = 1f;
        SceneManager.LoadScene(scene.name,LoadSceneMode.Single);
    }

    private void Exit()
    {
        GameManagerLogic.state = GameManagerLogic.State.start;
        Time.timeScale = 1f;
        SceneManager.LoadScene("StartMenu",LoadSceneMode.Single);
    }

}
