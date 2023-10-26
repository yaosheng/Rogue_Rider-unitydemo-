using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WaveManager : MonoBehaviour {

    public List<WaveBase> waves = new List<WaveBase>();
    public BaseEnemy enemy;
    public BaseEnemy enemy2;
    public Queue<BaseEnemy> enemies = new Queue<BaseEnemy>( );
    public Queue<BaseEnemy> enemies2 = new Queue<BaseEnemy>( );

    private WaveBase[ ] waveArray;
    private int order = 0;
    private UIManager uiManager;
    private Step step;

    void Awake( )
    {
        uiManager = FindObjectOfType(typeof(UIManager)) as UIManager;
        step = FindObjectOfType(typeof(Step)) as Step;
        foreach(Transform t in this.transform) {
            WaveBase w = t.GetComponent<WaveBase>( );
            waves.Add(w);
        }
        waveArray = waves.ToArray( );
    }

    void Start( )
    {
        waveArray[0].gameObject.SetActive(true);
        for(int i = 1; i < waveArray.Length; i++) {
            waveArray[i].gameObject.SetActive(false);
        }
    }

    void Update( )
    {
        if(waveArray[order].IsFinished( ) && order < waveArray.Length - 1) {
            order++;
            waveArray[order].gameObject.SetActive(true);
        }
        step.ShowStep(order);
        CheckGamePass( );
    }

    void CheckGamePass( )
    {
        int temp = 0;
        for(int i = 0; i < waveArray.Length; i++) {
            if(waveArray[i].IsFinished( )) {
                temp++;
            }
        }
        if(temp == waveArray.Length) {
            uiManager.OpenGamePassUI( );
        }
    }

    public BaseEnemy GetEnemy( )
    {
        BaseEnemy be = null;
        if(enemies.Count > 0) {
            be = enemies.Dequeue( );
        }
        else {
            be = Instantiate(enemy) as BaseEnemy;
        }
        return be;
    }

    public BaseEnemy GetBossEnemy( )
    {
        BaseEnemy be = null;
        if(enemies2.Count > 0) {
            be = enemies2.Dequeue( );
        }
        else {
            be = Instantiate(enemy2) as BaseEnemy;
        }
        return be;
    }

}
