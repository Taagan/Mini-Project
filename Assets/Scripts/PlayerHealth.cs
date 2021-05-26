using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] GameOverMenu gameOverMenu;

    public int maxHP = 100;
    public int HP = 100;

    public float dmgCDTime = .5f;
    public float dmgCDTimer = 0;

    private MovementInputController inputCtrlr;
    private AnimationMovementController mvntCtrlr;
    private AimController aimCtrlr;
    private CapsuleCollider capsCollider;

    private void Awake()
    {
        inputCtrlr = GetComponent<MovementInputController>();
        mvntCtrlr = GetComponent<AnimationMovementController>();
        aimCtrlr = GetComponent<AimController>();
        capsCollider = GetComponent<CapsuleCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Generic_hurt"))
        {
            TakeDamage(10);
        }
    }

    public void Update()
    {
        if (dmgCDTimer > 0)
            dmgCDTimer -= Time.deltaTime;
    }

    public void TakeDamage(int dmg)
    {
        if (dmgCDTimer > 0)
            return;

        HP -= dmg;
        mvntCtrlr.getHit();
        
        if (HP <= 0)
        {
            mvntCtrlr.Die(true);

            inputCtrlr.enabled = false;
            mvntCtrlr.enabled = false;
            aimCtrlr.enabled = false;
            capsCollider.enabled = false;

            gameOverMenu.Notify();
        }

        AudioPlayer.PlayAtPoint("grunt", gameObject.transform.position, 0.5f);

        dmgCDTimer = dmgCDTime;
    }

    public void ResetPlayer()
    {
        HP = maxHP;

        inputCtrlr.enabled = true;
        mvntCtrlr.enabled = true;
        aimCtrlr.enabled = true;
        capsCollider.enabled = true;

        mvntCtrlr.Die(false);
    }
}
