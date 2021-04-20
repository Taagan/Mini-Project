using UnityEngine;

public class NormalEnemy : Enemy
{
    [SerializeField]protected GameObject player;
    [SerializeField]protected int hp;
    [SerializeField]protected int speed;
    [SerializeField]protected int atkDamage;

    public NormalEnemy()
    {
            
    }

    public override void Attack()
    {
        //Attack stuff
    }

    public override void Flee()
    {
        //Flee stuff
    }

    public override void Idle()
    {
        //Idle stuff
    }
}
