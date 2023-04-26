using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DataInfo;

public class QuestObject : MonoBehaviour
{
    public QuestType QuestType;
    public List<string> QuestStartScript;
    public List<string> QuestCompleteScript;
    private CanvasGroup InteractionBT;
    [SerializeField]
    private CanvasGroup QuestScriptCG;
    private Text QuestText;
    private bool approachNPC = false;
    [SerializeField]
    private bool QuestReading = false;
    [SerializeField]
    private bool QuestProgress = false;
    [SerializeField]
    private bool QuestComplete = false;
    [SerializeField]
    private bool QuestEnd = false;
    [SerializeField]
    private int QuestTextNumber = 0;
    [SerializeField]
    public int MonsterKill = 0;
    [SerializeField]
    private Transform QuestObjectList;
    private GameObject ShowQuestObject;
    private GameObject SQOdum;
    private Text Questalarmtext;
    
    void Start()
    {
        InteractionBT = GameObject.Find("Panel-Talk").GetComponent<CanvasGroup>();
        QuestScriptCG = GameObject.Find("Panel-QuestScript").GetComponent<CanvasGroup>();
        QuestText = QuestScriptCG.transform.GetChild(0).GetComponent<Text>();
        QuestObjectList = GameObject.Find("Panel-QuestObject").GetComponent<Transform>();
        ShowQuestObject = Resources.Load<GameObject>("QuestObjectText");
        Questalarmtext = gameObject.transform.GetChild(0).GetChild(0).GetComponent<Text>();
    }

    
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.F) && approachNPC && !QuestProgress && !QuestEnd)
        {
            Debug.Log("QuestRead");
            QuestReading = true;
        }

        ReadQuest(QuestReading);
        ProgressQuest(QuestProgress);
        QuestAlarmtextupdate();

    }

    private void QuestAlarmtextupdate()
    {
        if (QuestEnd)
        {
            Questalarmtext.text = ("").ToString();
        }
        else if (QuestComplete)
        {
            Questalarmtext.text = ("!").ToString();
        }
        else if (QuestProgress)
        {
            Questalarmtext.text = ("...").ToString();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && !QuestProgress && !QuestEnd)
        {
            approachNPC = true;
            CGopen(InteractionBT, true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            CGopen(InteractionBT, false);
            approachNPC = false;
            QuestReadReset();
        }
    }

    private void QuestReadReset()
    {
        QuestReading = false;
        QuestTextNumber = 0;
    }

    private void CGopen(CanvasGroup CG, bool onoff)
    {
        CG.alpha = (onoff) ? 1.0f : 0.0f;
        CG.interactable = onoff;
        CG.blocksRaycasts = onoff;
    }

    private void ReadQuest(bool isreading)
    {
        CGopen(QuestScriptCG, isreading);

        if(Input.GetKeyDown(KeyCode.Space) && !QuestComplete)//퀘스트 받기
        {
            if (QuestStartScript.Count - 1 == QuestTextNumber && Input.GetKey(KeyCode.Space))
            {
                GameManager.QuestProgress = true;
                QuestProgress = true;
                QuestReadReset();
                return;
            }
                
            QuestTextNumber += 1;
        }
        else if(Input.GetKeyDown(KeyCode.Space) && QuestComplete)//퀘스트 끝내기
        {
            if (QuestCompleteScript.Count - 1 == QuestTextNumber && Input.GetKey(KeyCode.Space))
            {
                GameManager.QuestProgress = false;
                CGopen(InteractionBT, false);
                QuestEnd = true;
                Destroy(SQOdum);
                QuestReadReset();
                return;
            }

            QuestTextNumber += 1;
        }

        if (!QuestComplete)
            QuestText.text = QuestStartScript[QuestTextNumber].ToString();
        else if (QuestComplete)
            QuestText.text = QuestCompleteScript[QuestTextNumber].ToString();

        if(Input.GetKeyDown(KeyCode.Escape))
            QuestReadReset();
    }

    private void ProgressQuest(bool QuestStart)
    {
        if(QuestStart)
        {
            if (SQOdum == null)
                SQOdum = Instantiate(ShowQuestObject, QuestObjectList.transform);
            if (SQOdum != null)
                SQOdum.GetComponent<Text>().text = "슬라임퇴치: " + MonsterKill + " / 5".ToString();

            if (MonsterKill >= 5)
            {
                QuestComplete = true;
                QuestProgress = false;
            }
        }
    }
}
