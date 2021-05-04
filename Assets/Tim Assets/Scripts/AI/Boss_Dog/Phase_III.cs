using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phase_III : Node
{
    public Phase_III(Enemy enemy) : base(enemy) { }

    public override NodeState Execute()
    {
        int oneHp = enemy.MaxHp / 100;
        if (enemy.Hp >= oneHp * 25)
        {
            enemy.CurrentPhase = Enemy.Phase.III;
            enemy.Attack();
            return NodeState.Success;
        }
        return NodeState.Failure;
    }
}
