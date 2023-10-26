using UnityEngine;
using System.Collections;

public class ShootState : BaseState
{
    private float timer;

    public ShootState( BaseEnemy enemy ) : base(enemy)
    {
        timer = Time.time;
    }

    public override void StateUpdate( )
    {
        if(Time.time - timer >= 0.5f) {
            
        }
    }
}
