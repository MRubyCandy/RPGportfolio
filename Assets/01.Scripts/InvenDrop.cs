using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InvenDrop : MonoBehaviour, IDropHandler
{
    [SerializeField]
    private List<Transform> invenlist;

    void Start()
    {
        var invenlist_ = GameObject.Find("Panel-Items");
        if(invenlist_ != null)
        {
            invenlist_.GetComponentsInChildren<Transform>(invenlist);
            invenlist.RemoveAt(0);
        }
    }

    
    void Update()
    {
        
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (Drag.Dragitem.tag == "InvenItem" || Drag.Dragitem.tag == "PotionItem")
        {
            for (int i = 0; i <= invenlist.Count; i++)
            {
                if(invenlist[i].transform.childCount == 0 )
                {
                    Drag.Dragitem.transform.SetParent(invenlist[i].transform);
                    break;
                }
            }
        }
    }
}
