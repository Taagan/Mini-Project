using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequenceNode : Node
{
    public SequenceNode(BT_Tree bt) : base(bt) { }




    public override NodeState Execute()
    {
        for (int i = 0; i < children.Count; i++)
            if (children[i].Execute() == NodeState.Failure)
                return NodeState.Failure;

        return NodeState.Success;
    }
}
