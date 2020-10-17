using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float movementSpeed = 1f;

    Rigidbody2D myRigidBody;
    CircleCollider2D myBodyCollider;

    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        myBodyCollider = GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        TurnAroundAfterColliding();
    }

    private void Movement()
    {
        myRigidBody.velocity = new Vector2(movementSpeed, myRigidBody.velocity.y);
        if (myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Player")))
        {
            movementSpeed *= -1;
            transform.localScale = new Vector2(Mathf.Sign(movementSpeed), 1f);
        }
    }

    private void TurnAroundAfterColliding()
    {
        if (myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Player")))
        {
            myRigidBody.velocity = new Vector2(-movementSpeed, myRigidBody.velocity.y);
        }
    }
}
