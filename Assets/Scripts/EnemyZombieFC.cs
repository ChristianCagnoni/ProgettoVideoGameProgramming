using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyZombieFC : MonoBehaviour
{

    
    public NavMeshAgent agent;
    static bool enemyShooting;
    public bool playerSighted;
    public float moveSpeed = 4;
    public float maxDist = 10;
    public float minDist = 5;

    public float enemyCooldown = 1;
    public float damage = 1;

    private bool playerInRange = false;
    private bool canAttack = true;
    private Transform target;

    private void Awake()
    {
        playerSighted = false;
        enemyShooting = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        target=GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        playerFound();

        if (playerSighted)
            {
                if (target)
                {
                    agent.SetDestination(target.position);
                }
            }
        if (Vector3.Distance(target.position, transform.position) < Player.noiseLevel * 4)
            {
                if (target)
                {
                    agent.SetDestination(target.position);
                    agent.speed = moveSpeed + 1;
                }
            }
        if (playerInRange && canAttack)
        {
            //GameObject.Find("Player").GetComponent<ControllerForPlayer>().currentHealth -= damage;
            Debug.Log("Attacco");
            target.position = target.position + new Vector3(1.0f, 0, 0);
            StartCoroutine(AttackCooldown());
        }
    }

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
        if (other.gameObject.CompareTag("Player"))
        {
            playerInRange = false;
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
                Debug.Log("Shot");
            }
        }
    }

    IEnumerator AttackCooldown()
    {
        canAttack = false;
        yield return new WaitForSeconds(enemyCooldown);
        canAttack = true;
    }

}
