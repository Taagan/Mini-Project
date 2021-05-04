using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phase_I : Node
{
    public Phase_I(Enemy enemy) : base(enemy) { }

    public override NodeState Execute()
    {
        int oneHp = enemy.MaxHp / 100;
        if (enemy.Hp >= oneHp * 80)
        {
            enemy.CurrentPhase = Enemy.Phase.I;
            enemy.Attack();
            Debug.Log("Phase 1");
            return NodeState.Success;
            
        }
        return NodeState.Failure;
    }
}
