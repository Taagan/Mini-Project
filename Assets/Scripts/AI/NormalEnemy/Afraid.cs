using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Afraid : Node
{
    public Afraid(Enemy enemy) : base(enemy) { }


    public override NodeState Execute()
    {
        if (enemy.Courage <= 0)
        {
            return NodeState.Success;
        }
        return NodeState.Failure;
    }
}
