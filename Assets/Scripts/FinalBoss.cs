using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DigitalRuby.PyroParticles;

public class FinalBoss : MonoBehaviour
{

    public enum FinalBossStatus { start, half, dead };
    public enum FinalBossDamage {god,normal }

    public GameObject target;
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

    private bool canAttack;
    private GameObject launcher;
    private bool ready;
    private GameObject istance;
    private Vector3 normalizeDirection;
    private FinalBossStatus status;
    

    // Start is called before the first frame update
    void Start()
    {
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
            if (bossBar.GetHealth() <= maxH / 2)
            {
                damage = FinalBossDamage.god;
                status = FinalBossStatus.half;
            }
            if (bossBar.GetHealth() <= 0)
            {
                status=FinalBossStatus.dead;
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
            }else if(canAttack && status == FinalBossStatus.half)
            {

            }
            if (ready)
            {
                float step = speed * Time.deltaTime;
                istance.transform.position += normalizeDirection * step;
            }
            if(Vector3.Distance(transform.position,peak)<=1 && damage == FinalBossDamage.god)
            {
                poisonSphere.SetActive(false);
                poisonSphere.transform.position = new Vector3(0, 580.1f, 0);
                godAttack[0].SetActive(true);
                godAttack[1].SetActive(true);
                godAttack[2].SetActive(true);
                godAttack[3].SetActive(true);
                StartCoroutine("waitAttackGod");
            }
        }
    }

    IEnumerator waitAttackGod()
    {
        yield return new WaitForSeconds(godDuration);
        godAttack[0].SetActive(false);
        godAttack[1].SetActive(false);
        godAttack[2].SetActive(false);
        godAttack[3].SetActive(false);
        damage = FinalBossDamage.normal;
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
        canAttack = false;
        yield return new WaitForSeconds(cooldown);
        canAttack = true;
    }
}