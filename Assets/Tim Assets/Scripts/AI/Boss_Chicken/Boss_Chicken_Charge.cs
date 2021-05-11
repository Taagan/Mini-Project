using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Chicken_Charge : Node
{
    public Boss_Chicken_Charge(Enemy enemy) : base(enemy) { }

    public override NodeState Execute()
    {
        //Quite empty might add stuff later
        return NodeState.Success;
    }
}
