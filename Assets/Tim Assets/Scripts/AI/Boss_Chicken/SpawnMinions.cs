using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMinions : Node
{
    public SpawnMinions(Enemy enemy) : base(enemy) { }

    public override NodeState Execute()
    {
        //Chicken_Boss phaseI == Charging 
        //Chicken_Boss phaseII == Resting 
        if (enemy.CurrentPhase == Enemy.Phase.II)
        {
            return NodeState.Success;
        }
        return NodeState.Failure;
    }
}
