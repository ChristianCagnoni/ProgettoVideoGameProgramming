using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DigitalRuby.PyroParticles;

public class FinalBoss : MonoBehaviour
{

    public GameObject target;
    public float cooldown;
    public float firstWait;
    public GameObject BallFire;
    public float speed;

    private bool canAttack;
    private GameObject launcher;
    private bool ready;
    private GameObject istance;
    private Vector3 normalizeDirection;

    // Start is called before the first frame update
    void Start()
    {
        launcher = transform.GetChild(7).gameObject;
        canAttack = false;
        StartCoroutine("waitForAttack");
        ready = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < target.transform.position.y +32)
        {
            transform.position = transform.position + new Vector3(0,16f,0)*Time.deltaTime;
        }
        transform.LookAt(new Vector3(target.transform.position.x, 0, target.transform.position.z));
        if (canAttack)
        {
            StartCoroutine("AttackCooldown");
        }
        if (ready)
        {
            float step = speed * Time.deltaTime;
            istance.transform.position += normalizeDirection *step;//Vector3.MoveTowards(istance.transform.position, target.transform.position, step);
        }
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
