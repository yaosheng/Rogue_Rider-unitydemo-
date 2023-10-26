using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Wave : WaveBase
{
    public override void SetEnemies( )
    {
        for(int i = 0; i < number; i++) {
            BaseEnemy be = wm.GetEnemy( );
            be.gameObject.SetActive(false);
            be.transform.SetParent(this.transform);
            enemyList.Add(be);
        }
        enemyArray = enemyList.ToArray( );
    }
}

