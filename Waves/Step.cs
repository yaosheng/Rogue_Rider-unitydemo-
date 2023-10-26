using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Step : MonoBehaviour
{
    public List<Image> sprites = new List<Image>();

    void Start( )
    {
        foreach(Transform t in this.transform) {
            Image sr = t.GetComponent<Image>( );
            sprites.Add(sr);
        }
    }

    public void ShowStep( int temp )
    {
        Image[ ] srArray = sprites.ToArray( );
        for(int i = 0; i < srArray.Length; i++) {
            if(i == temp) {
                srArray[i].color = new Color32(255, 255, 255, 255);
            }
            else {
                srArray[i].color = new Color32(0, 0, 0, 255);
            }

        }
    }
}
