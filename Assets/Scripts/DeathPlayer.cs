using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//Script per la gestione della schermata quando il player muore
public class DeathPlayer : MonoBehaviour
{

    public Button restart;
    public Button close;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        restart.onClick.AddListener(Reload);
        close.onClick.AddListener(Exit);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //metodo attaccato al listener di un pulsante per ricaricare il livello
    private void Reload()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Scene scene = SceneManager.GetActiveScene();
        GameManagerLogic.state = GameManagerLogic.State.start;
        Time.timeScale = 1f;
        SceneManager.LoadScene(scene.name,LoadSceneMode.Single);
    }

    //metodo attaccato al listener di un pulsante per tornare alla schermata iniziale
    private void Exit()
    {
        GameManagerLogic.state = GameManagerLogic.State.start;
        Time.timeScale = 1f;
        SceneManager.LoadScene("StartMenu",LoadSceneMode.Single);
    }

}
