using UnityEngine;
using System.Collections;

public class KnockBackBossState : BaseState
{
    public KnockBackBossState( BaseEnemy enemy ) : base(enemy)
    {

    }

    public override void StateUpdate( )
    {
        Debug.Log("state knockback");
        enemy.anim.SetBool("Knockfly", true);
        enemy.KnockFly( );
        if(enemy.IsReturnToGround( )) {
            enemy.anim.SetBool("Knockfly", false);
            enemy.state = new SprintAndBackState(this.enemy);
        }
    }
}
