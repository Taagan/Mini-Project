using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackNode : Node
{
    public AttackNode(Enemy enemy) : base(enemy) { }




    public override NodeState Execute()
    {
        enemy.Attack();
        return NodeState.Success;
    }
}
