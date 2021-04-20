using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InverterNode : Node
{
    public InverterNode(BT_Tree bt) : base(bt) { }




    public override NodeState Execute()
    {
        if (children[0].Execute() == NodeState.Failure)
            return NodeState.Success;

        else if (children[0].Execute() == NodeState.Success)
            return NodeState.Failure;

        else
            return NodeState.Running;
    }
}
