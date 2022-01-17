using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

//sciprt che gestisce il lancio di una nuova partita
public class NewGame : MonoBehaviour
{

    public GameObject parent;
    public Toggle toggle;
    public InputField charcterN;
    public Dropdown character;
    public Dropdown difficulty;
    public Image characterI;
    public TextMeshProUGUI characterText;
    public Sprite characterSprite1;
    public Sprite characterSprite2;

    private Button backB;
    private Button confirmB;
    private string nameC;


    // Start is called before the first frame update
    void Start()
    {
        backB = transform.GetChild(4).gameObject.GetComponent<Button>();
        confirmB = transform.GetChild(5).gameObject.GetComponent<Button>();
        backB.onClick.AddListener(backButton);
        confirmB.onClick.AddListener(confirmButton);
        character.onValueChanged.AddListener(delegate { changeCharacter(); });
        difficulty.onValueChanged.AddListener(delegate { changeDifficulty(); });
        nameC = characterText.text;
    }

    //metodo che setta la difficoltà selezionata
    private void changeDifficulty()
    {
        int value=difficulty.value;
        if (value == 0)
        {
            SettingsManager.difficulty = "easy";
        }
        else if(value==1)
        {
            SettingsManager.difficulty = "medium";
        }
        else
        {
            SettingsManager.difficulty = "difficult";
        }
    }

    //metodo che setta il tipo di personaggio
    private void changeCharacter()
    {
        int value=character.value;
        if (value == 0)
        {
            SettingsManager.character = 0;
            characterI.sprite= characterSprite1;
            characterText.text = "Pro: più vita \nContro: meno energia";
        }
        else
        {
            SettingsManager.character = 1;
            characterI.sprite = characterSprite2;
            characterText.text = "Pro: più energia \nContro: meno vita";
        }
    }

    //metodo che gestisce il pulsante back
    private void backButton()
    {
        MenuStartupManager.actualMenu.SetActive(false);
        MenuStartupManager.actualMenu = parent;
        MenuStartupManager.actualMenu.SetActive(true);
    }

    //metodo che lancia il tutorial o il primo livello
    private void confirmButton()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        SettingsManager.characterName = nameC;
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
