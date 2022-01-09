using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyZombieFC : MonoBehaviour
{

    
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
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        HealthBar = GameObject.Find("GUI").transform.GetChild(1).GetComponent<HealthBar>();
        target=GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManagerLogic.state != GameManagerLogic.State.pause && GameManagerLogic.state != GameManagerLogic.State.death)
        {

            if (Vector3.Distance(target.position, transform.position) < viewArea)
            {
                playerSighted = true;
            }
            else
            {
                playerSighted = false;
            }

            //playerFound();
            if (!playerSighted)
            {
                if (!agent.hasPath)
                {
                    agent.SetDestination(GetPoint.Instance.GetRandomPoint(transform, radius));
                    animator.SetBool("Walk", true);
                }

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
            else
            {
                if (playerSighted)
                {
                    if (target)
                    {
                        agent.SetDestination(target.position);
                        animator.SetBool("Walk", true);
                    }
                }

                if (Vector3.Distance(target.position, transform.position) < Player.noiseLevel * 4)
                {
                    if (target)
                    {
                        agent.SetDestination(target.position);
                        animator.SetBool("Walk", true);
                        agent.speed = moveSpeed + 1;
                    }
                }

                if (playerInRange && canAttack)
                {
                    animator.SetBool("Walk", false);
                    animator.SetBool("Attack", true);
                    HealthBar.SetHealth((int)(HealthBar.GetHealth() - damage));
                    target.position = target.position + transform.forward + new Vector3(0, 1, 0);
                    StartCoroutine(AttackCooldown());
                    if (HealthBar.GetHealth() <= 0)
                    {
                        animator.SetBool("Attack", false);
                        animator.SetTrigger("AfterDead");
                    }
                }
            }
        }
    }

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
                HealthBar.SetHealth((int)(HealthBar.GetHealth() - 1));
            }
        }
    }

    IEnumerator AttackCooldown()
    {
        canAttack = false;
        yield return new WaitForSeconds(enemyCooldown);
        canAttack = true;
        animator.SetBool("Attack", false);
    }

#if UNITY_EDITOR

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
        Gizmos.DrawWireSphere(transform.position,viewArea);
    }

#endif

}
