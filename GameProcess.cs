using UnityEngine;
using System.Collections;

public class GameProcess : MonoBehaviour
{
    public float mainTime;
    public float subTime;
    public float acceleration1;
    public float acceleration2;
    public SpriteRenderer[ ] front1;
    public SpriteRenderer[ ] front2;
    public float vSpeed;

    void Start( )
    {

    }

    void Update( )
    {
        //ContinueToReduce( );
        for(int i = 0; i < front1.Length; i++) {
            //Vector3 velocity = (Vector3.left * mainTime) - (AddAcceleration(acceleration1) * Time.deltaTime);
            Vector3 velocity = vSpeed * (Vector3.left * mainTime);
            front1[i].transform.localPosition += velocity * Time.deltaTime;
            if(front1[i].transform.localPosition.x <= -19.2f) {
                front1[i].transform.localPosition = new Vector3(19.2f, front1[i].transform.localPosition.y, 0);
            }
            //float temp = SpeedRate(velocity, Vector3.left * mainTime);
            //Debug.Log("temp :" + temp);
            //Debug.Log("velocity1 :" + velocity);
        }

        for(int i = 0; i < front2.Length; i++) {
            //Vector3 velocity = (Vector3.left * subTime) - (AddAcceleration(acceleration2) * Time.deltaTime);
            Vector3 velocity = vSpeed * (Vector3.left * subTime);
            front2[i].transform.localPosition += velocity * Time.deltaTime;
            if(front2[i].transform.localPosition.x <= -14f) {
                front2[i].transform.localPosition = new Vector3(14f, front2[i].transform.localPosition.y, 0);
            }
            //Debug.Log("velocity2 :" + velocity);
            //float temp = SpeedRate(velocity, Vector3.left * subTime);
            //Debug.Log("temp :" + temp);
            //Debug.Log("velocity1 :" + velocity);
        }
    }

    public float SpeedRate(Vector3 currentVector, Vector3 defaultVector)
    {
        float tempFloat = 0;
        tempFloat = Mathf.Abs(currentVector.magnitude / defaultVector.magnitude);
        return tempFloat;
    }

    public Vector3 AddAcceleration( float a )
    {
        return Vector3.right * a;
    }

    public void SpeedUp( )
    {
        //Debug.Log("Speed up");
        //acceleration1 = 500;
        //acceleration2 = 150;
        if(vSpeed + 0.5f >= 3) {
            vSpeed = 3;
        }
        else {
            vSpeed += 0.5f;
        }
    }

    //public void ReturnSpeed(float speed)
    //{
    //    //acceleration1 = 0;
    //    //acceleration2 = 0;
    //    vSpeed = speed;
    //}

    public void SpeedDown( )
    {
        //acceleration1 = -250;
        //acceleration2 = -75;
        vSpeed = 0.5f;
    }

    public void SpeedZero( )
    {
        acceleration1 = -725;
        acceleration2 = -210;
    }

    public void ContinueToReduce( )
    {
        if(vSpeed > 1) {
            vSpeed -= Time.deltaTime * 0.1f;//0.05f
        }
    }
}
