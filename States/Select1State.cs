using UnityEngine;
using System.Collections;

public class Select1State : BaseState {

    private float timer;

    public Select1State( BaseEnemy enemy ) : base(enemy)
    {
        timer = Time.time;
    }

    public override void StateUpdate( )
    {
        Debug.Log("select state");
        //IState tempState = this;
        if(Time.time - timer >= 0.5f) {
            int random = Random.Range(0, 10);
            if(random < 4) {
                enemy.state = new WalkState(this.enemy);
            }
            else {
                enemy.state = new SprintState(this.enemy);
            }
        }
    }
}
