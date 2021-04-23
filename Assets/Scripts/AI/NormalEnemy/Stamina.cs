using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stamina : Node
{
    public Stamina(Enemy enemy) : base(enemy) { }


    public override NodeState Execute()
    {
        if (enemy.Stamina > 0)
        {
            return NodeState.Success;
        }
        return NodeState.Failure;
    }
}
