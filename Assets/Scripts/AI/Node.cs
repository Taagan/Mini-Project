using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public abstract class Node
{
    public enum NodeState { Success, Failure, Running };
    protected NodeState currentNodeState { get { return currentNodeState; } }
    protected List<Node> children;
    protected BT_Tree bt;


    public Node(BT_Tree bt)
    {
        this.bt = bt;
        children = new List<Node>();
    }


    public virtual NodeState Execute()
    {
        return currentNodeState;
    }
    protected void AddChild(Node child)
    {
        children.Add(child);
    }
}
