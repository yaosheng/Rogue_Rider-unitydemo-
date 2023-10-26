using UnityEngine;
using System.Collections;

public class GatherCollider : MonoBehaviour {

	void Update () {
	
	}

    public void Fallow(Vector3 vector )
    {
        transform.position = vector;
    }

    void OnTriggerEnter2D( Collider2D coll )
    {
        Debug.Log("hit");
        if(coll.gameObject.tag == "Enemy") {
            Debug.Log("hit enemy");
            coll.GetComponentInParent<BaseEnemy>( ).Destory( );
        }
    }
}
