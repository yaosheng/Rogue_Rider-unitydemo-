using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class WaveBase : MonoBehaviour
{
    public List<BaseEnemy> enemyList = new List<BaseEnemy>( );
    public int number;
    public float timer = 0;

    protected GameProcess gp;
    protected WaveManager wm;
    protected BaseEnemy[ ] enemyArray;
    protected int orderNumber = 0;


    void Awake( )
    {
        gp = FindObjectOfType(typeof(GameProcess)) as GameProcess;
        wm = FindObjectOfType(typeof(WaveManager)) as WaveManager;
        SetEnemies( );
    }

    void Start( )
    {

    }

    void Update( )
    {
        timer += Time.deltaTime * gp.vSpeed;
        Appearance( );
    }

    public bool IsFinished( )
    {
        bool tempBool = false;
        int tempInt = 0;
        foreach(BaseEnemy be in enemyList) {
            if(!be.isDead) {
                tempInt++;
            }
        }
        if(tempInt == 0) {
            tempBool = true;
        }
        else {
            tempBool = false;
        }
        return tempBool;
    }

    void Appearance( )
    {
        if(timer >= 1.0f && orderNumber <= enemyArray.Length - 1) {
            enemyArray[orderNumber].gameObject.SetActive(true);
            orderNumber++;
            timer = 0;
        }
    }

    public abstract void SetEnemies( );
}
