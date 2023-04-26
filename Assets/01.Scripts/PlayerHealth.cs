using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    private Animator playerani;
    private Image HPbar;
    private Image MPbar;
    private Text HPtext;
    private Text MPtext;
    public float PlayerHP;
    public float PlayerMP;
    private float regentime = 1f;
    private float nowtime = 0f;
    public bool playerdie = false;

    void Start()
    {
        playerani = gameObject.GetComponent<Animator>();
        HPbar = GameObject.Find("UI").transform.GetChild(0).transform.GetChild(0).GetComponent<Image>();
        MPbar = GameObject.Find("UI").transform.GetChild(1).transform.GetChild(0).GetComponent<Image>();
        HPtext = GameObject.Find("UI").transform.GetChild(0).transform.GetChild(1).GetComponent<Text>();
        MPtext = GameObject.Find("UI").transform.GetChild(1).transform.GetChild(1).GetComponent<Text>();
        PlayerHP = GameManager.gameManager.GameData.HP;
        PlayerMP = GameManager.gameManager.GameData.MP;
    }

    
    void Update()
    {
        UpdateHpMp();
        rezenHPMP();

    }

    private void rezenHPMP()
    {
        nowtime += Time.deltaTime;
        if (nowtime >= regentime)
        {
            nowtime = 0;
            PlayerHP = Mathf.Clamp(PlayerHP + 1f, 0, GameManager.gameManager.GameData.HP);
            PlayerMP = Mathf.Clamp(PlayerMP + 1f, 0, GameManager.gameManager.GameData.MP);
        }
    }

    private void UpdateHpMp()
    {
        HPbar.fillAmount = PlayerHP / GameManager.gameManager.GameData.HP;
        MPbar.fillAmount = PlayerMP / GameManager.gameManager.GameData.MP;
        HPtext.text = string.Format("{0}" + "/" + "{1}", PlayerHP, GameManager.gameManager.GameData.HP).ToString();
        MPtext.text = string.Format("{0}" + "/" + "{1}", PlayerMP, GameManager.gameManager.GameData.MP).ToString();
    }

    public void OnDamage(float damage)
    {
        PlayerHP = Mathf.Clamp(PlayerHP - damage, 0, GameManager.gameManager.GameData.HP);
        if(PlayerHP <= 0)
        {
            PlayerDie();
        }
    }

    public void PlayerDie()
    {
        playerdie = true;
        playerani.SetTrigger("Isdie");
        GameManager.gameManager.isgameover = true;
    }
}
