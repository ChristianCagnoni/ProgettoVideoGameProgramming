using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Script che gestisce il passaggio dal secondo al livello bonus
public class ChapterBonus : MonoBehaviour
{


    public static bool inBonusChapter;

    // Start is called before the first frame update
    void Start()
    {
        inBonusChapter = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //quando il player entra nel trigger attiva un booleano
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player")
        {
            Player.secret = true;
        }
    }

    //quando il player esce dal trigger disattiva un booleano
    private void OnTriggerExit(Collider other)
    {
        if (other.name == "Player")
        {
            Player.secret = false;
        }
    }

    //se il player sta nel trigger e soddisfa una condizione lancia una corutine
    private void OnTriggerStay(Collider other)
    {
        if (other.name == "Player")
        {
            if (Player.isPower && Player.isPowerUsed)
            {
                StartCoroutine("Bonus");
            }
        }
    }

    //metodo che gestisce il passaggio al livello bonus
    IEnumerator Bonus()
    {
        inBonusChapter=true;
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("BonusChapter", LoadSceneMode.Single);
        yield return null;
    }

}
