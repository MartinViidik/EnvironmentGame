﻿using UnityEngine;
using System.Collections;
public class PlayerMovement : MonoBehaviour
{
    [Header("Player Attributes")]
    [SerializeField] float speed;

    [Header("References")]
    [SerializeField] Rigidbody2D rb;
    [SerializeField] Animator anim;
    SpriteRenderer playerSprite;

    float horizontal;
    float vertical;
    float movementSpeed;
    Vector2 movementDirection;
    float lastDirectionHorizontal = 0;
    float lastDirectionVertical = 0;

    bool checkForGround = false;
    bool isFlyingBackwards = false;
    float flyingSpeed; // depends on trap
    Direction flyingDirection = Direction.East;

    float groundCoordinate;
    public bool active = true;
    void Awake()
    {
        if (rb == null)
        {
            rb = GetComponent<Rigidbody2D>();
        }
        playerSprite = anim.gameObject.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (active)
        {
            GetInput();
        } else {
            return;
        }
    }

    private void FixedUpdate()
    {
        movementDirection = new Vector2(horizontal, vertical);
        rb.velocity = movementDirection * speed;
        movementSpeed = Mathf.Clamp(movementDirection.magnitude, 0.0f, 1.0f);
        if(movementSpeed > 0)
        {
            lastDirectionHorizontal = horizontal;
            lastDirectionVertical = vertical;
            Animate(horizontal, vertical, movementSpeed);
        } else {
            Animate(lastDirectionHorizontal, lastDirectionVertical, 0);
        }
        float lastDir = transform.eulerAngles.y;

        if (rb.IsSleeping())
        {
            rb.WakeUp();
        }
        if (checkForGround && transform.position.y < groundCoordinate)
        {
            rb.gravityScale = 0;
        }
        if (isFlyingBackwards)
        {
            if (flyingDirection == Direction.East)
            {
                flyingSpeed *= 0.99f;
                rb.AddForce(Vector2.right * flyingSpeed);
            }
            else
            {
                flyingSpeed *= 0.99f;
                rb.AddForce(Vector2.left * flyingSpeed);
            }
        }
    }

    void GetInput()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        // don't allow up-down movment when flying in geyser vapors
        if (rb.gravityScale < 1)
            vertical = Input.GetAxisRaw("Vertical");
        else
            vertical = 0;
    }

    void Animate(float horizontal, float vertical, float speed)
    {
        anim.SetFloat("Horizontal", horizontal);
        anim.SetFloat("Vertical", vertical);
        anim.SetFloat("Speed", speed);
    }

    public void FallDown(float groundPositionY)
    {
        StartCoroutine(EnableGravityForDuration(4.5f, groundPositionY));
    }

    public void FlyBackwards(float speed, float flyDuration)
    {
        flyingSpeed = speed;
        StartCoroutine(EnableFlyingBackwardsForDuration(flyDuration));
        if (!playerSprite.flipX)
            flyingDirection = Direction.East;
        else
            flyingDirection = Direction.West;
    }

    public void AddForce(Vector2 force)
    {
        rb.AddForce(force);
    }

    private IEnumerator EnableFlyingBackwardsForDuration(float duration)
    {
        isFlyingBackwards = true;
        yield return new WaitForSeconds(duration);
        isFlyingBackwards = false;
    }

    private IEnumerator EnableGravityForDuration(float duration, float lowestAllowedCoordinate)
    {
        rb.gravityScale = 0;
        yield return new WaitForSeconds(0.8f);
        groundCoordinate = lowestAllowedCoordinate;
        checkForGround = true;
        rb.gravityScale = 21f;
        yield return new WaitForSeconds(duration);
        checkForGround = false;
        rb.gravityScale = 0;
    }
}