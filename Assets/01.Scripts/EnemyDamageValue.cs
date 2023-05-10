using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamageValue : MonoBehaviour
{
    private PlayerHealth Player;
    private BoxCollider attackcolider;
    
    void Start()
    {
        Player = GameObject.FindWithTag("Player").transform.GetChild(0).GetComponent<PlayerHealth>();
        attackcolider = gameObject.GetComponent<BoxCollider>();
    }

    
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Player.OnDamage(50f);
        }
    }
}
