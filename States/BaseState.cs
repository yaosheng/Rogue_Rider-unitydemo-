using UnityEngine;
using System.Collections;

public abstract class BaseState : IState
{
    //public Vector3 currentPosition;
    public BaseEnemy enemy;

    public BaseState( BaseEnemy enemy )
    {
        this.enemy = enemy;
    }

    public abstract void StateUpdate( );
}
