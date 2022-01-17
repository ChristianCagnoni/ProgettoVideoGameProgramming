using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DigitalRuby.PyroParticles;

//script per la gestione del nemico skeleton
public class Skeleton : MonoBehaviour
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
    public GameObject FlameThrowner;

    private UnityEngine.AI.NavMeshAgent agent;
    private HealthBar HealthBar;
    private bool playerInRange = false;
    private bool canAttack = true;
    private Transform target;
    private Animator animator;
    private bool isAttacking;
    private int old;
    private int last;
    private GameObject currentPrefabObject;
    private FireBaseScript currentPrefabScript;

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
        numberT = transforms[0].childCount;
        isAttacking = false;
        animator = GetComponent<Animator>();
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        HealthBar = GameObject.Find("GUI").transform.GetChild(1).GetComponent<HealthBar>();
        target =null;
    }

    // Update is called once per frame
    void Update()
    {
        //gestione del target
        if (target == null)
        {
            if (GameObject.Find("Player") != null)
            {
                target = GameObject.Find("Player").transform; ;
            }
        }

        //esegui il seguente blocco se gioco non è in pausa e il target esiste
        if (GameManagerLogic.state != GameManagerLogic.State.pause && GameManagerLogic.state != GameManagerLogic.State.death && target!=null)
        {

            if (Vector3.Distance(target.position, transform.position) < radius)//target visto
            {
                playerSighted = true;
            }
            else
            {
                playerSighted = false;
            }

            if (!playerSighted)//se target non visto movimento verso un punto casuale
            {
                if (!agent.hasPath)
                {
                    int dec = Random.Range(0, numberT);
                    agent.SetDestination(transforms[0].GetChild(dec).transform.position);
                    animator.SetBool("Walk", true);
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
            else if (!isAttacking)//movimento
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
            if (currentPrefabScript != null && currentPrefabScript.Duration > 10000)
            {
                currentPrefabScript.Stop();
            }
            currentPrefabObject = null;
            currentPrefabScript = null;
        }
    }

    //metodo per la gestione del lanciafiamme
    private void BeginEffect()
    {
        Vector3 pos;
        float yRot = transform.rotation.eulerAngles.y;
        Vector3 forwardY = Quaternion.Euler(0.0f, yRot, 0.0f) * Vector3.forward;
        Vector3 forward = transform.forward;
        Vector3 right = transform.right;
        Vector3 up = transform.up;
        Quaternion rotation = Quaternion.identity;
        currentPrefabObject = GameObject.Instantiate(FlameThrowner);
        currentPrefabObject.transform.GetChild(0).gameObject.layer=6;
        currentPrefabScript = currentPrefabObject.GetComponent<FireConstantBaseScript>();

        if (currentPrefabScript == null)
        {
            // temporary effect, like a fireball
            currentPrefabScript = currentPrefabObject.GetComponent<FireBaseScript>();
            if (currentPrefabScript.IsProjectile)
            {
                // set the start point near the player
                rotation = transform.rotation;
                pos = transform.position + forward + right + up;
            }
            else
            {
                // set the start point in front of the player a ways
                pos = transform.position + (forwardY * 10.0f);
            }
        }
        else
        {
            // set the start point in front of the player a ways, rotated the same way as the player
            pos = transform.position + (forwardY * 5.0f);
            rotation = transform.rotation;
            pos.y = 0.0f;
        }

        FireProjectileScript projectileScript = currentPrefabObject.GetComponentInChildren<FireProjectileScript>();
        if (projectileScript != null)
        {
            // make sure we don't collide with other fire layers
            projectileScript.ProjectileCollisionLayers &= (~UnityEngine.LayerMask.NameToLayer("FireLayer"));
        }

        currentPrefabObject.transform.position = pos;
        currentPrefabObject.transform.rotation = rotation;
    }

    //metodo per la gestione del cooldown
    IEnumerator AttackCooldown()
    {
        BeginEffect();
        canAttack = false;
        animator.SetBool("Attack", true);
        GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(animator.runtimeAnimatorController.animationClips[0].length);
        animator.SetBool("Attack", false);
        canAttack = true;
        isAttacking = false;
        Debug.Log("2");
    }

    //metodo per la visione in editor dell'area visible
#if UNITY_EDITOR

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }

#endif
}
