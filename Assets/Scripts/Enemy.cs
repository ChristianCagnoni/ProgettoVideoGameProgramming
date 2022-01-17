using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//Script per la gestione del nemico nel tutorial
public class Enemy : MonoBehaviour
{
    //parametri per la gestione del nemico
    public Transform target;
    public NavMeshAgent agent;
    static bool enemyShooting;
    public bool playerSighted;
    public float moveSpeed=4;
    public float maxDist=10;
    public float minDist=5;

    public float enemyCooldown = 1;
    public float damage = 1;

    private bool playerInRange = false;
    private bool canAttack = true;

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
        //in base alla fase del tutorial fai cose diverse
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
            //if (transform.position.x == target.position.x && transform.position.z == target.position.z)
            //{
            //}
        }
        if(TutorialManager.tutorialPhase == 3)
        {
            if (Vector3.Distance(target.position, transform.position) < Player.noiseLevel *4)
            {
                if (target)
                {
                    agent.SetDestination(target.position);
                }
            }
        }
        /*if (TutorialManager.tutorialPhase == 8)
        {
            if (target)
            {
                agent.SetDestination(target.position);
                agent.speed = moveSpeed + 1;
            }
            if (playerInRange && canAttack)
            {
                //GameObject.Find("Player").GetComponent<ControllerForPlayer>().currentHealth -= damage;
                Debug.Log("Attacco");
                target.position = target.position + new Vector3(1.0f, 0, 0);
                StartCoroutine(AttackCooldown());
            }
        }*/
    }

    //cambia parametri quando player entra nel trigger
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform == target)
        {
            Debug.Log("audioSource");
        }
        if (other.gameObject.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }


    //cambia parametri quando player sta nel trigger
    private void OnTriggerStay(Collider other)
    {
        if (other.transform == target)
        {
            playerSighted = true;
        }
    }

    //cambia parametri quando player esce dal trigger
    private void OnTriggerExit(Collider other)
    {
        if (other.transform == target)
        {
            playerSighted = false;
            enemyShooting = false;
        }
        if (other.gameObject.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }

    //metodo per quando il player viene trovato
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

    //metodo per la gestione del cooldown dell'attacco
    IEnumerator AttackCooldown()
    {
        canAttack = false;
        yield return new WaitForSeconds(enemyCooldown);
        canAttack = true;
    }

}
