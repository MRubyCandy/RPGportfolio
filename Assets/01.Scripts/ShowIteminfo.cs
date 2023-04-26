using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowIteminfo : MonoBehaviour
{
    public Text Itemtext;

    void Start()
    {
        Itemtext = transform.GetChild(0).GetComponent<Text>();
    }

    
    void Update()
    {
        
    }

    private void OnDisable()
    {
        
    }
}
