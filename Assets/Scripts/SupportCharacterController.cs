using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SupportCharacterController : MonoBehaviour
{
    public float moveSpeed;
    public float jumpHeight;
    public float followDistance;
    public float resetDistance;
    public float accelerationFactor; //smaller values mean slower acceleration

    Rigidbody2D rigidBody;
    Rigidbody2D targetBody;
    //object to follow
    public GameObject targetGameObject;

    // bool isJumping = false;
    float distance;

    public GameObject sprite;
    bool facingRight = true;

    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        rigidBody = GetComponent<Rigidbody2D>();
        targetBody = targetGameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // characterPosition = gameObject.transform.position;
        // targetPosition = targetGameObject.transform.position;
        distance = Vector2.Distance(gameObject.transform.position, targetGameObject.transform.position);
        float distance_x = Mathf.Abs(targetGameObject.transform.position.x - gameObject.transform.position.x);
        Debug.Log("Distance x: " + distance_x);
        float playerSpeed = targetBody.velocity.x;
        

        // animator.SetFloat("speed", Mathf.Abs(playerSpeed));

        if(distance_x > followDistance)
        {
            animator.SetFloat("speed", 2f);
            int direction = 1;
            if( !facingRight)
            {
                direction = -1;
            }
            float speed = Mathf.Max(Mathf.Abs(playerSpeed), moveSpeed);
            // rigidBody.velocity = new Vector2(direction*speed, rigidBody.velocity.y);
            rigidBody.velocity = new Vector2(direction * moveSpeed, rigidBody.velocity.y);
        } else {
            animator.SetFloat("speed", 0);
        }
        SpriteDirection();
    }

    void SpriteDirection()
    {
        Vector2 orientation = sprite.transform.localEulerAngles;
        
        if(gameObject.transform.position.x < targetGameObject.transform.position.x)
        {
            orientation.y = 0;
            facingRight = true;
        } else {
            orientation.y = 180f;
            facingRight = false;
        }

        sprite.transform.eulerAngles = orientation;
    }
}
