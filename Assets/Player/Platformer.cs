using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platformer : MonoBehaviour
{
    public float speed;
    public float jump_power;
    public float set_jump_timer;

    bool isGrounded = false;
    public Collider2D ground_collider;
    public LayerMask groundLayer;

    private float jump_timer;
    private float x;
    private float time_since_grounded;

    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        jump_timer = -1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        x = Input.GetAxisRaw("Horizontal");
        Jump();
    }
    void FixedUpdate()
    {
        Move();
        CheckIfGrounded();
    }

    void Move()
    {
        float moveBy = x * speed;
        rb.velocity = new Vector2(moveBy, rb.velocity.y);
    }

    void Jump()
    {
        if (isGrounded) 
        {
            jump_timer = 1.0f;
        }
        if (jump_timer > set_jump_timer)
        {
            float jump = Input.GetAxisRaw("Jump");
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y + (jump * jump_power * jump_timer * Time.deltaTime));
            if(jump < 0.1)
            {
                jump_timer = -1.0f;
            }
            jump_timer -= Time.deltaTime;
        }
    }

    void CheckIfGrounded()
    {
        isGrounded = ground_collider.IsTouchingLayers(groundLayer);
        if(isGrounded)
        {
            time_since_grounded = 0.0f;
        } else
        {
            time_since_grounded += Time.deltaTime;
        }
    }
}
