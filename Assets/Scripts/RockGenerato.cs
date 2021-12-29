using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockGenerato : MonoBehaviour
{

    public GameObject prefab;
    public int number;
    public float distance;
    public Terrain terrain;

    private Vector3 position;
    
    private Vector3 terrainP;
    private float minW = -511;
    private float minH = -710;
    private float maxH=210;
    private float maxW=419;

    // Start is called before the first frame update
    void Start()
    {
        position = new Vector3(1,0,2);
        StartCoroutine("spawner");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator spawner()
    {
        Random.seed = 30000;

        for (int i = 0; i < number; i++)
        {
            GameObject istanObject=Instantiate(prefab, position, Quaternion.identity);
            bool correct = false;
            Vector3 posUpdate = new Vector3(maxW,0,maxH);
            Vector3 tmpPos = Vector3.zero;
            int iter = 0;
            while (!correct)
            {
                posUpdate= new Vector3(Random.Range(-20.0f, 20.0f) + Random.Range(-10, 10) * distance, 0, Random.Range(-20.0f, 20.0f) + Random.Range(-10, 10) * distance);
                tmpPos= position + posUpdate;
                if (tmpPos.x> minW && tmpPos.x < maxW && tmpPos.z > minH && tmpPos.z < maxH)
                {
                    correct = true;
                }
                iter++;
                if (iter > 5)
                {
                    posUpdate = new Vector3(Random.Range(-20.0f, -1) + Random.Range(-10, -1) * distance, 0, Random.Range(-20.0f, -1) + Random.Range(-10, -1) * distance);
                    correct = true;
                }
            }
            correct = false;
            position = position + posUpdate;
        }
        yield return null;
    }
}
