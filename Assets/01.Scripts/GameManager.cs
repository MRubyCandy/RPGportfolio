using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DataInfo;

public class GameManager : MonoBehaviour
{
    //private static GameManager _gameManager;
    public static GameManager gameManager;
    //{
    //    get
    //    {
    //        if (!_gameManager)
    //        {
    //            _gameManager = FindObjectOfType(typeof(GameManager)) as GameManager;
    //            if (_gameManager == null)
    //                Debug.Log("no singleton obj");
    //        }
    //        return _gameManager;
    //    }
    //}
    public GameData GameData;
    private CanvasGroup SkillCG;
    private bool Skillopen = false;
    private CanvasGroup InventoryCG;
    private bool Inventoryopen = false;
    private CanvasGroup ItemdropCG;
    public List<GameObject> dropItems;
    private GameObject invenitem;
    [SerializeField]
    private List<Transform> invenitemlist;
    private List<GameObject> QuestList;
    [SerializeField]
    public static bool QuestProgress = false;
    private CanvasGroup QuestShowObjectCG;
    public int GameMoney;
    [SerializeField]
    private Text moneytext;
    private AudioSource cameraaudio;
    private AudioClip menusfx;
    [SerializeField]
    private Transform Weaponequit;
    private InvenItemicon equitWeapon;
    public float statdmg = 0;
    public bool isgameover = false;
    private CanvasGroup MenuCG;
    private bool Menucgopen = false;
    private CanvasGroup DebugCG;

    private void Awake()
    {
        if (gameManager == null)
        {
            gameManager = this;
        }
        else if (gameManager != this)
        {
            Destroy(gameObject);
        }
        //DontDestroyOnLoad(gameObject); //씬이넘어갈때 씬리로딩할때 오류가생김
    }

    void Start()
    {
        SkillCG = GameObject.Find("Panel-Skill").GetComponent<CanvasGroup>();
        InventoryCG = GameObject.Find("Panel-Inventory").GetComponent<CanvasGroup>();
        ItemdropCG = GameObject.Find("Panel-DropItem").GetComponent<CanvasGroup>();
        invenitem = Resources.Load<GameObject>("ItemIcon");
        QuestShowObjectCG = GameObject.Find("Panel-QuestObject").GetComponent<CanvasGroup>();
        moneytext = InventoryCG.transform.GetChild(2).GetChild(0).GetComponent<Text>();
        Weaponequit = GameObject.Find("Panel-Equit").transform.GetChild(0).GetComponent<Transform>();
        cameraaudio = GameObject.FindWithTag("SFXaudioCtrl").GetComponent<AudioSource>();
        menusfx = Resources.Load<AudioClip>("Uisfx/Menuopen");
        MenuCG = GameObject.Find("Panel-Menu").GetComponent<CanvasGroup>();
        DebugCG = GameObject.Find("Panel-Debug").GetComponent<CanvasGroup>();

        isgameover = false;
        var invenlist = InventoryCG.transform.GetChild(1);
        if (invenlist != null)
        {
            invenlist.GetComponentsInChildren<Transform>(invenitemlist);
            invenitemlist.RemoveAt(0);
        }
    }


    void Update()
    {
        SkillOpen();
        InvenOpen();
        MenuOpen();
        QuestShowObjectCtr();
        Getdropitem();
        equititemcheck();

        moneytext.text = string.Format("{0}", GameMoney.ToString()) + " Gold";

        if (ItemdropCG.transform.childCount <= 0)
        {
            ItemdropCG.alpha = 0.0f;
            ItemdropCG.blocksRaycasts = false;
            ItemdropCG.interactable = false;
        }

    }


    private void equititemcheck()
    {
        if(Weaponequit.transform.childCount != 0)
        {
            equitWeapon = Weaponequit.GetChild(0).GetComponent<InvenItemicon>();
            switch (equitWeapon.itemdata.Itemtype)
            {
                case ItemData.itemtype.LongSword:
                    statdmg = GameData.Damage + 10f;
                    break;
                case ItemData.itemtype.NormalSword:
                    statdmg = GameData.Damage + 5f;
                    break;
            }
        }
        else
        {
            statdmg = GameData.Damage;
            equitWeapon = null;
        }
    }

    private void Getdropitem()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (dropItems.Count > 0)
            {
                for (int i = 0; i <= invenitemlist.Count; i++)
                {
                    if (invenitemlist[i].transform.childCount == 0)
                    {
                        var invenitemobj = Instantiate(invenitem, invenitemlist[i].transform);
                        invenitemobj.GetComponent<InvenItemicon>().itemdata.Itemtype =
                            dropItems[0].GetComponent<ItemDrop>().itemdata.Itemtype;
                        break;
                    }
                }
                Destroy(dropItems[0]);
                dropItems.RemoveAt(0);
            }
        }
    }
    public void Debugopen(bool cgopen)
    {
        UICGonoff(DebugCG, cgopen);
    }
    private void MenuOpen()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            cameraaudio.PlayOneShot(menusfx);
            if (!Menucgopen)
                Menucgopen = true;
            else
                Menucgopen = false;
            UICGonoff(MenuCG, Menucgopen);
        }
    }
    private void InvenOpen()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            cameraaudio.PlayOneShot(menusfx);
            if (!Inventoryopen)
                Inventoryopen = true;
            else
                Inventoryopen = false;
            UICGonoff(InventoryCG, Inventoryopen);
        }
    }

    private void SkillOpen()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            cameraaudio.PlayOneShot(menusfx);
            if (!Skillopen)
                Skillopen = true;
            else
                Skillopen = false;
            UICGonoff(SkillCG, Skillopen);
        }
    }

    //private void SkillCGOnOff(bool IsOpen)
    //{
    //    SkillCG.alpha = (IsOpen) ? 1.0f : 0.0f;
    //    SkillCG.interactable = IsOpen;
    //    SkillCG.blocksRaycasts = IsOpen;
    //}
    //private void InventoryOnOff(bool IsOpen)
    //{
    //    InventoryCG.alpha = (IsOpen) ? 1.0f : 0.0f;
    //    InventoryCG.interactable = IsOpen;
    //    InventoryCG.blocksRaycasts = IsOpen;
    //}
    private void UICGonoff(CanvasGroup CG, bool IsOpen)
    {
        CG.alpha = (IsOpen) ? 1.0f : 0.0f;
        CG.interactable = IsOpen;
        CG.blocksRaycasts = IsOpen;
    }
    private void QuestShowObjectCtr()
    {
        bool onoff = false;
        if (QuestShowObjectCG.transform.childCount == 0)
            onoff = false;
        else
            onoff = true;

        QuestShowObjectCG.alpha = (onoff) ? 1.0f : 0.0f;
        QuestShowObjectCG.interactable = onoff;
        QuestShowObjectCG.blocksRaycasts = onoff;
    }
}
