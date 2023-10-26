using UnityEngine;
using System.Collections;

public class KnockBackState : BaseState
{
    public KnockBackState( BaseEnemy enemy ) : base(enemy)
    {

    }

    public override void StateUpdate( )
    {
        Debug.Log("state knockback");
        enemy.anim.SetBool("Knockfly", true);
        enemy.KnockFly( );
        if(enemy.IsReturnToGround( )) {
            enemy.anim.SetBool("Knockfly", false);
            enemy.state = new Select1State(this.enemy);
        }
    }
}
