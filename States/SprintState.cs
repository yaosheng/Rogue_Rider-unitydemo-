using UnityEngine;
using System.Collections;

public class SprintState : BaseState
{

    public SprintState( BaseEnemy enemy ) : base(enemy)
    {

    }

    public override void StateUpdate( )
    {
        //enemy.Walk(enemy.sprintSpeed);
        enemy.Move(enemy.sprintSpeed);
        if(Mathf.Abs(enemy.transform.position.x - enemy.attackPoint) < 0.25f) {
            enemy.GetFightBackEvent(true);
        }
        else {
            enemy.GetFightBackEvent(false);
        }
    }
}
