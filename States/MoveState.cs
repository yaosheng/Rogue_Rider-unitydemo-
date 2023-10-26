using UnityEngine;
using System.Collections;

public class MoveState : BaseState
{
    public MoveState( BaseEnemy enemy ) : base(enemy)
    {

    }

    public override void StateUpdate( )
    {
        enemy.Move(enemy.walkSpeed);
        //enemy.rb.transform.position += Vector3.up * 5 * Time.deltaTime;
        //    enemy.Move(1, 1);
        //    if(enemy.transform.position.y >= 1) {
        //        return new AttackState(this.enemy);
        //    }
        //    else {
        //        return this;
        //    }
        //if(enemy.nextPoint >= transform.position.y) {
        //    transform.position += Vector3.up * 5 * Time.deltaTime;
        //}
        //else {
        //    transform.position += Vector3.down * 5 * Time.deltaTime;
        //}

        //if(Mathf.Abs(transform.position.y - nextPoint) < 0.01) {
        //    transform.position = new Vector3(transform.position.x, nextPoint, 0);
        //    float tempFloat = nextPoint;
        //    nextPoint = currentPoint;
        //    currentPoint = tempFloat;
        //    return new AttackState(this.enemy);
        //}
        //else {
        //    return this;
        //}
        //return this;
    }
}
