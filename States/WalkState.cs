using UnityEngine;
using System.Collections;

public class WalkState : BaseState
{
    public WalkState( BaseEnemy enemy ) : base(enemy)
    {

    }

    public override void StateUpdate( )
    {
        enemy.Walk(enemy.walkSpeed);
        if(enemy.transform.position.x < enemy.attackPoint) {
            enemy.state = new StayState(this.enemy);
        }
    }
}
