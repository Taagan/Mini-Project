using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected GameObject player;
    [SerializeField] protected int maxHp;
    [SerializeField] protected int hp;
    [SerializeField] protected int speed;
    [SerializeField] protected int atkDamage;
    [SerializeField] protected int aggroRange;
    [SerializeField] protected float stamina;
    [SerializeField] protected float courage;
    [SerializeField] protected bool resting;
    [SerializeField] public enum Phase {I,II,III,IV};
    [SerializeField] protected Phase currentPhase;

    public Enemy()
    {

    }

    protected virtual void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public virtual GameObject Player { get { return player; } }

    public virtual int MaxHp { get { return maxHp; } }

    public virtual int Hp { get { return hp; } }

    public virtual int Speed { get { return speed; } }

    public virtual int AtkDamage { get { return atkDamage; } }

    public virtual int AggroRange { get { return aggroRange; } }

    public virtual float Stamina { get { return stamina; } }

    public virtual float Courage { get { return courage; } }

    public virtual bool Resting { get { return resting; } set { resting = value; } }

    public virtual Phase CurrentPhase { get { return currentPhase; } set { currentPhase = value; } }

    public virtual void Execute() { }

    public virtual void Attack() { }

    public virtual void Flee() { }

    public virtual void Idle() { }



    public virtual void TakeDamage(int damage)
    {
        hp -= damage;
    }
}
