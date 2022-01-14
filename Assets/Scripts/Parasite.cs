using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Parasite : MonoBehaviour
{

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
        if (target == null)
        {
            if (GameObject.Find("Player") != null)
            {
                target= GameObject.Find("Player").transform; ;
            }
        }
        if (GameManagerLogic.state != GameManagerLogic.State.pause && GameManagerLogic.state != GameManagerLogic.State.death && target!=null)
        {

            if (Vector3.Distance(target.position, transform.position) < radius)
            {
                playerSighted = true;
            }
            else
            {
                playerSighted = false;
            }

            if (!playerSighted)
            {
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
            else if (canAttack && playerInRange)
            {
                Debug.Log("1");
                isAttacking = true;
                agent.ResetPath();
                animator.SetBool("Walk", false);
                StartCoroutine(AttackCooldown());
            }
            else if(!isAttacking)
            {
                if (target)
                {
                    agent.SetDestination(target.position);
                    animator.SetBool("Walk", true);
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
        if (other.gameObject.CompareTag("Player"))
        {
            playerInRange = false;
            animator.SetBool("Attack", false);
            canAttack = true;
            isAttacking = false;
        }
    }

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

#if UNITY_EDITOR

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }

#endif

}
