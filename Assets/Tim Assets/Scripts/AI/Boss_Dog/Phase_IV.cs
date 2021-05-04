using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phase_IV : Node
{
    public Phase_IV(Enemy enemy) : base(enemy) { }

    public override NodeState Execute()
    {
        enemy.CurrentPhase = Enemy.Phase.IV;
        enemy.Attack();
        return NodeState.Success;       
    }
}
