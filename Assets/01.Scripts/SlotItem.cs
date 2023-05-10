using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DataInfo;

public class SlotItem : MonoBehaviour
{
    private PlayerMove player;
    [SerializeField]
    private PlayerHealth playerHealth;
    [SerializeField]
    private Transform inslotitem;
    public KeyCode key;

    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<PlayerMove>();
        playerHealth = player.transform.GetChild(0).GetComponent<PlayerHealth>();
    }

    void Update()
    {
        useslotitem();
        checkslot();
    }

    private void checkslot()
    {
        if (transform.childCount != 0)
        {
            inslotitem = transform.GetChild(0);
        }
        else
        {
            inslotitem = null;
        }
    }

    void useslotitem()
    {
        if(Input.GetButtonDown(key.ToString()))
        {
            if (inslotitem != null)
            {
                if (inslotitem.tag == "Skill")
                {
                    switch (inslotitem.GetComponent<SkillType>().skillData.skill)
                    {
                        case SkillData.Skill.SpinAttack:
                            player._SpinAttack();
                            break;
                        case SkillData.Skill.FlameAttack:
                            player._FlameAttack();
                            break;
                        case SkillData.Skill.LightningAttack:
                            player._LightningAttack();
                            break;
                    }
                }
                else if (inslotitem.tag == "PotionItem")
                {
                    switch(inslotitem.GetComponent<InvenItemicon>().itemdata.Itemtype)
                    {
                        case ItemData.itemtype.RedPotion:
                            Debug.Log("hp up");
                            playerHealth.PlayerHP = Mathf.Clamp(playerHealth.PlayerHP += 50f, 0, GameManager.gameManager.GameData.HP);
                            Destroy(inslotitem.gameObject);
                            break;
                        case ItemData.itemtype.BluePotion:
                            Debug.Log("mp up");
                            playerHealth.PlayerMP = Mathf.Clamp(playerHealth.PlayerMP += 50f, 0, GameManager.gameManager.GameData.MP);
                            Destroy(inslotitem.gameObject);
                            break;
                    }
                }
            }
        }
    }
}
