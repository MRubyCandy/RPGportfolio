using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameSceneManager : MonoBehaviour
{
    public static GameSceneManager gamesceneManager;
    private Image padepanel;
    [SerializeField]
    private float alphavalue = 0;
    private bool ispadein = false;
    private bool returntitle = false;

    private void Awake()
    {
        if (gamesceneManager == null)
            gamesceneManager = this;
        else if (gamesceneManager != this)
            Destroy(gameObject);
                
        //DontDestroyOnLoad(gameObject); //사용시 씬이 로드중멈춤
    }

    void Start()
    {
        padepanel = GameObject.Find("Panel-Pade").GetComponent<Image>();
        alphavalue = 1;

        StartCoroutine(padein());
    }
    
    void Update()
    {
        padepanel.color = new Color(0, 0, 0, alphavalue);
        padeingamestart(ispadein);
        returntotitle(returntitle);

        var isgameover = GameManager.gameManager;
        if (isgameover !=null)
            padeoutgameover(isgameover.isgameover);

    }

    public void Startgame()
    {
        ispadein = true;
    }
    public void TitleButton()
    {
        returntitle = true;
    }

    private void returntotitle(bool ispade)
    {
        if (!ispade)
            return;
        padepanel.color = new Color(0, 0, 0, alphavalue);
        if (alphavalue < 1)
        {
            alphavalue += (Time.deltaTime * 1.2f);
        }
        else if (alphavalue >= 1)
        {
            SceneManager.LoadScene("LobbyScene");
        }
    }

    private void padeingamestart(bool ispade)
    {
        if (!ispade)
            return;
        padepanel.color = new Color(0, 0, 0, alphavalue);
        if (alphavalue < 1)
        {
            alphavalue += (Time.deltaTime * 1.2f);
        }
        else if (alphavalue >= 1)
        {
            SceneManager.LoadScene("SampleScene");
        }
    }

    private void padeoutgameover(bool ispade)
    {
        if (!ispade)
            return;
        padepanel.color = new Color(0, 0, 0, alphavalue);
        if (alphavalue <= 1)
        {
            alphavalue += (Time.deltaTime * 1.3f);
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    IEnumerator padein()
    {
        while (alphavalue >= 0 )
        {
            yield return new WaitForSeconds(Time.deltaTime * 1.25f);
            alphavalue -= 0.015f;
        }
    }
}
