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
        Movement();
    }

    private void Movement()
    {
        if (IsFacingRight())
        {
            myRigidBody.velocity = new Vector2(movementSpeed, myRigidBody.velocity.y);
        }
        else
        {
            myRigidBody.velocity = new Vector2(-movementSpeed, myRigidBody.velocity.y);
        }
    }

    private bool IsFacingRight()
    {
        return transform.localScale.x > 0;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        transform.localScale = new Vector2(-(Mathf.Sign(myRigidBody.velocity.x)), 1f);
    }
}
