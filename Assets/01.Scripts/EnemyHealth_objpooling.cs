using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class EnemyHealth_objpooling : MonoBehaviour
{
    private Transform Enemyobj;
    private Animator enemyani;
    private float hpinit = 150f;
    private float hp = 0f;
    public bool isdie = false;
    [SerializeField]
    private float disabletime = 0f;
    [SerializeField]
    private Image HPbar;

    
    private void OnEnable()
    {
        isdie = false;
        hp = hpinit;
    }
    void Start()
    {
        Enemyobj = gameObject.transform.parent.GetComponent<Transform>();
        enemyani = gameObject.GetComponent<Animator>();
        HPbar = gameObject.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Image>();
    }

    
    void Update()
    {
        disabletimecheck();

        HPbar.fillAmount = hp / hpinit;

    }

    private void disabletimecheck()
    {
        if (isdie && disabletime <= 5f)
        {
            disabletime += Time.deltaTime;
        }
    }

    public void awakeenemy()
    {
        isdie = false;
        hp = hpinit;
    }

    public void OnDamage(float damage)
    {
        if (isdie)
            return;

        hp -= damage;
        if (hp <= 0)
        {
            enemydie();
        }
        enemyani.SetTrigger("Ishit");
    }

    private void enemydie()
    {
        isdie = true;
        enemyani.SetTrigger("Isdie");
        StartCoroutine(disableenemy());
    }

    IEnumerator disableenemy()
    {
        yield return new WaitForSeconds(5.0f);
        Enemyobj.gameObject.SetActive(false);
    }
}
