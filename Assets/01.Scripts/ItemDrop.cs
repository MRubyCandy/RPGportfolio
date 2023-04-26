using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DataInfo;

public class ItemDrop : MonoBehaviour
{
    private CanvasGroup ItemdropCG;
    private CanvasGroup UIpoolCG;
    private GameObject Item;
    private GameObject Itemdrops;
    private GameObject showitem;
    public ItemData itemdata;

    // Start is called before the first frame update
    void Start()
    {
        ItemdropCG = GameObject.Find("Panel-DropItem").GetComponent<CanvasGroup>();
        UIpoolCG = GameObject.Find("Panel-UIpooling").GetComponent<CanvasGroup>();
        Itemdrops = this.gameObject;
        Item = Resources.Load<GameObject>("Item");
        showitem = Instantiate(Item, UIpoolCG.transform);
    }

    // Update is called once per frame
    void Update()
    {
        if (ItemdropCG.transform.childCount <= 0)
        {
            dropitemshow(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            dropitemshow(true);
            GameManager.gameManager.dropItems.Add(Itemdrops);
            showitem.transform.SetParent(ItemdropCG.transform);
            showitem.GetComponent<ShowIteminfo>().Itemtext.text = string.Format("{0}", itemdata.Itemtype.ToString());
            showitem.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(showitem != null || other.tag == "Player")
        {
            GameManager.gameManager.dropItems.Remove(Itemdrops);
            showitem.transform.SetParent(UIpoolCG.transform);
            showitem.SetActive(false);
        }
    }

    void dropitemshow(bool open)
    {
        ItemdropCG.alpha = (open) ? 1.0f : 0.0f;
        ItemdropCG.blocksRaycasts = open;
        ItemdropCG.interactable = open;
    }

    private void OnDisable()
    {
        Destroy(showitem);
    }
}
