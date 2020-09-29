using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 10f;

    Rigidbody2D myRigidbody;
    BoxCollider2D colliders ;
    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        colliders = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Move(); 
    }

    private void Move()
    {
        myRigidbody.velocity = new Vector2(moveSpeed, 0);

        bool turn = myRigidbody.velocity.x != 0;
        if (turn)
        {
            transform.localScale = new Vector2(Mathf.Sign(myRigidbody.velocity.x), 1f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (colliders.IsTouchingLayers(LayerMask.GetMask("Player"))) { return; }
        moveSpeed = moveSpeed * -1;
    }
}
