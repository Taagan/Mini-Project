using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stamina : Node
{
    public Stamina(Enemy enemy) : base(enemy) { }


    public override NodeState Execute()
    {
        if (enemy.Stamina > 0 && !enemy.Resting)
        {
            //Debug.Log("not resting");
            return NodeState.Success;
        }
        else if (!enemy.Resting)
        {
            enemy.Resting = true;
            return NodeState.Failure;
        }
        return NodeState.Failure;
    }
}
