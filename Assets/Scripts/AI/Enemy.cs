using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] protected GameObject player;
    [SerializeField] protected int hp;
    [SerializeField] protected int speed;
    [SerializeField] protected int atkDamage;
    [SerializeField] protected int aggroRange;
    [SerializeField] protected int stamina;
    [SerializeField] protected double courage;

    public Enemy()
    {

    }

    public virtual GameObject Player { get { return player; } }

    public virtual int Hp { get { return hp; } }

    public virtual int Speed { get { return speed; } }

    public virtual int AtkDamage { get { return atkDamage; } }

    public virtual int AggroRange { get { return aggroRange; } }

    public virtual int Stamina { get { return stamina; } }

    public virtual double Courage { get { return courage; } }


    public virtual void Execute() { }

    public virtual void Attack() { }

    public virtual void Flee() { }

    public virtual void Idle() { }

}