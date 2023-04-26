using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DataInfo;

public class Shopitemprice : MonoBehaviour, IPointerClickHandler
{
    public ItemData itemdata;
    [SerializeField]
    private GameObject itemicon;
    private CanvasGroup ItembuycheckCG;
    private Text ItembuycheckText;
    private Button Itembuycheckyesbutton;
    private Button Itembuychecknobutton;
    [SerializeField]
    private List<Transform> inventory;

    void Start()
    {
        itemicon = Resources.Load<GameObject>("Itemicon");
        ItembuycheckCG = GameObject.Find("Panel-Itembuycheck").GetComponent<CanvasGroup>();
        ItembuycheckText = ItembuycheckCG.transform.GetChild(0).GetComponent<Text>();
        Itembuycheckyesbutton = ItembuycheckCG.transform.GetChild(1).GetComponent<Button>();
        Itembuychecknobutton = ItembuycheckCG.transform.GetChild(2).GetComponent<Button>();

        var invenlist = GameObject.Find("Panel-Inventory").transform.GetChild(1).GetComponent<Transform>();
        if(invenlist != null)
        {
            invenlist.GetComponentsInChildren<Transform>(inventory);
            inventory.RemoveAt(0);
        }
    }

    
    void Update()
    {
        
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        Buythisitem();
    }

    private void Buythisitem()
    {
        Itembuycheckyesbutton.onClick.AddListener(Buyitemtoinven);
        Itembuychecknobutton.onClick.AddListener(Checkcgoff);
        ItembuycheckText.text = (itemdata.Itemtype + " 30골드" + System.Environment.NewLine + "이 아이템을 구매하시겠습니다까?").ToString();
        CGopen(ItembuycheckCG, true);
    }
    

    private void Buyitemtoinven()
    {
        #region 스위치케이스
        //switch(itemdata.Itemtype)
        //{
        //    case ItemData.itemtype.RedPotion:
        //        Setinvenbuyitem(itemicon, ItemData.itemtype.RedPotion);
        //        CGopen(ItembuycheckCG, false);
        //        break;
        //    case ItemData.itemtype.BluePotion:
        //        Setinvenbuyitem(itemicon, ItemData.itemtype.BluePotion);
        //        CGopen(ItembuycheckCG, false);
        //        break;
        //}
        #endregion
        if(GameManager.gameManager.GameMoney < 30)
        {
            Debug.Log("소지골드가 부족합니다.");
            Itembuycheckyesbutton.onClick.RemoveListener(Buyitemtoinven);
            Itembuychecknobutton.onClick.RemoveListener(Checkcgoff);
            CGopen(ItembuycheckCG, false);
            return;
        }
        else
        {
            GameManager.gameManager.GameMoney -= 30;
            Setinvenbuyitem(itemicon, itemdata.Itemtype);
            CGopen(ItembuycheckCG, false);
            Itembuycheckyesbutton.onClick.RemoveListener(Buyitemtoinven);
            Itembuychecknobutton.onClick.RemoveListener(Checkcgoff);
        }
    }

    private void CGopen(CanvasGroup CG, bool onoff)
    {
        CG.alpha = (onoff) ? 1.0f : 0.0f;
        CG.blocksRaycasts = onoff;
        CG.interactable = onoff;
    }
    private void Checkcgoff()
    {
        ItembuycheckCG.alpha = 0.0f;
        ItembuycheckCG.blocksRaycasts = false;
        ItembuycheckCG.interactable = false;
        Itembuycheckyesbutton.onClick.RemoveListener(Buyitemtoinven);
        Itembuychecknobutton.onClick.RemoveListener(Checkcgoff);
    }

    private void Setinvenbuyitem(GameObject itemicon, ItemData.itemtype itemData)
    {
        for (int i = 0; i <= inventory.Count; i++)
        {
            if(inventory[i].transform.childCount == 0)
            {
                var itemiconobj = Instantiate(itemicon, inventory[i].transform);
                itemiconobj.GetComponent<InvenItemicon>().itemdata.Itemtype = itemData;
                break;
            }
        }
    }

}
