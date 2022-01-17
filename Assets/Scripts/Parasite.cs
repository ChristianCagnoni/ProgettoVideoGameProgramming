using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//script per la gestione del nemico parasite
public class Parasite : MonoBehaviour
{
    //parametri per la gestione del personaggio
    public bool playerSighted;
    public float moveSpeed = 4;
    public float maxDist = 10;
    public float minDist = 5;
    public float enemyCooldown = 1;
    public float damage = 1;
    public float radius;
    public Transform[] transforms;
    public int numberT;

    private NavMeshAgent agent;
    private HealthBar HealthBar;
    private bool playerInRange = false;
    private bool canAttack = true;
    private Transform target;
    private Animator animator;
    private bool isAttacking;
    private int old;
    private int last;

    private void Awake()
    {
        playerSighted = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        //influenza parametri in base alla difficoltà
        if (SettingsManager.difficulty == "easy")
        {
        }
        else if (SettingsManager.difficulty == "medium")
        {
            moveSpeed++;
            maxDist += 5;
            minDist -= 2;
            enemyCooldown -= 0.25f;
            damage += 1;
            radius += 5;
        }
        else
        {
            moveSpeed += 2;
            maxDist += 10;
            minDist -= 4;
            enemyCooldown -= 0.5f;
            damage += 2;
            radius += 10;
        }
        old = 0;
        last = 0;
        numberT = transforms.Length;
        isAttacking = false;
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        HealthBar = GameObject.Find("GUI").transform.GetChild(1).GetComponent<HealthBar>();
        target = null;
    }

    // Update is called once per frame
    void Update()
    {
        //gestione del target
        if (target == null)
        {
            if (GameObject.Find("Player") != null)
            {
                target= GameObject.Find("Player").transform; ;
            }
        }
        //esegui il seguente blocco se gioco non è in pausa o se target esiste
        if (GameManagerLogic.state != GameManagerLogic.State.pause && GameManagerLogic.state != GameManagerLogic.State.death && target!=null)
        {

            if (Vector3.Distance(target.position, transform.position) < radius)//visione target
            {
                playerSighted = true;
            }
            else
            {
                playerSighted = false;
            }

            if (!playerSighted)//se target non visto
            {
                //movimento casuale
                if (!agent.hasPath)
                {
                    int dec = Random.Range(0, numberT);
                    if (old + dec == 4 || old + dec == 5)
                    {
                        dec = 0;
                        old = 0;
                    }
                    agent.SetDestination(transforms[dec].position);
                    animator.SetBool("Walk", true);
                    if (last != dec)
                    {
                        old += dec;
                        last = dec;
                    }
                    
                }
                //raycast per evitare collisioni
                RaycastHit hit;
                Vector3 raycastDir = agent.destination - transform.position;
                Ray landingRay = new Ray(transform.position, raycastDir);
                if (agent.hasPath)
                {
                    Debug.DrawRay(landingRay.origin, landingRay.direction, Color.red);
                    if (Physics.Raycast(landingRay, out hit, 1))
                    {
                        if (hit.collider.tag == "Cave")
                        {
                            agent.ResetPath();
                            agent.SetDestination(transforms[Random.Range(0, numberT)].position);
                            animator.SetBool("Walk", true);
                        }
                    }
                }
            }
            else if (canAttack && playerInRange)//attacco
            {
                Debug.Log("1");
                isAttacking = true;
                agent.ResetPath();
                animator.SetBool("Walk", false);
                StartCoroutine(AttackCooldown());
            }
            else if(!isAttacking)//movimento
            {
                if (target)
                {
                    agent.SetDestination(target.position);
                    animator.SetBool("Walk", true);
                }
            }
        }
    }

    //cambia parametri se player entra nel trigger
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform == target)
        {
            Debug.Log("audioSource");
            agent.ResetPath();
        }
        if (other.gameObject.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }



    private void OnTriggerStay(Collider other)
    {
    }

    //cambia parametri se player esce dal trigger
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerInRange = false;
            animator.SetBool("Attack", false);
            canAttack = true;
            isAttacking = false;
        }
    }

    //metodo per l agestione del cooldown
    IEnumerator AttackCooldown()
    {
        canAttack = false;
        animator.SetBool("Attack", true);
        GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(animator.runtimeAnimatorController.animationClips[0].length);
        if (playerInRange)
        {
            HealthBar.SetHealth((int)(HealthBar.GetHealth() - damage));
            if (HealthBar.GetHealth() <= 0)
            {
            }
        }
        animator.SetBool("Attack", false);
        canAttack = true;
        isAttacking = false;
        Debug.Log("2");
    }

    //visione in editor del range
#if UNITY_EDITOR

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }

#endif

}
