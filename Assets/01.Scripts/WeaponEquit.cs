using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class WeaponEquit : MonoBehaviour, IDropHandler
{
    private Transform inslotweapon;

    void Update()
    {
        if(this.transform.childCount != 0)
        {
            inslotweapon = this.transform.GetChild(0).GetComponent<Transform>();
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (transform.childCount != 0)
        {
            inslotweapon.SetParent(Drag.beforedragtr);
        }

        if (Drag.Dragitem.GetComponent<InvenItemicon>().itemdata.Itemtype == DataInfo.ItemData.itemtype.LongSword ||
            Drag.Dragitem.GetComponent<InvenItemicon>().itemdata.Itemtype == DataInfo.ItemData.itemtype.NormalSword)
        {
            Drag.Dragitem.transform.SetParent(this.transform);
        }
    }
}
