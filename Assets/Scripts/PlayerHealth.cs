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
        GetComponent<AnimationMovementController>().getHit();
        
        if (HP <= 0)
        {
            GetComponent<AnimationMovementController>().Die();
            
            
            //Time.timeScale = 0.01f;
            GetComponent<MovementInputController>().enabled = false;
            GetComponent<AnimationMovementController>().enabled = false;
            GetComponent<AimController>().enabled = false;
            GetComponent<CapsuleCollider>().enabled = false;
            gameOverMenu.Notify();

        }

        dmgCDTimer = dmgCDTime;
    }

    public void Reset()
    {
        HP = maxHP;

        GetComponent<MovementInputController>().enabled = true;
        GetComponent<AnimationMovementController>().enabled = true;
    }
}
