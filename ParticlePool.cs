using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ParticlePool : MonoBehaviour {

    private Transform pool;

    public ParticleSystem smoke;
    public ParticleSystem gather;

    public Queue<ParticleSystem> smokePool = new Queue<ParticleSystem>( );
    public Queue<ParticleSystem> gatherPool = new Queue<ParticleSystem>( );

    void Awake( )
    {
        pool = this.transform;
    }

    public void GetSmoke( )
    {
        ParticleSystem ps;
        if(smokePool.Count > 0) {
            ps = smokePool.Dequeue( );
        }
        else {
            ps = Instantiate(smoke) as ParticleSystem;
        }

        ps.gameObject.SetActive(true);
        ps.transform.SetParent(pool);
    }

    public void GetGather(Transform trans)
    {
        ParticleSystem ps;
        if(gatherPool.Count > 0) {
            ps = gatherPool.Dequeue( );
        }
        else {
            ps = Instantiate(gather) as ParticleSystem;
        }
        ps.transform.SetParent(pool);
        ps.GetComponent<Gather>( ).SetPosition(trans);
        ps.gameObject.SetActive(true);
    }
}
