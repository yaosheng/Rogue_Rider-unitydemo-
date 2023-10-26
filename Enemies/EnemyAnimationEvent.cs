using UnityEngine;
using System.Collections;

public class EnemyAnimationEvent : MonoBehaviour {

    private HeroController hero;
    private BaseEnemy enemy;

    void Awake( )
    {
        hero = FindObjectOfType(typeof(HeroController)) as HeroController;
        enemy = GetComponentInParent<BaseEnemy>( );
    }

    void AttackToHero( )
    {
        float distance = Vector3.Distance(hero.transform.position, enemy.transform.position);
        //Debug.Log("distance :" + distance);
        if(distance <= 2.0f) {
            StartCoroutine(hero.GetHurt(enemy.atk));
        }
    }

    void StartToFightBack( )
    {
        enemy.GetFightBackEvent(true);
    }

    void EndToFightBack( )
    {
        enemy.GetFightBackEvent(false);
    }
}

//Vector2 vector2d = new Vector2(transform.position.x, transform.position.y);
//RaycastHit2D hit = Physics2D.Raycast(vector2d + Vector2.up, vector2d + Vector2.up + Vector2.left, 100f);

//Debug.Log("hit.distance :"+hit.distance);

//if(hit.collider != null && hit.collider.tag == "Hero") {
//    StartCoroutine(hero.GetHurt(enemy.atk));
//}