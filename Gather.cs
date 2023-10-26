using UnityEngine;
using System.Collections;

public class Gather : MonoBehaviour {

    private ParticlePool particlePool
    {
        get {
            return FindObjectOfType(typeof(ParticlePool)) as ParticlePool;
        }
    }
    private ParticleSystem thisParticle;
    private GatherCollider gatherCollider;

    public Collider2D gc;

    void Awake( )
    {
        thisParticle = GetComponent<ParticleSystem>( );
    }

    void OnEnable( )
    {
        Collider2D ga = Instantiate(gc) as Collider2D;
        gatherCollider = ga.GetComponent<GatherCollider>( );
    }

	void Update () {
        gatherCollider.Fallow(transform.position);
        if(transform.position.x < 13) {
            transform.Translate(Vector3.right * 25 * Time.deltaTime);
        }
        else {
            Recover( );
        }
	}

    void Recover( )
    {
        gameObject.SetActive(false);
        gatherCollider.gameObject.SetActive(false);
        particlePool.gatherPool.Enqueue(thisParticle);
    }

    public void SetPosition(Transform tran)
    {
        transform.position = tran.position + Vector3.right * 2 + Vector3.up;
    }
}
