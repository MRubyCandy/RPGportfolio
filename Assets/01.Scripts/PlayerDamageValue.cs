using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamageValue : MonoBehaviour
{
    public Collider NormalAttackcol;
    public Collider SpinAttackcol;
    public Collider ElementAttackcol;
    private GameObject FlameEffect;
    private GameObject LightningEffect;
    private PlayerMove Playermove;
    private AudioSource playeraudio;
    private AudioClip[] walksfx;
    private AudioClip normalatksfx;
    private AudioClip lightningatcsfx;
    private AudioClip flameatksfx;

    private void Start()
    {
        FlameEffect = Resources.Load<GameObject>("FlameEffect");
        LightningEffect = Resources.Load<GameObject>("LightningEffect");
        Playermove = GameObject.FindWithTag("Player").GetComponent<PlayerMove>();
        playeraudio = this.gameObject.GetComponent<AudioSource>();
        walksfx = Resources.LoadAll<AudioClip>("Walksfx");
        normalatksfx = Resources.Load<AudioClip>("Skillsfx/NormalAttacksfx");
        lightningatcsfx = Resources.Load<AudioClip>("Skillsfx/LightningAttacksfx");
        flameatksfx = Resources.Load<AudioClip>("Skillsfx/FlameAttacksfx");
    }

    void NormalAttack()
    {
        playeraudio.PlayOneShot(normalatksfx);
        StartCoroutine(Atkcol(NormalAttackcol));
    }
    void SpinAttack()
    {
        playeraudio.PlayOneShot(normalatksfx);
        StartCoroutine(Atkcol(SpinAttackcol));
    }
    void FlameAttack()
    {
        playeraudio.PlayOneShot(flameatksfx);
        StartCoroutine(ElementAtkcol(ElementAttackcol,FlameEffect));
    }
    void LightningAttack()
    {
        playeraudio.PlayOneShot(lightningatcsfx);
        StartCoroutine(ElementAtkcol(ElementAttackcol, LightningEffect));
    }
    void SkillUsingEnd()
    {
        Playermove.usingskill = false;
    }
    void Walksound()
    {
        int walknum = Random.Range(0, walksfx.Length);
        playeraudio.PlayOneShot(walksfx[walknum]);
    }

    IEnumerator Atkcol(Collider collider)
    {
        collider.enabled = true;
        yield return new WaitForSeconds(0.1f);
        collider.enabled = false;
    }
    IEnumerator ElementAtkcol(Collider collider,GameObject effect)
    {
        collider.enabled = true;
        GameObject skilleff = Instantiate(effect,collider.transform.position, collider.transform.rotation);
        yield return new WaitForSeconds(0.1f);
        collider.enabled = false;
        Destroy(skilleff, 1.9f);
    }
}
