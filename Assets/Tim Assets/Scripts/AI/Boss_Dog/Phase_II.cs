using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phase_II : Node
{
    public Phase_II(Enemy enemy) : base(enemy) { }

    public override NodeState Execute()
    {
        int oneHp = enemy.MaxHp / 100;
        if (enemy.Hp >= oneHp * 50)
        {
            enemy.CurrentPhase = Enemy.Phase.II;
            enemy.Attack();
            return NodeState.Success;
        }
        return NodeState.Failure;
    }
}
