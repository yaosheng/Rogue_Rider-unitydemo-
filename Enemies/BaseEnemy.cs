using UnityEngine;
using System.Collections;

public abstract class BaseEnemy : MonoBehaviour
{
    public Animator anim;

    public Vector3 flyVector;
    public Vector3 originalPosition;
    public Vector3 tempVector;
    public float maxHealth;
    public float health;
    public float atk;
    public float walkSpeed;
    public float sprintSpeed;
    public float attackPoint;
    public float selectPoint;

    public Wave wave;
    public bool isDead = false;
    public IState state;
    public AnimatorStateInfo currentState;
    public GameProcess gp;
    public Rigidbody2D rb;

    private bool isfightbackTime = false;
    protected WaveManager wm;
    protected SpriteRenderer sr;

    void Awake( )
    {
        anim = GetComponentInChildren<Animator>( );
        gp = FindObjectOfType(typeof(GameProcess)) as GameProcess;
        rb = GetComponent<Rigidbody2D>( );
        wm = FindObjectOfType(typeof(WaveManager)) as WaveManager;
        sr = GetComponentInChildren<SpriteRenderer>( );
    }

    void Start( )
    {
        initialStateMethod( );
        transform.position = originalPosition;
    }

    void OnEnable( )
    {
        initialStateMethod( );
        transform.position = originalPosition;
        health = maxHealth;
    }

    void Update( )
    {
        state.StateUpdate( );
        //state = state.StateUpdate( );
        currentState = anim.GetCurrentAnimatorStateInfo(0);
        if(transform.position.x < -11 || health <= 0) {
            Destory( );
        }
    }

    public void Walk( float multiple )
    {
        Vector3 velocity = (Vector3.left * gp.mainTime) - (gp.AddAcceleration(gp.acceleration1) * Time.deltaTime);
        //transform.position += velocity * Time.deltaTime;
        velocity = multiple * velocity;
        rb.MovePosition(rb.transform.position + velocity * Time.deltaTime);
        //transform.position = new Vector3(transform.position.x, originalPosition.y, 0);

        rb.constraints = RigidbodyConstraints2D.FreezePositionY;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    public void KnockFly( )
    {
        Vector3 acceleration = Vector3.down * 15;
        tempVector += acceleration * Time.deltaTime;
        transform.position += tempVector * Time.deltaTime;
        //rd.MovePosition(rd.transform.position + flyVector * Time.deltaTime);
    }

    public bool IsReturnToGround( )
    {
        bool tempBool = false;
        if(transform.position.y < originalPosition.y) {
            transform.position = new Vector3(transform.position.x, originalPosition.y, 0);
            tempBool = true;
        }
        return tempBool;
    }

    public bool IsFightBackTime( )
    {
        if(isfightbackTime) {
            return true;
        }
        else {
            return false;
        }
    }

    public void GetFightBackEvent(bool tempBool)
    {
        Debug.Log("fight back :" + tempBool);
        isfightbackTime = tempBool;
    }

    public abstract void Move(float multiple);
    public abstract void Destory( );
    public abstract void initialStateMethod( );
    public abstract void GetHurt( float atk );
    public abstract void Attack( );
}
