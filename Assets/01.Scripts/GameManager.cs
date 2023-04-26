using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DataInfo;

public class GameManager : MonoBehaviour
{
    private static GameManager _gameManager;
    public static GameManager gameManager
    {
        get
        {
            if(!_gameManager)
            {
                _gameManager = FindObjectOfType(typeof(GameManager)) as GameManager;
                if (_gameManager == null)
                    Debug.Log("no singleton obj");
            }
            return _gameManager;
        }
    }
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
    private Image Padepanel;
    private float padealpha = 1f;
    public bool isgameover = false;

    private void Awake()
    {
        if (_gameManager == null)
        {
            _gameManager = this;
        }
        else if (_gameManager != this)
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
        cameraaudio = GameObject.FindWithTag("MainCamera").GetComponent<AudioSource>();
        menusfx = Resources.Load<AudioClip>("Uisfx/Menuopen");
        Padepanel = GameObject.Find("Panel-Pade").GetComponent<Image>();

        isgameover = false;
        StartCoroutine(padein());
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
        QuestShowObjectCtr();
        Getdropitem();
        equititemcheck();
        gameoverpadeout();

        Padepanel.color = new Color(0, 0, 0, padealpha);

        moneytext.text = string.Format("{0}", GameMoney.ToString()) + " Gold";

        if (ItemdropCG.transform.childCount <= 0)
        {
            ItemdropCG.alpha = 0.0f;
            ItemdropCG.blocksRaycasts = false;
            ItemdropCG.interactable = false;
        }

    }

    private void gameoverpadeout()
    {
        if(isgameover)
        {
            if (padealpha <= 1)
            {
                Padepanel.color = new Color(0, 0, 0, padealpha);
                padealpha += (Time.deltaTime * 1.3f);
            }
            else
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }
    IEnumerator padein()
    {
        while (padealpha >= 0)
        {
            if (padealpha <= 0)
                break;
            yield return new WaitForSeconds(Time.deltaTime * 1.2f);
            padealpha -= 0.015f;
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

    private void InvenOpen()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            cameraaudio.PlayOneShot(menusfx);
            if (!Inventoryopen)
                Inventoryopen = true;
            else
                Inventoryopen = false;
            InventoryOnOff(Inventoryopen);
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
            SkillCGOnOff(Skillopen);
        }
    }

    private void SkillCGOnOff(bool IsOpen)
    {
        SkillCG.alpha = (IsOpen) ? 1.0f : 0.0f;
        SkillCG.interactable = IsOpen;
        SkillCG.blocksRaycasts = IsOpen;
    }
    private void InventoryOnOff(bool IsOpen)
    {
        InventoryCG.alpha = (IsOpen) ? 1.0f : 0.0f;
        InventoryCG.interactable = IsOpen;
        InventoryCG.blocksRaycasts = IsOpen;
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
