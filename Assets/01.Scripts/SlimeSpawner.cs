using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeSpawner : MonoBehaviour
{
    [SerializeField]
    private List<Transform> spawnpoint;
    public Transform spawnpointset;
    private int randomspawnpoint = 0;
    private GameObject slimemonster;
    private float spawntime = 4f;
    private float remaintime = 0f;
    public int monstercount = 0;
    private int maxmonstercount = 5;
    
    void Start()
    {
        var point = spawnpointset;
        if(point !=null)
        {
            point.GetComponentsInChildren<Transform>(spawnpoint);
            spawnpoint.RemoveAt(0);
        }
        slimemonster = Resources.Load<GameObject>("Slime");
    }

    
    void Update()
    {
        slimespawn();
    }

    private void slimespawn()
    {
        remaintime += Time.deltaTime;
        if(remaintime >= spawntime && monstercount < maxmonstercount)
        {
            remaintime = 0;
            randomspawnpoint = Random.Range(0, spawnpoint.Count);
            monstercount++;
            Instantiate(slimemonster, spawnpoint[randomspawnpoint].position, spawnpoint[randomspawnpoint].rotation);
        }
    }
}
