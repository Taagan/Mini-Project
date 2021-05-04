using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInArena : Node
{
    public PlayerInArena(Enemy enemy) : base(enemy) { }

    public override NodeState Execute()
    {
        //Checks if the player is in the boss arena
        if (/*enemy.Player.in_Arena == true*/true)
        {
            return NodeState.Success;
        }
        return NodeState.Failure;
    }
}
