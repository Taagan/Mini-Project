using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleNode : Node
{
    public IdleNode(BT_Tree bt) : base(bt) { }




    public override NodeState Execute()
    {
        //Give Extra stuff for idle(Currently no more info needed)
        bt.Idle();
        return NodeState.Success;
    }
}
