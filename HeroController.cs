using UnityEngine;
using System.Collections;

public class HeroController : MonoBehaviour
{
    public Animator anim;
    public Vector3 initjumpVelocity;
    public Vector3 originalPosition;
    public Vector3 deadPosition;
    public Vector3 boostVelocity;
    public ParticleSystem slashParticle;
    public ParticleSystem drop;
    public ParticleSystem gathering;
    public Transform noumenon;
    public Collider2D attackCollider;
    public Collider2D dropCollider;
    public Collider2D boostCollider;
    public bool isCollided = false;
    public float health;
    public float atk;

    private AnimatorStateInfo currentState;
    private Vector3 jumpVelocity;
    private Vector3 runVelocity;
    private Vector3 tempVector;
    private int jumpCount = 0;
    private bool isJump = false;
    private bool isSmoke = false;
    private float timer1 = 0;
    private GameProcess gp
    {
        get {
            return FindObjectOfType(typeof(GameProcess)) as GameProcess;
        }
    }
    public ParticlePool particlePool
    {
        get {
            return FindObjectOfType(typeof(ParticlePool)) as ParticlePool;
        }
    }

    private UIManager uiManager;
    private Rigidbody2D rd;
    private SpriteRenderer heroRender;

    private Color32 originalColor = new Color32(255, 255, 255, 255);
    private Color32 hurtColor = new Color32(255, 0, 0, 255);

    void Awake( )
    {
        originalPosition = this.transform.position;
        heroRender = GetComponentInChildren<SpriteRenderer>( );
        rd = GetComponent<Rigidbody2D>( );
        uiManager = FindObjectOfType(typeof(UIManager)) as UIManager;
    }

    void Start( )
    {
        Time.timeScale = 1;
    }

    void Update( )
    {
        currentState = anim.GetCurrentAnimatorStateInfo(0);
        StateArrange( );
        NotStateArrange( );
        AnimationSpeedHandler( );

        Jump( );
        ReturnToRunState( );
        CheckDead( );
    }

    void FixedUpdate( )
    {
        CheckCollideOrNot( );
    }

    void Jump( )
    {
        if(isJump) {
            timer1 += Time.deltaTime;
            AddVelocity(100);
            if(jumpVelocity.y >= -1.0f && jumpVelocity.y <= 1.0f) {
                anim.SetFloat("JumpHeight", 1);
            }
            else if(jumpVelocity.y > 1.0f) {
                anim.SetFloat("JumpHeight", 0);
            }
            else {
                anim.SetFloat("JumpHeight", 2);
            }
        }
        else {
            timer1 = 0;
        }
    }

    void AnimationSpeedHandler( )
    {
        if(gp.vSpeed < 1) {
            //Debug.Log(" < 1");
            anim.speed = 0.5f + 0.5f * (gp.vSpeed);
            rd.mass = 100;
            if(!isCollided) {
                gp.vSpeed += 0.0015f;
            }
        }
        else {
            if(Mathf.Abs(gp.vSpeed - 1) < 0.01f) {
                gp.vSpeed = 1;
            }
            anim.speed = gp.vSpeed;
            rd.mass = 10000000;
            //transform.position = originalPosition;
        }
    }
    
    public void CheckDead( )
    {
        if(transform.position.x <= deadPosition.x || health <= 0) {
            uiManager.OpenGameOverUI( );
        }
    }

    public void AnimationController( int value )
    {
        switch(value) {
            case 0:
            if(currentState.IsName("Base Layer.Run")) {
                gp.SpeedUp( );
                anim.SetBool("Boost", true);
            }
            break;
            //case 1:
            //if(currentState.IsName("Base Layer.Run")) {
            //    anim.SetBool("Dodge", true);
            //    particlePool.GetSmoke( );
            //}
            //break;
            case 2:
            if(!currentState.IsName("Base Layer.Boost") && !currentState.IsName("Base Layer.Dodge")) {
                if(jumpCount <= 1) {
                    anim.SetBool("Jump", true);
                    isJump = true;
                    jumpCount++;
                    jumpVelocity = initjumpVelocity;
                }
            }
            break;
            case 3:
            if(currentState.IsName("Base Layer.JumpMotion") || currentState.IsName("Base Layer.Run")) {
                anim.SetBool("Attack", true);
                StartCoroutine(Slash( ));
            }
            break;
            case 4:
            if(currentState.IsName("Base Layer.JumpMotion") || currentState.IsName("Base Layer.Run")) {
                anim.SetBool("Attack", true);
                particlePool.GetGather(transform);
            }
            break;
            case 5:
            if(currentState.IsName("Base Layer.JumpMotion")) {
                anim.SetBool("DropAttack", true);
            }
            break;
        }
    }

    void StateArrange( )
    {
        if(currentState.IsName("Base Layer.Run")) {
            anim.SetBool("OnTheGround", false);
            GoBackToTheOriginalPosition( );
            KeepTransform( );
            //SlowDown( );
        }
        else if(currentState.IsName("Base Layer.JumpMotion")) {
            ReturnToRunState( );
        }
        else if(currentState.IsName("Base Layer.Dodge")) {
            anim.SetBool("Dodge", false);
            gp.SpeedDown( );
            gp.SpeedZero( );
        }
        else if(currentState.IsName("Base Layer.Attack")) {
            anim.SetBool("Attack", false);
        }
        else if(currentState.IsName("Base Layer.Boost")) {
            boostCollider.gameObject.SetActive(true);
            Boost( );
            KeepTransform( );
        }
        else if(currentState.IsName("Base Layer.DropAttack")) {
            dropCollider.gameObject.SetActive(true);
            anim.SetBool("DropAttack", false);
            ShowDropParticle( );
            AddVelocity(200);
            ReturnToRunState( );
        }
    }

