using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Drop : MonoBehaviour, IDropHandler
{
    [SerializeField]
    private Transform inslotitem;

    private void Update()
    {
        if(transform.childCount != 0)
        {
            inslotitem = this.transform.GetChild(0).GetComponent<Transform>();
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        if(transform.childCount != 0)
        {
            inslotitem.transform.SetParent(Drag.beforedragtr);
        }
        
        if (Drag.Dragitem.tag == "Skill" || Drag.Dragitem.tag == "PotionItem" && this.gameObject.tag == "Quickslot")
            Drag.Dragitem.transform.SetParent(this.transform);

        if(Drag.Dragitem.tag == "InvenItem" || Drag.Dragitem.tag == "PotionItem" && this.gameObject.tag == "Inventoryslot")
            Drag.Dragitem.transform.SetParent(this.transform);
    }
}
