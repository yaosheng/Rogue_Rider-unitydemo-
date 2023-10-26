using UnityEngine;
using System.Collections;

public class SprintAndBackState : BaseState
{
    public SprintAndBackState( BaseEnemy enemy ) : base(enemy)
    {

    }

    public override void StateUpdate( )
    {
        enemy.Move(enemy.walkSpeed);
    }
}
