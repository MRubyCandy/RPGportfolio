using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DataInfo;

public class InvenItemicon : MonoBehaviour
{
    public Text invenitemtext;
    public ItemData itemdata;

    void Start()
    {
        invenitemtext = transform.GetChild(0).GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        invenitemtext.text = string.Format("{0}", itemdata.Itemtype.ToString());

        if(itemdata.Itemtype == ItemData.itemtype.BluePotion || itemdata.Itemtype == ItemData.itemtype.RedPotion)
        {
            this.gameObject.tag = "PotionItem";
        }
    }
}
