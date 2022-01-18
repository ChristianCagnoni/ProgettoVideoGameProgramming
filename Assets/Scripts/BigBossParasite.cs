using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//Script per la gestione del boss del secondo livello
public class BigBossParasite : MonoBehaviour
{

    public enum BossStatus { live, death };     //stati del boss (live o death)
    //parametri per la gestione del personaggio
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
    //parametri relativi alle azioni e allo stato del personaggio
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
        Debug.Log(SettingsManager.difficulty);
        //in base alla difficoltà influenza i parametri
        if (SettingsManager.difficulty == "easy")
        {
        }
        else if (SettingsManager.difficulty == "medium")
        {
            moveSpeed+=2;
            maxDist += 10;
            minDist -= 2;
            enemyCooldown -= 0.25f;
            damage += 5;
            radius += 10;
            maxH += 50;
        }
        else if (SettingsManager.difficulty == "difficult")
        {
            moveSpeed += 4;
            maxDist += 20;
            minDist -= 4;
            enemyCooldown -= 0.5f;
            damage += 10;
            radius += 20;
            maxH += 100;
        }
        else
        {
            moveSpeed += 6;
            maxDist += 40;
            minDist -= 5;
            enemyCooldown -= 0.75f;
            damage += 20;
            radius += 40;
            maxH += 200;
        }
        state =BossStatus.live;
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
        //esegui il seguente blocco se il gioco non è in pausa
        if (GameManagerLogic.state != GameManagerLogic.State.pause && GameManagerLogic.state != GameManagerLogic.State.death && state!=BossStatus.death)
        {
            if (canAttack && playerInRange)//attacco
            {
                Debug.Log("1");
                isAttacking = true;
                agent.ResetPath();
                animator.SetBool("Walk", false);
                StartCoroutine(AttackCooldown());
            }
            else if (!isAttacking && playerSighted)//movimento casuale
            {
                if (target)
                {
                    animator.SetBool("Walk", true);
                    agent.SetDestination(target.position);
                    agent.speed = moveSpeed;
                }
            }
            if (Vector3.Distance(target.position, transform.position) < radius)//visione del target
            {
                playerSighted = true;
            }
            else
            {
                playerSighted = false;
            }
            if (bossBar.GetHealth() <= 0)//vita pari a 0
            {
                state = BossStatus.death;
                dissolveTarget.GetComponent<SkinnedMeshRenderer>().material.shader = dissolve;
                dissolveTarget.AddComponent<DissolveObject>();
                StartCoroutine("waitDissolve");
            }
        }
    }

    //metodo che produce l'effetto dissolvenza
    IEnumerator waitDissolve()
    {
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
        gui.transform.GetChild(4).gameObject.SetActive(false);
        portal.SetActive(true);
    }

    //cambia parametri di gestione se il player entra nel trigger
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

    //cambia parametri di gestione se il player esce dal trigger
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }

    //metodo per la gestione del cooldown dell'attacco
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

    //visualizza in editor l'area d'interesse
#if UNITY_EDITOR

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }

#endif

}
