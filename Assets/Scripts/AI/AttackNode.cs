using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackNode : Node
{
    public AttackNode(BT_Tree bt) : base(bt) { }




    public override NodeState Execute()
    {
        //Give Extra stuff for attack(Currently no more info needed)
        bt.Attack();
        return NodeState.Success;
    }
}
