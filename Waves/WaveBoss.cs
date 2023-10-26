using UnityEngine;
using System.Collections;

public class WaveBoss : WaveBase
{
    public override void SetEnemies( )
    {
        for(int i = 0; i < number; i++) {
            BaseEnemy be = wm.GetBossEnemy( );
            be.gameObject.SetActive(false);
            be.transform.SetParent(this.transform);
            enemyList.Add(be);
        }
        enemyArray = enemyList.ToArray( );
    }
}
