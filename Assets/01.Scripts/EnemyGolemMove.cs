using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyGolemMove : MonoBehaviour
{
    private Transform enemytr;
    private Transform playertr;
    private NavMeshAgent enemynav;
    private Animator enemyani;
    private Transform throwrockpoint;
    public Transform rockfirepoint;
    private GameObject rock;
    private float playerdist = 0f;
    private float throwattacktime = 0f;
    private float throwatkcooltime = 3f;
    private float meleeattacktime = 0f;
    private float meleeatkcooltime = 3f;

    void Start()
    {
        enemytr = gameObject.GetComponent<Transform>();
        playertr = GameObject.FindWithTag("Player").GetComponent<Transform>();
        enemynav = gameObject.GetComponent<NavMeshAgent>();
        enemyani = gameObject.GetComponent<Animator>();
        rock = Resources.Load<GameObject>("GolemRock");
    }

    
    void Update()
    {
        if (gameObject.GetComponent<EnemyHealth_objpooling>().isdie)
            return;

        playerdist = (playertr.transform.position - enemytr.transform.position).magnitude;
        if(playerdist <= 3f)//근접공격
        {
            meleeattacktime += Time.deltaTime;
            lookplayer();
            if (meleeattacktime >= meleeatkcooltime)
            {
                meleeattacktime = 0f;
                enemyani.SetTrigger("MeleeAttack");

            }
        }
        else if(playerdist <= 10f)//돌던지기공격
        {
            throwattacktime += Time.deltaTime;
            lookplayer();
            if (throwattacktime >= throwatkcooltime)
            {
                throwattacktime = 0f;
                enemyani.SetTrigger("ThrowAttack");

            }
        }
        else //가만히
        {

        }

    }
    private void lookplayer()
    {
        Quaternion rot = Quaternion.LookRotation(playertr.position - enemytr.position);
        enemytr.rotation = Quaternion.Slerp(enemytr.rotation, rot, Time.deltaTime * 10f);
    }

    private void throwattack()
    {
        rockfirepoint.LookAt(playertr.transform);
        Instantiate(rock, rockfirepoint.position, rockfirepoint.rotation);
        Debug.Log("throwattack");
    }
    private void meleeattack()
    {
        if (playerdist <= 3)
        {
            playertr.GetChild(0).GetComponent<PlayerHealth>().OnDamage(60f);
        }
        Debug.Log("meleeattack");
    }
}
