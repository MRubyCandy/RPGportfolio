using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMove : MonoBehaviour
{
    public static Transform PlayerTr;
    private PlayerHealth phealth;
    private Transform CamArm;
    Animator Playerani;
    public float Speed = 5f;
    public bool usingskill = false;
    private float spinattackCT = 1.5f;
    [SerializeField]
    private float spinattackNT = 0f;
    private float flameattackCT = 1.5f;
    [SerializeField]
    private float flameattackNT = 0f;
    private float lightningattackCT = 1.5f;
    [SerializeField]
    private float lightningattackNT = 0f;

    void Start()
    {
        PlayerTr = this.transform.GetChild(0).GetComponent<Transform>();
        CamArm = this.transform.GetChild(1).GetComponent<Transform>();
        Playerani = PlayerTr.transform.GetComponent<Animator>();
        phealth = this.transform.GetChild(0).GetComponent<PlayerHealth>();
    }

    void FixedUpdate()
    {
        Move();
        _ComboAttack();

        //쿨타임스케쥴러
        spinattackNT = Mathf.Clamp(spinattackNT - Time.deltaTime, 0f, spinattackCT);
        flameattackNT = Mathf.Clamp(flameattackNT - Time.deltaTime, 0f, flameattackCT);
        lightningattackNT = Mathf.Clamp(lightningattackNT - Time.deltaTime, 0f, lightningattackCT);

    }

    private void Move()
    {
        Vector2 moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        bool ismove = moveInput.magnitude != 0;
        Playerani.SetBool("IsMove", ismove);
        if (ismove)
        {
            Vector3 lookForward = new Vector3(CamArm.forward.x, 0f, CamArm.forward.z).normalized;
            Vector3 lookRight = new Vector3(CamArm.right.x, 0f, CamArm.right.z).normalized;
            Vector3 moveDir = lookForward * moveInput.y + lookRight * moveInput.x;

            PlayerTr.forward = moveDir;
            transform.position += moveDir.normalized * Time.deltaTime * Speed;
        }
    }

    private void _ComboAttack()
    {
        if (EventSystem.current.IsPointerOverGameObject() ||
            Drag.Dragging)
            return;
        if (Input.GetMouseButton(0))
        {
            Playerani.SetBool("IsCombo", true);
        }
        else
        {
            Playerani.SetBool("IsCombo", false);
        }
    }

    public void _SpinAttack()
    {
        if (spinattackNT == 0 && !usingskill && phealth.PlayerMP >= 20f)
        {
            usingskill = true;
            spinattackNT = spinattackCT;
            phealth.PlayerMP -= 20f;
            Playerani.SetTrigger("SpinAttack");
        }
    }
    public void _FlameAttack()
    {
        if (flameattackNT == 0 && !usingskill && phealth.PlayerMP >= 30f)
        {
            usingskill = true;
            flameattackNT = flameattackCT;
            phealth.PlayerMP -= 30f;
            Playerani.SetTrigger("FlameAttack");
        }
    }
    public void _LightningAttack()
    {
        if (lightningattackNT == 0 && !usingskill && phealth.PlayerMP >= 30f)
        {
            usingskill = true;
            lightningattackNT = lightningattackCT;
            phealth.PlayerMP -= 30f;
            Playerani.SetTrigger("LightningAttack");
        }
    }

}
