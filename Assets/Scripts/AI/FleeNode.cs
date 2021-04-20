using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FleeNode : Node
{
    public FleeNode(BT_Tree bt) : base(bt) { }




    public override NodeState Execute()
    {
        //Give Extra stuff for flee(Currently no more info needed)
        bt.Flee();
        return NodeState.Success;
    }
}
