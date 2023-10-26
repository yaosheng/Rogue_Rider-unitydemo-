using UnityEngine;
using System.Collections;

public class StayState : BaseState
{
    private float timer;

    public StayState( BaseEnemy enemy ) : base(enemy)
    {
        timer = Time.time;
    }

    public override void StateUpdate( )
    {
        if(Time.time - timer >= 1.0f) {
            Debug.Log("attack");
            enemy.anim.SetBool("Attack", true);
            enemy.state = new AttackState(this.enemy);
        }
    }
}