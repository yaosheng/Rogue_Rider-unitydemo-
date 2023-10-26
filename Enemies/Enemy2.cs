using UnityEngine;
using System.Collections;

public class Enemy2 : BaseEnemy
{
    public ParticleSystem shootingParticle;
    public Collider2D lineCollider;
    public float currentPoint;
    public float nextPoint;
    private int tempInt;
    private bool isBack = false;

    public override void initialStateMethod( )
    {
        state = new InitialStateBoss(this);
    }

    public override void Destory( )
    {
        //wave.enemies.Enqueue(this);
        wm.enemies2.Enqueue(this);
        isDead = true;

        GetComponent<Collider2D>( ).enabled = false;
        sr.enabled = false;
        gameObject.SetActive(false);
    }

    public override void GetHurt( float atk )
    {
        health -= atk;
        tempVector = flyVector;
        state = new KnockBackBossState(this);
        isBack = true;
    }

    public override void Attack( )
    {
        anim.SetBool("Attack", true);
        StartCoroutine(Shooting( ));
    }

    public override void Move( float multiple )
    {
        if(state.GetType() == typeof(MoveState)) {
            MoveUpAndDown( );
        }
        else if (state.GetType( ) == typeof(SprintAndBackState)) {
            if(!isBack) {
                Walk(sprintSpeed);
                if(Mathf.Abs(transform.position.x - attackPoint) < 0.2f) {
                    isBack = true;
                }
            }
            else {
                WalkBack(multiple);
            }
        }
    }

    void MoveUpAndDown( )
    {
        if(nextPoint >= transform.position.y) {
            transform.position += Vector3.up * 2.5f * Time.deltaTime;
        }
        else {
            transform.position += Vector3.down * 2.5f * Time.deltaTime;
        }

        if(Mathf.Abs(transform.position.y - nextPoint) < 0.2f) {
            transform.position = new Vector3(transform.position.x, nextPoint, 0);
            float tempFloat = nextPoint;
            nextPoint = currentPoint;
            currentPoint = tempFloat;
            state = new AttackState(this);
            tempInt++;
        }
        if(tempInt == 2) {
            state = new SprintAndBackState(this);
            tempInt = 0;
        }
    }

    void WalkBack(float multiple )
    {
        Vector3 velocity = (Vector3.right * gp.mainTime) + (gp.AddAcceleration(gp.acceleration1) * Time.deltaTime);
        velocity = multiple * velocity;
        rb.MovePosition(rb.transform.position + velocity * Time.deltaTime);
        //transform.position = new Vector3(transform.position.x, originalPosition.y, 0);

        rb.constraints = RigidbodyConstraints2D.FreezePositionY;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;

        if(Mathf.Abs(transform.position.x - selectPoint) < 0.2f) {
            isBack = false;
            state = new AttackState(this);
        }
    }

    IEnumerator Shooting( )
    {
        Debug.Log("shooting");
        shootingParticle.gameObject.SetActive(true);
        lineCollider.enabled = true;
        yield return new WaitForSeconds(0.5f);
        shootingParticle.gameObject.SetActive(false);
        lineCollider.enabled = false;
    }

    //public void Shooting( )
    //{
    //    shootingParticle.gameObject.SetActive(true);
    //}

    //public void DontShooting( )
    //{
    //    shootingParticle.gameObject.SetActive(false);
    //}
}
