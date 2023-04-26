using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMove : MonoBehaviour
{
    [SerializeField]
    private Transform EnemyTr;
    [SerializeField]
    private Transform PlayerTr;
    [SerializeField]
    private NavMeshAgent nvagent;
    private float attackdist = 2.0f;
    private float tracedist = 10f;
    [SerializeField]
    private List<Transform> Patrolpoint;
    private int nextwaypoint = 0;
    [SerializeField]
    private float attacktime = 0f;
    private float attackcooltime = 3.0f;
    private Animator enemyani;
    [SerializeField]
    private BoxCollider attackcolider;

    void Start()
    {
        enemyani = gameObject.GetComponent<Animator>();
        EnemyTr = this.gameObject.transform;
        PlayerTr = GameObject.FindWithTag("Player").GetComponent<Transform>();
        nvagent = gameObject.GetComponent<NavMeshAgent>();
        var point = GameObject.Find("SlimePatrolPoint");
        if(point != null)
        {
            point.GetComponentsInChildren<Transform>(Patrolpoint);
            Patrolpoint.RemoveAt(0);
            nextwaypoint = Random.Range(0, Patrolpoint.Count);
        }
        attackcolider = gameObject.transform.GetChild(3).GetComponent<BoxCollider>();
    }

    
    void Update()
    {
        if (EnemyTr.GetComponent<EnemyHealth>().isdie)
            return;
        checkdistance();
    }

    private void checkdistance()
    {
        float playerdist = (PlayerTr.position - EnemyTr.position).magnitude;
        float patrolpdist = (Patrolpoint[nextwaypoint].position - EnemyTr.position).magnitude;

        if(playerdist<attackdist)
        {
            nvagent.isStopped = true;
            attacking();
            enemyani.SetBool("IsMove", false);
        }
        else if(playerdist<tracedist)
        {
            //따라가기
            nvagent.isStopped = false;
            nvagent.destination = PlayerTr.position;
            enemyani.SetBool("IsMove", true);
        }
        else
        {
            //패트롤
            nvagent.isStopped = false;
            nvagent.destination = Patrolpoint[nextwaypoint].position;
            if(patrolpdist < 0.5f)
            {
                nextwaypoint = Random.Range(0, Patrolpoint.Count);
            }
            enemyani.SetBool("IsMove", true);
        }
    }
    
    private void attacking()
    {
        Quaternion rot = Quaternion.LookRotation(PlayerTr.position - EnemyTr.position);
        EnemyTr.rotation = Quaternion.Slerp(EnemyTr.rotation, rot, Time.deltaTime * 10f);

        attacktime += Time.deltaTime;
        if(attacktime >= attackcooltime)
        {
            attacktime = 0;
            enemyani.SetTrigger("IsAttack");
        }
    }

    void Attack()
    {
        attackcolider.enabled = true;
    }
    void AttackEnd()
    {
        attackcolider.enabled = false;
    }
}
