using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControl : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private Collider2D coll;
    [SerializeField] private AudioSource footstep;
    [SerializeField] private AudioSource coincollect;
    [SerializeField] private AudioSource hurt;
    [SerializeField] public int coins = 0;
    [SerializeField] private LayerMask ground;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private float hurtforce = 5f;
    [SerializeField] private Text coinsScore;

    private enum State { idle, run, jump, fall,hurt};
    private State state = State.idle;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        coll = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (state != State.hurt)
        {    float hDirection = Input.GetAxis("Horizontal");
            if (hDirection < 0)
            {
                rb.velocity = new Vector2(-speed, rb.velocity.y);
                gameObject.GetComponent<SpriteRenderer>().flipX = true;
            }
            else if (hDirection > 0)
            {
               rb.velocity = new Vector2(speed, rb.velocity.y);
               gameObject.GetComponent<SpriteRenderer>().flipX = false;

            }
        

            if (Input.GetButtonDown("Jump")&& coll.IsTouchingLayers(ground))
            {
                Jump();
            }

        }
      
        Speedswitch();
        anim.SetInteger("state", (int)state);

    }
    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                state = State.jump;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Collectable")
        {
            coincollect.Play();
            Destroy(collision.gameObject);
            coins += 1;
            coinsScore.text = coins.ToString();
        }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "enemy")
        {
            Enemy enemy = other.gameObject.GetComponent<Enemy>();
            if (state == State.fall)
            {   
                enemy.JumpedOn();
                Jump();
            }
            else
            {
                state = State.hurt;
                if (other.gameObject.transform.position.x > transform.position.x)
                {
                    rb.velocity = new Vector2(-hurtforce, rb.velocity.y);
                }
                else
                {
                    rb.velocity = new Vector2(hurtforce, rb.velocity.y);
                }
            }
        }
    }

    private void Speedswitch()
    {
        if (state == State.jump)
        {
            if (rb.velocity.y < 0.5f )
            {
                state = State.fall;
            }
        }
        else if (state == State.fall)
        {
            if (coll.IsTouchingLayers(ground))
            {
                state = State.idle;
            }
        }
        else if (state == State.hurt)
        {
            if (Mathf.Abs(rb.velocity.x) < .1f)
            {
                state = State.idle;
            }
        }
        else if (Mathf.Abs(rb.velocity.x) > Mathf.Epsilon)
        {
            state = State.run;
        }
        
        else
        {
            state = State.idle;
        }
    }
    private void Footstep()
    {
        footstep.Play();
    }
    private void Hurt()
    {
        hurt.Play();
    }
}

