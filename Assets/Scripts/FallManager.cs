using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


//script che gestisce le cadute nel vuoto del personaggio
public class FallManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //se cadi nel vuoto azzera la vita, se sei nel tutorial ricomincia il tutorial
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player")
        {
            if (SceneManager.GetActiveScene().name == "Tutorial")
            {
                SceneManager.LoadScene("Tutorial", LoadSceneMode.Single);
            }
            else
            {
                HealthBar healthBar = GameObject.Find("GUI").transform.GetChild(1).GetComponent<HealthBar>();
                healthBar.SetHealth(0);
            }
        }
    }
}
