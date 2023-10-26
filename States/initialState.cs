using UnityEngine;
using System.Collections;

public class InitialState : BaseState
{
    public InitialState( BaseEnemy enemy ) : base(enemy)
    {

    }

    public override void StateUpdate( )
    {
        enemy.gameObject.SetActive(true);
        Vector3 velocity = (Vector3.left * enemy.gp.mainTime) - (enemy.gp.AddAcceleration(enemy.gp.acceleration1) * Time.deltaTime);
        //transform.position += velocity * Time.deltaTime;
        enemy.rb.MovePosition(enemy.rb.transform.position + velocity * Time.deltaTime);
        //enemy.transform.position = new Vector3(enemy.transform.position.x, enemy.originalPosition.y, 0);

        enemy.rb.constraints = RigidbodyConstraints2D.FreezePositionY;
        enemy.rb.constraints = RigidbodyConstraints2D.FreezeRotation;

        if(enemy.transform.position.x < enemy.selectPoint) {
            enemy.state = new Select1State(this.enemy);
        }
    }
}
