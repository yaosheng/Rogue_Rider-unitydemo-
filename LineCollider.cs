using UnityEngine;
using System.Collections;

public class LineCollider : MonoBehaviour {

    void OnTriggerEnter2D( Collider2D coll )
    {
        Debug.Log("hit");
        if(coll.gameObject.tag == "Hero") {
            Debug.Log("hit Hero");
            HeroController hero = coll.GetComponentInParent<HeroController>( );
            StartCoroutine(hero.GetHurt(30));
        }
    }
}
