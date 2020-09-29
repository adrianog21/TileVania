using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{
    [SerializeField] float speed = 5f;
    [SerializeField] float jumpSpeed = 10f;
    [SerializeField] float ladderSpeed = 2f;
    [SerializeField] GameObject loseLabel;

    bool isAlive = true;

    Rigidbody2D myrigidbody;
    Animator myAnimator;
    CapsuleCollider2D myBodyCollider;
    BoxCollider2D myFeetCollider;
    float playerGravityatStart;

    // Start is called before the first frame update
    void Start()
    {
        loseLabel.SetActive(false);
        myrigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myBodyCollider = GetComponent<CapsuleCollider2D>();
        myFeetCollider = GetComponent<BoxCollider2D>();
        playerGravityatStart = myrigidbody.gravityScale;
    }


    // Update is called once per frame
    void Update()
    {
        if (!isAlive) { return; }
        Move();
        FlipSprite();
        Jump();
        Climb();
        Death();
    }

    private void Move()
    {
        float deltaX = CrossPlatformInputManager.GetAxis("Horizontal"); // ritorna il vaolre tra -1 e +1
        Vector2 playerVelocity = new Vector2(deltaX * speed, myrigidbody.velocity.y);
        myrigidbody.velocity = playerVelocity;
        if(myrigidbody.velocity.x  !=0)
        {
            myAnimator.SetBool("Running", true);
        }
        else
        {
            myAnimator.SetBool("Running", false);
        }
    }

    private void Jump()
    {
        if (!myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground"))) { return; }  // se  il giocatore non tocca terra return 
        if(Input.GetButtonDown("Jump") && myrigidbody.velocity.y == 0)
        {
            Vector2 jumpForce = new Vector2(0, jumpSpeed);
            myrigidbody.velocity += jumpForce;
        }
    }

    private void Climb()
    {
        if(!myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Ladder")))
        {
            myrigidbody.gravityScale = playerGravityatStart;
            myAnimator.SetBool("Climbing", false);
            return;
        }

        if (Input.GetAxis("Vertical") != 0)
        {
            myrigidbody.gravityScale = 0f;
            float deltaY  = CrossPlatformInputManager.GetAxis("Vertical");
            Vector2 ladder = new Vector2(myrigidbody.velocity.x,Mathf.Round(deltaY) * ladderSpeed);
            myrigidbody.velocity = ladder;
            if (myrigidbody.velocity.y != 0)
            {
                myAnimator.SetBool("Climbing", true);
            }
        }
    }
    
    private void FlipSprite()
    {
        //Mathf.Sign ritorna un valore di -1 se il valore è minore di 0, +1 se il valore è maggiore di 0
        // Mathf.Epsilon è un folat di un valore minimo diverso da 0
        bool playerHorizontalMove = Mathf.Abs(myrigidbody.velocity.x) > Mathf.Epsilon;
        if(playerHorizontalMove)
        {
            transform.localScale =new Vector2( Mathf.Sign(myrigidbody.velocity.x), 1f);
        }

    }
    
    private void Death()
    {
        if (myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemy", "Dangers")))
        {
            myrigidbody.velocity = new Vector2(-10f, 10f);
            myAnimator.SetTrigger("Death");
            isAlive = false;
            loseLabel.SetActive(true);
            FindObjectOfType<GameSession>().ProcessPlayer();
        }       
    }
}
