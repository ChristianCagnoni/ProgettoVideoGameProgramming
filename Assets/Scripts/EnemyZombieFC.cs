using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//script per la gestione del nemico generico
public class EnemyZombieFC : MonoBehaviour
{

    //parametri che ne gestiscono il comportamento
    static bool enemyShooting;
    public bool playerSighted;
    public float moveSpeed = 4;
    public float maxDist = 10;
    public float minDist = 5;
    public float enemyCooldown = 1;
    public float damage = 1;
    public float radius;
    public float viewArea;

    private NavMeshAgent agent;
    private HealthBar HealthBar;
    private bool playerInRange = false;
    private bool canAttack = true;
    private bool isAttacking;
    private Transform target;
    private Animator animator;

    private void Awake()
    {
        playerSighted = false;
        enemyShooting = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        //in base alla difficoltà influenza i parametri
        if (SettingsManager.difficulty == "easy")
        {
        }else if (SettingsManager.difficulty == "medium")
        {
            moveSpeed++;
            maxDist += 5;
            minDist -= 2;
            enemyCooldown -= 0.25f;
            damage += 1;
            radius+=5;
            viewArea+=5;
        }
        else
        {
            moveSpeed+=2;
            maxDist += 10;
            minDist -= 4;
            enemyCooldown -= 0.5f;
            damage += 2;
            radius += 10;
            viewArea += 10;
        }
        isAttacking = false;
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        HealthBar = GameObject.Find("GUI").transform.GetChild(1).GetComponent<HealthBar>();
        target=GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        //esgui il seguente blocco se il gioco non è in pausa
        if (GameManagerLogic.state != GameManagerLogic.State.pause && GameManagerLogic.state != GameManagerLogic.State.death)
        {

            if (Vector3.Distance(target.position, transform.position) < viewArea)//visione del target
            {
                playerSighted = true;
            }
            else
            {
                playerSighted = false;
            }

            //playerFound();
            if (!playerSighted)//se target non trovato movimento casuale
            {
                animator.SetBool("Attack", false);
                if (!agent.hasPath)
                {
                    agent.SetDestination(GetPoint.Instance.GetRandomPoint(transform, radius));
                    animator.SetBool("Walk", true);
                }
                //raycasting per evitare le montagne
                RaycastHit hit;
                Vector3 raycastDir = agent.destination - transform.position;
                Ray landingRay = new Ray(transform.position, raycastDir);
                if (agent.hasPath)
                {
                    Debug.DrawRay(landingRay.origin, landingRay.direction, Color.red);
                    if (Physics.Raycast(landingRay, out hit, 1))
                    {
                        if (hit.collider.tag == "Rock")
                        {
                            agent.ResetPath();
                            agent.SetDestination(GetPoint.Instance.GetRandomPoint(transform, radius));
                            animator.SetBool("Walk", true);
                        }
                    }
                }
            }
            else//se target trovato
            {
                if (playerSighted && !isAttacking)//movimento
                {
                    if (target)
                    {
                        agent.SetDestination(target.position);
                        animator.SetBool("Walk", true);
                    }
                }

                if (Vector3.Distance(target.position, transform.position) < Player.noiseLevel * 4 && !isAttacking)//ascolto
                {
                    if (target)
                    {
                        agent.SetDestination(target.position);
                        animator.SetBool("Walk", true);
                        agent.speed = moveSpeed + 1;
                    }
                }

                if (playerInRange && canAttack)//attacco
                {
                    isAttacking = true;
                    animator.SetBool("Walk", false);
                    animator.SetBool("Attack", true);
                    StartCoroutine(AttackCooldown());
                }
            }
        }
    }

    //influenza i parametri se player entra nel trigger
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

    //influenza i parametri se player esce dal trigger
    private void OnTriggerExit(Collider other)
    {
        if (other.transform == target)
        {
            enemyShooting = false;
        }
        if (other.gameObject.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }

    /*void playerFound()
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
                HealthBar.SetHealth((int)(HealthBar.GetHealth() - 1));
            }
        }
    }*/

    //metodo per la gestione del cooldown dell'attacco
    IEnumerator AttackCooldown()
    {
        canAttack = false;
        GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(animator.runtimeAnimatorController.animationClips[2].length);
        if (playerInRange)
        {
            HealthBar.SetHealth((int)(HealthBar.GetHealth() - damage));
            target.position = target.position + transform.forward;
            if (HealthBar.GetHealth() <= 0)
            {
                animator.SetBool("Attack", false);
                animator.SetTrigger("AfterDead");
            }
        }
        isAttacking = false;
        canAttack = true;
        animator.SetBool("Attack", false);
    }


    /*private IEnumerator WaitForAnimation(Animation animation)
    {
        do
        {
            yield return null;
        } while (animation.isPlaying);
    }*/

    //mostra in editor l'area di visione e di ricerca
#if UNITY_EDITOR

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
        Gizmos.DrawWireSphere(transform.position,viewArea);
    }

#endif

}
