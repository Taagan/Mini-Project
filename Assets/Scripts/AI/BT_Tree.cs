using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BT_Tree : MonoBehaviour
{
    [SerializeField]Enemy enemy;

    public BT_Tree()
    {

    }

    public void Attack()
    {
        enemy.Attack();
    }

    public void Flee()
    {
        enemy.Flee();
    }

    public void Idle()
    {
        enemy.Idle();
    }
}
