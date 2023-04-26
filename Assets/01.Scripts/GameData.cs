using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace DataInfo
{
    [CreateAssetMenu(fileName = "GameDataSO", menuName = "Create GameData", order = 1)]
    public class GameData : ScriptableObject
    {
        public float HP = 500f;
        public float MP = 300f;
        public float Damage = 25f;
        public float Str = 5;
        public float Dex = 5;
        public float Int = 5;
    }
    [System.Serializable]
    public class SkillData
    {
        public enum Skill { SpinAttack, FlameAttack, LightningAttack }
        public Skill skill;
    }
    [System.Serializable]
    public class ItemData
    {
        public enum itemtype { LongSword, ShortSword, NormalSword, RedPotion, BluePotion }
        public itemtype Itemtype;
    }
    [System.Serializable]
    public class QuestType
    {
        public enum Quest { MainQuest, SubQuest, ChainQuest}
        public Quest quest;
    }
}