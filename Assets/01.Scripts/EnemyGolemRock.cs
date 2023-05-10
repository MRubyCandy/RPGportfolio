using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGolemRock : MonoBehaviour
{
    private Rigidbody rockrigid;
    private Transform rocktr;

    void Start()
    {
        rockrigid = GetComponent<Rigidbody>();
        rocktr = GetComponent<Transform>();
    }

    
    void Update()
    {
        rocktr.Translate(Vector3.forward * Time.deltaTime * 5);
    }

    private void OnTriggerEnter(Collider other)
    {
        var playerhealth = other.GetComponent<PlayerHealth>();
        if(playerhealth != null)
        {
            playerhealth.OnDamage(100f);
            Debug.Log("givedamage");
        }
        Destroy(gameObject);
    }
}
