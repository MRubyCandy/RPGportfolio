using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UILookCamera : MonoBehaviour
{
    private RectTransform UIrtr;
    private Transform CamArmtr;
    private Transform Camtr;

    void Start()
    {
        UIrtr = this.GetComponent<RectTransform>();
        CamArmtr = GameObject.FindWithTag("Player").transform.GetChild(1).GetComponent<Transform>(); //Y좌표
        Camtr = GameObject.FindWithTag("Player").transform.GetChild(1).GetChild(0).GetComponent<Transform>(); //X좌표
    }

    // Update is called once per frame
    void Update()
    {

        UIrtr.rotation = Quaternion.Euler(Camtr.eulerAngles.x, CamArmtr.eulerAngles.y, 0f);

    }
}
