using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Configuration parameters
    [SerializeField] float movementSpeed = 4f;
    [SerializeField] float jumpSpeed = 3f;
    [SerializeField] float climbSpeed = 5f;

    // State variables
    bool isAlive = true;

    // Cached component references
    Rigidbody2D myRigidBody;
    Animator myAnimator;
    CapsuleCollider2D myBodyCollider;
    BoxCollider2D myFeetCollider;
    float gravityScaleAtStart;

    // Start is called before the first frame update
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myBodyCollider = GetComponent<CapsuleCollider2D>();
        myFeetCollider = GetComponent<BoxCollider2D>();
        gravityScaleAtStart = myRigidBody.gravityScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isAlive) { return; }
        Run();
        FlipSprite();
        Jump();
        ClimbLadder();
        Die();
    }

    private void Run()
    {
        var deltaX = Input.GetAxis("Horizontal") * movementSpeed;
        Vector2 playerVelocity = new Vector2(deltaX, myRigidBody.velocity.y);
        myRigidBody.velocity = playerVelocity;

        bool hasHorizontalSpeed = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;
        myAnimator.SetBool("Running", hasHorizontalSpeed);
    }

    private void Die()
    {
        if (myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemy", "Water")))
        {
            isAlive = false;
            myAnimator.SetTrigger("Die");
            myRigidBody.velocity = new Vector2(0f, 8f);
            GetComponent<Collider2D>().enabled = false;
            DestroyAfterSec(3f);
        }
    }

    IEnumerator DestroyAfterSec(float timeSeconds)
    {
        yield return new WaitForSeconds(timeSeconds);
        Destroy(gameObject);
    }

    private void ClimbLadder()
    {
        if (!myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Climbing")))
        {
            myAnimator.SetBool("Climbing", false);
            myRigidBody.gravityScale = gravityScaleAtStart;
            return;
        }

        float deltaY = Input.GetAxis("Vertical") * climbSpeed;
        Vector2 climbVelocity = new Vector2(myRigidBody.velocity.x, deltaY);
        myRigidBody.velocity = climbVelocity;
        myRigidBody.gravityScale = 0;

        bool hasVerticalSpeed = Mathf.Abs(myRigidBody.velocity.y) > Mathf.Epsilon;
        myAnimator.SetBool("Climbing", hasVerticalSpeed);
    }

    private void Jump()
    {
        if (!myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground"))) { return; }

        if (Input.GetButtonDown("Jump"))
        {
            Vector2 jumpVelocityToAdd = new Vector2(0f, jumpSpeed);
            myRigidBody.velocity += jumpVelocityToAdd;
        }
    }

    private void FlipSprite()
    {
        bool hasHorizontalSpeed = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;
        if (hasHorizontalSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(myRigidBody.velocity.x), 1f);
        }
    }
}
