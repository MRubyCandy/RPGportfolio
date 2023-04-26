using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public Transform CamTr;
    public float damping = 4;
    float r;
    float rotspeed = 180f;
    
    void Start()
    {
        //CamTr = GetComponent<Transform>();
    }

    
    void Update()
    {
        //CamTr.position = PlayerTr.position - (PlayerTr.forward * damping) + (PlayerTr.up * damping);
        //CamTr.LookAt(PlayerTr.position + (PlayerTr.up * 2f));

        r = Input.GetAxis("Mouse X");
        CamTr.Rotate(CamTr.up * r * rotspeed * Time.deltaTime);
    }
}
