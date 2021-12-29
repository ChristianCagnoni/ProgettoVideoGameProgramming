using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{

    public GameObject[] prefabs;
    public GameObject[] targets;
    public Material material;  

    private int maxIndex;
    private int maxEnemy;
    private int maxPosition;
    private int enemyCounter;

    // Start is called before the first frame update
    void Start()
    {
        maxIndex = prefabs.Length;
        maxEnemy = 30;
        enemyCounter = 0;
        maxPosition = targets.Length;
        StartCoroutine("enemySpawner");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator enemySpawner()
    {
        yield return new WaitForSeconds(5);
        RenderSettings.skybox =material;
        while (true)
        {
            if (enemyCounter < maxEnemy)
            {
                enemyCounter++;
                int tmp = Random.Range(0, maxIndex);
                int tmpPosition = Random.Range(0, maxPosition);
                Instantiate(prefabs[tmp], targets[tmpPosition].transform.position, Quaternion.identity);
            }
            yield return new WaitForSeconds(5);
        }
    }
}