    void KeepTransform( )
    {
        rd.constraints = RigidbodyConstraints2D.FreezePositionY;
        rd.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    void Boost( )
    {
        if(anim.GetBool("Boost")) {
            Vector3 acceleration = Vector3.left * 5f;
            boostVelocity += acceleration * Time.deltaTime;
            transform.position += boostVelocity * Time.deltaTime;
            //rd.MovePosition(rd.transform.position + boostVelocity * Time.deltaTime);
        }
        if(transform.position.x - originalPosition.x < 0 && boostVelocity.x < 0) {
            Debug.Log("fixed");
            anim.SetBool("Boost", false);
            transform.position = originalPosition;
            boostVelocity = Vector3.right * 8;
        }
    }

    void GoBackToTheOriginalPosition( )
    {
        if(transform.position.x < originalPosition.x) {
            Vector3 acceleration = Vector3.right * 0.2f;
            runVelocity += acceleration * Time.deltaTime;
            //rd.MovePosition(rd.transform.position + runVelocity * Time.deltaTime);
            transform.position += runVelocity * Time.deltaTime;
        }
        else {
            runVelocity = Vector3.zero;
        }
    }

    void NotStateArrange( )
    {
        if(!currentState.IsName("Base Layer.Run")) {

        }
        if(!currentState.IsName("Base Layer.JumpMotion")) {

        }
        if(!currentState.IsName("Base Layer.Dodge")) {
            isSmoke = false;
        }
        if(!currentState.IsName("Base Layer.Attack")) {

        }
        if(!currentState.IsName("Base Layer.Boost")) {
            boostCollider.gameObject.SetActive(false);
            gp.ContinueToReduce( );
        }
        if(!currentState.IsName("Base Layer.DropAttack")) {
            dropCollider.gameObject.SetActive(false);
            HideDropParticle( );
        }
    }

    void AddVelocity( float multiple )
    {
        Vector3 acceleration = Vector3.down * multiple;
        jumpVelocity += acceleration * Time.deltaTime;
        transform.position += jumpVelocity * Time.deltaTime;
        //rd.MovePosition(rd.transform.position + jumpVelocity * Time.deltaTime);
    }

    void ReturnToRunState( )
    {
        if(transform.position.y <= originalPosition.y && anim.GetFloat("JumpHeight") == 2) {
            isJump = false;
            anim.SetBool("OnTheGround", true);
            anim.SetBool("Jump", false);
            jumpCount = 0;
            transform.position = new Vector3(transform.position.x, originalPosition.y, 0);
            HideDropParticle( );
        }
    }

    IEnumerator Slash( )
    {
        attackCollider.gameObject.SetActive(true);
        slashParticle.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.05f);
        attackCollider.gameObject.SetActive(false);
        slashParticle.gameObject.SetActive(false);
    }

    void ShowDropParticle( )
    {
        drop.gameObject.SetActive(true);
    }

    void HideDropParticle( )
    {
        drop.gameObject.SetActive(false);
    }

    public void Gathering( )
    {
        gathering.gameObject.SetActive(true);
    }

    public void NotGathering( )
    {
        gathering.gameObject.SetActive(false);
    }

    void CheckCollideOrNot( )
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right, 1.0f);

        if(hit.collider != null && hit.collider.tag == "Enemy" && gp.vSpeed <= 1) {
            isCollided = true;
            gp.vSpeed -= 0.0015f;
        }
        else {
            isCollided = false;
        }
    }

    public IEnumerator GetHurt(float hurt)
    {
        if(heroRender.color != hurtColor) {
            heroRender.color = hurtColor;
        }
        health -= hurt;
        yield return new WaitForSeconds(0.1f);
        if(heroRender.color != originalColor) {
            heroRender.color = originalColor;
        }
    }

    public IEnumerator GetHurtEffect( )
    {
        yield return new WaitForSeconds(0.1f);
    }
}

//void Update( )
//{
//    currentState = anim.GetCurrentAnimatorStateInfo(0);
//    StateArrange( );
//    Jump( );
//    ReturnToRunState( );
//    NotStateArrange( );
//    HeroAnimationHandler( );
//    CheckDead( );
//}

//void SlowDown( )
//{
//    //float position = (transform.position.x - deadPosition.x) / (originalPosition.x - deadPosition.x);
//    //if(position < 1) {
//    //    gp.vSpeed *= position;
//    //}
//}

//void OnCollisionStay2D( Collision2D coll )
//{
//    if(coll.collider.tag == "Enemy" && gp.vSpeed <= 1) {
//        Debug.Log("isCollided");
//        isCollided = true;
//        gp.vSpeed -= 0.0015f;
//    }
//}

//void OnCollisionExit2D( Collision2D coll ) { }
//    {
//        isCollided = false;
//    }
