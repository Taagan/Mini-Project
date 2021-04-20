using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Enemy()
    {

    }


    public virtual void Attack() { }

    public virtual void Flee() { }

    public virtual void Idle() { }

}
