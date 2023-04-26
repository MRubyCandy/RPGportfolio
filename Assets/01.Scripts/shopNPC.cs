using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class shopNPC : MonoBehaviour
{
    private CanvasGroup shopcanvas;
    private CanvasGroup talkBT;
    //private bool shoponoff = false;
    private bool approchnpc = false;

    void Start()
    {
        shopcanvas = GameObject.Find("Panel-shop").GetComponent<CanvasGroup>();
        talkBT = GameObject.Find("Panel-Talk").GetComponent<CanvasGroup>();
    }

    
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F) && approchnpc)
        {
            cgopen(shopcanvas, true);
        }
    }

    private void cgopen(CanvasGroup CG, bool onoff)
    {
        CG.alpha = (onoff) ? 1.0f : 0.0f;
        CG.blocksRaycasts = onoff;
        CG.interactable = onoff;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            approchnpc = true;
            cgopen(talkBT, true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            approchnpc = false;
            cgopen(talkBT, false);
            cgopen(shopcanvas, false);
        }
    }
}
