using UnityEngine;
using System.Collections;

public class Smoke : MonoBehaviour {

    private HeroController hero
    {
        get {
            return FindObjectOfType(typeof(HeroController)) as HeroController;
        }
    }
    private ParticlePool particlePool
    {
        get {
            return FindObjectOfType(typeof(ParticlePool)) as ParticlePool;
        }
    }
    private ParticleSystem thisParticle;

    void Awake( )
    {
        thisParticle = GetComponent<ParticleSystem>( );
    }

    void Update () {
        transform.position = hero.noumenon.transform.position;
        if(thisParticle.isStopped) {
            Recover( );
        }
	}

    void Recover( )
    {
        gameObject.SetActive(false);
        particlePool.smokePool.Enqueue(thisParticle);
    }
}
