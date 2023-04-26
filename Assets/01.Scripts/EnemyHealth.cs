using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    private Transform enemytr;
    Animator EHani;
    float hpinit = 50;
    float hp;
    public bool isdie = false;
    private CapsuleCollider bodycolider;
    [SerializeField]
    QuestObject questObject;
    private SlimeSpawner slimeSpawner;
    private float mobdeletetime = 3f;
    [SerializeField]
    private float nowtime = 0;
    private GameObject[] dropitems;
    private Image HPbar;

    void Start()
    {
        enemytr = this.gameObject.GetComponent<Transform>();
        EHani = GetComponent<Animator>();
        hp = hpinit;
        bodycolider = gameObject.GetComponent<CapsuleCollider>();
        slimeSpawner = GameObject.Find("SlimeSpawner").GetComponent<SlimeSpawner>();
        dropitems = Resources.LoadAll<GameObject>("Weapon");
        HPbar = transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Image>();
    }

    void Update()
    {
        if (GameManager.QuestProgress)//퀘스트진행시
            questObject = GameObject.Find("QuestObject").GetComponent<QuestObject>();
        else
            questObject = null;

        HPbar.fillAmount = hp / hpinit;

        if(isdie)
            nowtime += Time.deltaTime;
        if (nowtime >= mobdeletetime)
        {
            Destroy(this.gameObject);
        }
    }

    public void OnDamage(float Damage)
    {
        if (isdie)
            return;
        Debug.Log("get damage");

        hp -= Damage;
        EHani.SetTrigger("IsHit");
        hp = Mathf.Clamp(hp, 0f, hpinit);

        EnemyKill();

    }

    private void EnemyKill()
    {
        if (hp <= 0)
        {
            slimeSpawner.monstercount--;
            isdie = true;
            bodycolider.isTrigger = true;
            EHani.SetTrigger("IsDie");
            randomitemdrop();
            if(GameManager.QuestProgress)
            {
                questObject.MonsterKill++;
            }
            Debug.Log("Enemy Die");
        }
    }

    private void randomitemdrop()
    {
        int randomcount = Random.Range(0, 100);
        if (randomcount > 70)
        {
            int itemincount = Random.Range(0, 1);
            Instantiate(dropitems[itemincount], enemytr.position, enemytr.rotation);
        }
    }
}
