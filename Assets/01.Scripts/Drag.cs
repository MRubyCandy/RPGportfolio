using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DataInfo;

public class Drag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Transform PlayerTr;
    private Transform itemtr;
    private GameObject itemLongsword;
    private GameObject itemNormalsword;
    private GameObject hppotion;
    private GameObject mppotion;
    private RectTransform itemrtr;
    private CanvasGroup canvasGroup;
    private Transform UICanvas;
    public static bool Dragging = false;
    public static GameObject Dragitem = null;
    public static Transform beforedragtr;

    private void Start()
    {
        PlayerTr = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        itemLongsword = Resources.Load<GameObject>("Weapon/LongSword");
        itemNormalsword = Resources.Load<GameObject>("Weapon/NormalSword");
        hppotion = Resources.Load<GameObject>("HPpotion");
        mppotion = Resources.Load<GameObject>("MPpotion");
        itemtr = GetComponent<Transform>();
        itemrtr = GetComponent<RectTransform>();
        UICanvas = GameObject.Find("UI").GetComponent<Transform>();
        canvasGroup = GetComponent<CanvasGroup>();

        itemrtr.localScale = new Vector3(1.0f, 1.0f, 1.0f);
    }

    void Update()
    {
        
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        beforedragtr = this.transform.parent;
        Dragging = true;
        Dragitem = this.gameObject;
        itemtr.transform.SetParent(UICanvas);
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        itemtr.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;
        if(Dragitem.transform.parent == UICanvas) //아이템을 땅에 버렸을때
        {
            if(this.gameObject.tag == "InvenItem" || this.gameObject.tag =="PotionItem")
            {
                switch(this.gameObject.GetComponent<InvenItemicon>().itemdata.Itemtype)
                {
                    case ItemData.itemtype.LongSword:
                        Instantiate(itemLongsword, PlayerTr.position, PlayerTr.rotation);
                        break;
                    case ItemData.itemtype.NormalSword:
                        Instantiate(itemNormalsword, PlayerTr.position, PlayerTr.rotation);
                        break;
                    case ItemData.itemtype.RedPotion:
                        Instantiate(hppotion, PlayerTr.position, PlayerTr.rotation);
                        break;
                    case ItemData.itemtype.BluePotion:
                        Instantiate(mppotion, PlayerTr.position, PlayerTr.rotation);
                        break;
                }
            }
            Destroy(this.gameObject);
        }
        if (Dragitem != null)
            Dragitem = null;
        Dragging = false;
    }
}
