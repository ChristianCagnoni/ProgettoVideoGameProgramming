using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DigitalRuby.PyroParticles;

public class FinalBoss : MonoBehaviour
{

    public enum FinalBossStatus { start, half, dead,quarter };
    public enum FinalBossDamage {god,normal }

    private GameObject target;
    public float cooldown;
    public float firstWait;
    public GameObject BallFire;
    public float speed;
    public BossHealthBar bossBar;
    public int maxH;
    public Vector3 peak;
    public static FinalBossDamage damage;
    public GameObject[] godAttack;
    public float godDuration;
    public GameObject poisonSphere;
    public Shader dissolve;
    public GameObject dissolveTarget;
    public GameObject gui;
    public GameObject specialObject;

    private bool canAttack;
    private GameObject launcher;
    private bool ready;
    private GameObject istance;
    private Vector3 normalizeDirection;
    private FinalBossStatus status;
    

    // Start is called before the first frame update
    void Start()
    {
        if (SettingsManager.difficulty == "easy")
        {
        }
        else if (SettingsManager.difficulty == "medium")
        {
            maxH += 100;
            speed += 50;
            firstWait -= 3;
            cooldown -= 2;
        }
        else
        {
            maxH += 200;
            speed += 100;
            firstWait -= 6;
            cooldown -= 4;
        }
        target = GameObject.Find("Player");
        damage = FinalBossDamage.normal;
        bossBar.SetMaxHealth(maxH);
        status = FinalBossStatus.start;
        launcher = transform.GetChild(7).gameObject;
        canAttack = false;
        StartCoroutine("waitForAttack");
        ready = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManagerLogic.state != GameManagerLogic.State.pause && GameManagerLogic.state != GameManagerLogic.State.death)
        {
            if (bossBar.GetHealth() <= maxH / 2 && Vector3.Distance(transform.position, peak) <= 1)
            {
                damage = FinalBossDamage.god;
                status = FinalBossStatus.half;
            }
            if (bossBar.GetHealth() <= maxH / 4 && Vector3.Distance(transform.position, peak) <= 1 && SettingsManager.difficulty=="difficult")
            {
                damage = FinalBossDamage.god;
                status = FinalBossStatus.half;
            }
            if (bossBar.GetHealth() <= 0)
            {
                status=FinalBossStatus.dead;
                dissolveTarget.GetComponent<SkinnedMeshRenderer>().material.shader = dissolve;
                StartCoroutine("waitDissolve");
            }
            if (transform.position.y < target.transform.position.y + 32)
            {
                transform.position = transform.position + new Vector3(0, 16f, 0) * Time.deltaTime;
            }
            transform.LookAt(new Vector3(target.transform.position.x, 0, target.transform.position.z));
            transform.rotation = new Quaternion(0, transform.rotation.y, 0,transform.rotation.w);
            if (canAttack && status==FinalBossStatus.start)
            {
                StartCoroutine("AttackCooldown");
            }
            if (ready)
            {
                float step = speed * Time.deltaTime;
                istance.transform.position += normalizeDirection * step;
                Debug.Log(speed);
                Debug.Log(step);
            }
            if(Vector3.Distance(transform.position,peak)<=1 && damage == FinalBossDamage.god)
            {
                //poisonSphere.SetActive(false);
                poisonSphere.transform.position = new Vector3(0, 580.1f, 0);
                if (SettingsManager.difficulty == "easy")
                {
                    godAttack[0].SetActive(true);
                }
                else if (SettingsManager.difficulty == "medium")
                {
                    godAttack[0].SetActive(true);
                    godAttack[1].SetActive(true);
                }
                else
                {
                    godAttack[0].SetActive(true);
                    godAttack[1].SetActive(true);
                    godAttack[2].SetActive(true);
                    godAttack[3].SetActive(true);
                }
                StartCoroutine("waitAttackGod");
            }
        }
    }

    IEnumerator waitDissolve()
    {
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
        gui.transform.GetChild(4).gameObject.SetActive(false);
        Destroy(poisonSphere);
        specialObject.SetActive(true);
    }

    IEnumerator waitAttackGod()
    {
        yield return new WaitForSeconds(godDuration);
        if (SettingsManager.difficulty == "easy")
        {
            godAttack[0].SetActive(false);
        }
        else if (SettingsManager.difficulty == "medium")
        {
            godAttack[0].SetActive(false);
            godAttack[1].SetActive(false);
        }
        else
        {
            godAttack[0].SetActive(false);
            godAttack[1].SetActive(false);
            godAttack[2].SetActive(false);
            godAttack[3].SetActive(false);
        }
        ready = false;
        damage = FinalBossDamage.normal;
        status = FinalBossStatus.start;
    }

    IEnumerator waitForAttack()
    {
        yield return new WaitForSeconds(firstWait);
        canAttack = true;
    }

    private void BeginEffect()
    {
        istance = Instantiate(BallFire,launcher.transform.position,Quaternion.identity);
        normalizeDirection = (target.transform.position - istance.transform.position).normalized;
        ready = true;
    }

    IEnumerator AttackCooldown()
    {
        BeginEffect();
        GetComponent<AudioSource>().Play();
        canAttack = false;
        yield return new WaitForSeconds(cooldown);
        canAttack = true;
    }
}
