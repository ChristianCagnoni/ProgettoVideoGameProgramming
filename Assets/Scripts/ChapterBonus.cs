using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player")
        {
            Player.secret = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.name == "Player")
        {
            Player.secret = false;
        }
    }

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

    IEnumerator Bonus()
    {
        inBonusChapter=true;
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("BonusChapter", LoadSceneMode.Single);
        yield return null;
    }

}
