using UnityEngine;
using System.Collections;

public class AttackCollider : MonoBehaviour {

    public HeroController hero;

    void OnEnable( )
    {
        hero = transform.root.GetComponent<HeroController>( );
    }

    void OnTriggerEnter2D( Collider2D coll )
    {
        Debug.Log("hit");
        if(coll.gameObject.tag == "Enemy") {
            BaseEnemy enemy = coll.GetComponent<BaseEnemy>( );
            if(enemy.IsFightBackTime( )) {
                Debug.Log("success to fightback");
                hero.particlePool.GetGather(hero.transform);
            }
            else {
                coll.GetComponent<BaseEnemy>( ).GetHurt(hero.atk);
            }
        }
    }
}
