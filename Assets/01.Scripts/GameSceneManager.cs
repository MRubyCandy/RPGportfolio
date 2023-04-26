using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameSceneManager : MonoBehaviour
{
    private Image padepanel;
    [SerializeField]
    private float alphavalue;
    private bool ispadein;

    void Start()
    {
        padepanel = GameObject.Find("Panel-Pade").GetComponent<Image>();
    }

    
    void Update()
    {
        pade(ispadein);
    }

    public void Startgame()
    {
        ispadein = true;
    }

    private void pade(bool ispade)
    {
        padepanel.color = new Color(0, 0, 0, alphavalue);
        if (alphavalue < 1 && ispade)
        {
            alphavalue += (Time.deltaTime * 1.2f);
        }
        else if (alphavalue >= 1)
        {
            SceneManager.LoadScene("SampleScene");
        }
    }
}
