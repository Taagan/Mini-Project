using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossModel : MonoBehaviour
{
    [SerializeField] protected float m_Health;
    [SerializeField] protected float m_Stamina;

    public float Health => m_Health;
    public float MaxHealth { get; protected set; }

    public float Stamina => m_Stamina;

    // ...

    protected virtual void Attack() { }
    protected virtual void Chase() { }
    protected virtual void Idle() { }
    protected virtual void Rest() { }
}
