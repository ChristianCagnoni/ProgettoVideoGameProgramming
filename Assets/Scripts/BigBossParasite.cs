using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BigBossParasite : MonoBehaviour
{

    public enum BossStatus { live, death };

    public bool playerSighted;
    public float moveSpeed = 4;
    public float maxDist = 10;
    public float minDist = 5;
    public float enemyCooldown = 1;
    public float damage = 1;
    public float radius;
    public BossHealthBar bossBar;
    public int maxH;
    public static BossStatus state;
    public GameObject gui;
    public GameObject portal;
    public Shader dissolve;
    public GameObject dissolveTarget;

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
        state=BossStatus.live;
        bossBar.SetMaxHealth(maxH);
        old = 0;
        last = 0;
        isAttacking = false;
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        HealthBar = GameObject.Find("GUI").transform.GetChild(1).GetComponent<HealthBar>();
        target = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManagerLogic.state != GameManagerLogic.State.pause && GameManagerLogic.state != GameManagerLogic.State.death && state!=BossStatus.death)
        {
            if (canAttack && playerInRange)
            {
                Debug.Log("1");
                isAttacking = true;
                agent.ResetPath();
                animator.SetBool("Walk", false);
                StartCoroutine(AttackCooldown());
            }
            else if (!isAttacking && playerSighted)
            {
                if (target)
                {
                    animator.SetBool("Walk", true);
                    agent.SetDestination(target.position);
                    agent.speed = moveSpeed;
                }
            }
            if (Vector3.Distance(target.position, transform.position) < radius)
            {
                playerSighted = true;
            }
            else
            {
                playerSighted = false;
            }
            if (bossBar.GetHealth() <= 0)
            {
                state = BossStatus.death;
                dissolveTarget.GetComponent<SkinnedMeshRenderer>().material.shader = dissolve;
                StartCoroutine("waitDissolve");
            }
        }
    }

    IEnumerator waitDissolve()
    {
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
        gui.transform.GetChild(4).gameObject.SetActive(false);
        portal.SetActive(true);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Player")
        {
            Debug.Log("ok");
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
        }
    }

    IEnumerator AttackCooldown()
    {
        canAttack = false;
        animator.SetBool("Attack",true);
        GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(animator.runtimeAnimatorController.animationClips[2].length);
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
    }

#if UNITY_EDITOR

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }

#endif

}
