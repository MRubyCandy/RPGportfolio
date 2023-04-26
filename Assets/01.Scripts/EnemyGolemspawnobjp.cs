using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGolemspawnobjp : MonoBehaviour
{
    public GameObject golem;
    private Transform spawnpoint;

    void Start()
    {
        spawnpoint = GameObject.Find("GolemSpawnPoint").GetComponent<Transform>();

        StartCoroutine(spawngolem());
    }


    void Update()
    {
        
    }

    IEnumerator spawngolem()
    {
        while (!GameManager.gameManager.isgameover)
        {
            yield return new WaitForSeconds(10f);//10초마다 골렘스폰
            if(golem.gameObject.activeSelf == false)
            {
                golem.gameObject.SetActive(true);
                golem.transform.position = spawnpoint.position;
                Debug.Log("spawn");
            }
        }
    }
}
