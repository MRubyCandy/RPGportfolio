using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DataInfo;

public class shotitemsell : MonoBehaviour, IDropHandler
{
    private ItemData itemtype;
    void Start()
    {
        
    }

    
    void Update()
    {

    }
    public void OnDrop(PointerEventData eventData)
    {
        if(Drag.Dragitem.tag == "InvenItem" || Drag.Dragitem.tag == "PotionItem")
        {
            Drag.Dragitem.transform.SetParent(this.transform);
            switch(Drag.Dragitem.GetComponent<InvenItemicon>().itemdata.Itemtype)
            {
                case ItemData.itemtype.LongSword:
                    GameManager.gameManager.GameMoney += 100;
                    break;
                case ItemData.itemtype.ShortSword:
                    GameManager.gameManager.GameMoney += 80;
                    break;
                case ItemData.itemtype.NormalSword:
                    GameManager.gameManager.GameMoney += 90;
                    break;
                case ItemData.itemtype.RedPotion:
                    GameManager.gameManager.GameMoney += 20;
                    break;
                case ItemData.itemtype.BluePotion:
                    GameManager.gameManager.GameMoney += 20;
                    break;
            }
            Destroy(Drag.Dragitem);
        }
    }
}
