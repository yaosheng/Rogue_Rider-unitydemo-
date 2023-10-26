using UnityEngine;
using System.Collections;

public class Enemy1 : BaseEnemy
{
    public override void initialStateMethod( )
    {
        state = new InitialState(this);
    }

    public override void Destory( )
    {
        wm.enemies.Enqueue(this);
        isDead = true;

        GetComponent<Collider2D>( ).enabled = false;
        sr.enabled = false;
        gameObject.SetActive(false);
    }

    public override void GetHurt( float atk )
    {
        health -= atk;
        if(health <= 0) {
            Destory( );
        }
        tempVector = flyVector;
        state = new KnockBackState(this);
    }

    public override void Attack( )
    {
        Debug.Log("override attack");
        anim.SetBool("Attack", true);
    }

    public override void Move( float multiple )
    {
        Walk(multiple);
        if(state.GetType( ) == typeof(WalkState)) {
            if(transform.position.x <= attackPoint) {
                Debug.Log("change to staystate");
                transform.position = new Vector3(attackPoint, transform.position.y, 0);
                state = new StayState(this);
            }
        }
    }
}

//public Animator anim;
//public Vector3 flyVector;
//public Vector3 originalPosition;
//public Vector3 tempVector;

//public float health;
//public float atk;
//public float walkSpeed;
//public float sprintSpeed;
//public float attackPoint;
//public float selectPoint;

//public Wave wave;
//public bool isDecidedAttack = false;
//public IState state;
//public AnimatorStateInfo currentState;
//public GameProcess gp;
//public Rigidbody2D rb;

//void Awake( )
//{
//    gp = FindObjectOfType(typeof(GameProcess)) as GameProcess;
//    rb = GetComponent<Rigidbody2D>( );
//}

//void Start( )
//{
//    state = new initialState(this);
//    transform.position = originalPosition;
//}

//void OnEnable( )
//{
//    state = new initialState(this);
//    transform.position = originalPosition;
//    health = 50;
//}

//void Update( )
//{
//    state = state.StateUpdate( );
//    currentState = anim.GetCurrentAnimatorStateInfo(0);
//    //StateArrange( );
//    if(transform.position.x < -11 || health <= 0) {
//        Destory( );
//    }
//}

//void FixedUpdate( )
//{ 

//}

//void StateArrange( )
//{
//    if(currentState.IsName("Base Layer.Walk")) {
//    }
//    else if(currentState.IsName("Base Layer.knockfly")) {
//    }
//    else if(currentState.IsName("Base Layer.Attack")) {
//        //anim.SetBool("Attack", false);
//    }
//}

//public void Walk( float multiple )
//{
//    Vector3 velocity = (Vector3.left * gp.mainTime) - (gp.AddAcceleration(gp.acceleration1) * Time.deltaTime);
//    //transform.position += velocity * Time.deltaTime;
//    velocity = multiple * velocity;
//    rb.MovePosition(rb.transform.position + velocity * Time.deltaTime);
//    transform.position = new Vector3(transform.position.x, originalPosition.y, 0);

//    rb.constraints = RigidbodyConstraints2D.FreezePositionY;
//    rb.constraints = RigidbodyConstraints2D.FreezeRotation;
//}

//public void KnockFly( )
//{
//    Debug.Log("fly");
//    Vector3 acceleration = Vector3.down * 15;
//    tempVector += acceleration * Time.deltaTime;
//    transform.position += tempVector * Time.deltaTime;
//    //rd.MovePosition(rd.transform.position + flyVector * Time.deltaTime);
//}

//public bool IsReturnToGround( )
//{
//    bool tempBool = false;
//    if(transform.position.y < originalPosition.y) {
//        transform.position = new Vector3(transform.position.x, originalPosition.y, 0);
//        tempBool = true;
//    }
//    return tempBool;
//}

//void DoBehavior( )
//{
//    //if(transform.position.x >= 7.5f) {
//    //    behavior = Behavior.Walk;
//    //}
//    //else if(transform.position.x < 7.5f) {
//    //    if(!isDecidedAttack) {
//    //        int random = Random.Range(0, 10);
//    //        if(random <= 4) {
//    //            behavior = Behavior.Walk;
//    //        }
//    //        else {
//    //            behavior = Behavior.Sprint;
//    //        }
//    //        isDecidedAttack = true;
//    //    }
//    //}
//    //else if(transform.position.x < -3) {
//    //    if(behavior == Behavior.Walk) {
//    //        transform.position = new Vector3(-3, transform.position.y, 0);
//    //        behavior = Behavior.Stay;
//    //    }
//    //}
//}

//void OnCollisionEnter2D( Collision2D coll )
//{
//    if(coll.gameObject.tag == "Hero") {
//        anim.SetBool("Knockfly", true);
//        tempVector = flyVector;
//    }
//}

//public IEnumerator KnockFly( )
//{
//    Debug.Log("fly 1");
//    float timer = 0;
//    while(timer < 2) {
//        timer += Time.deltaTime;
//        Vector3 acceleration = Vector3.down * 15;
//        flyVector += acceleration * Time.deltaTime;
//        transform.position += flyVector * Time.deltaTime;
//        //rd.MovePosition(rd.transform.position + flyVector * Time.deltaTime);
//        //if(transform.position.y < originalPosition.y) {
//        //    transform.position = new Vector3(transform.position.x, originalPosition.y, 0);
//        //    isWalk = true;
//        //}
//        //else {
//        //    continue;
//        //}
//        yield return null;
//    }
//    //yield return new WaitForSeconds(3.0f);
//}
