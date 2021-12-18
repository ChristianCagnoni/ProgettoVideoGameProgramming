using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/*


instead of actually trying to detect a "sound", you can just set a variable that stores like a noiseLevel, and if the distance from the player to the enemy is smaller than the noiseLevel, the enemy knows you're there.

for example, when you're not doing anything, the noise level is 0. when you're walking it is 1. if you shoot, it is 10. and if the distance from the enemy to you is smaller than the noiselevel, they find you.
 
 
 */

public class Enemy : MonoBehaviour
{

    public Transform target;
    public NavMeshAgent agent;
    static bool enemyShooting;
    public bool playerSighted;
    public float moveSpeed=4;
    public float maxDist=10;
    public float minDist=5;

    private void Awake()
    {
        playerSighted = false;
        enemyShooting = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (TutorialManager.tutorialPhase ==2)
        {
            if (playerSighted)
            {
                playerFound();
                if (target)
                {
                    agent.SetDestination(target.position);
                }
            }
            if (transform.position.x == target.position.x && transform.position.z == target.position.z)
            {
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform == target)
        {
            Debug.Log("audioSource");
        }
    }



    private void OnTriggerStay(Collider other)
    {
        if (other.transform == target)
        {
            playerSighted = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform == target)
        {
            playerSighted = false;
            enemyShooting = false;
        }
    }

    void playerFound()
    {
        Vector3 lookAt = target.position;

        lookAt.y = transform.position.y;
        transform.LookAt(lookAt);

        if (Vector3.Distance(transform.position, target.position) >= minDist)
        {
            //transform.position += transform.forward * moveSpeed * Time.deltaTime;
            if (Vector3.Distance(transform.position, target.position) <= maxDist)
            {
                enemyShooting = true;
            }
        }
    }

}
