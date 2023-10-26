using UnityEngine;
using System.Collections;

public class AttackState : BaseState
{
    public AttackState( BaseEnemy enemy ) : base(enemy)
    {

    }

    public override void StateUpdate( )
    {
        enemy.Attack( );
    }
}
