using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wire : MonoBehaviour
{
    private bool canHurtPlayer = true;
    private bool isWireActive = true;

    private float fuelDamage = 0.11f;

    #region THROWING UP
    private bool canThrowUpPlayer = false;
    private float collisionUpThrow = 70;
    private float collisionDefaultUpThrow = 70;
    private float wireUpThrowStartPosition;
    private bool wireThrowUpLocationSet = false;
    private Collider2D playerCollision;
    #endregion

    private float wireKnockbackSpeed = 40f;
    private float wireKnocbackDuration = 30f;
    private bool canThrowPlayerBack = false;
    private Direction wireKnockBackDirection;

    [SerializeField]
    private Animator wireAnimator;

    private PlayerMovement playerMovement;
    private PlayerFuel playerFuel;
    private SpriteRenderer playerSprite;



    private void Start()
    {
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        playerFuel = GameObject.FindGameObjectWithTag("PlayerFuel").GetComponent<PlayerFuel>();
        playerSprite = playerMovement.gameObject.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && isWireActive)
        {
            HurtPlayer();
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")// && !hurtPlayerDuringActivity)
        {
            if (isWireActive)
            {

                ThrowPlayerUpwards(collision);
                HurtPlayer();
            }
            else
            {
                wireThrowUpLocationSet = false;
                collisionUpThrow = collisionDefaultUpThrow;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            wireThrowUpLocationSet = false;
            collisionUpThrow = collisionDefaultUpThrow;
            canThrowPlayerBack = false;
            canThrowUpPlayer = false;
            playerCollision = collision;
            StartCoroutine(ThrowPlayerUpForDuration());
        }
    }

    private void ThrowPlayerUpwards(Collider2D collision)
    {
        if (!wireThrowUpLocationSet)
        {
            wireUpThrowStartPosition = transform.position.y + 0.5f;
            Debug.Log("WIRE START POS " + wireUpThrowStartPosition);
            StartCoroutine(ThrowPlayerBackForDuration());
            playerMovement.FallDown(wireUpThrowStartPosition);
            //playerMovement.FlyBackwards(wireKnockbackSpeed);
            wireThrowUpLocationSet = true;
        }
       
    }

    private IEnumerator ThrowPlayerUpForDuration()
    {
        canThrowUpPlayer = true;
        yield return new WaitForSeconds(2f);
        canThrowUpPlayer = false;
    }

    private IEnumerator ThrowPlayerBackForDuration()
    {
        canThrowPlayerBack = true;
        if (!playerSprite.flipX)
        {
            wireKnockBackDirection = Direction.East;
        }
        else
        {
            wireKnockBackDirection = Direction.West;
        }
        yield return new WaitForSeconds(wireKnocbackDuration);
        canThrowPlayerBack = false;
    }

    private void FixedUpdate()
    {
        if (canThrowUpPlayer)
        {
            if (collisionUpThrow > 2)
            {
                collisionUpThrow += 2.5f - 0.1f * (Mathf.Abs(playerMovement.gameObject.transform.position.y - wireUpThrowStartPosition));
            }
            //Debug.Log("THROWING UP " + Time.time);
            playerCollision.attachedRigidbody.AddForce(Vector2.up * collisionUpThrow);
        }

        if (!canThrowPlayerBack)
            return;
        if (wireKnockBackDirection == Direction.East)
        {
            playerMovement.AddForce(new Vector2(wireKnockbackSpeed, 0));
        }
        else
        {
            playerMovement.AddForce(new Vector2(-wireKnockbackSpeed, 0));
        }
    }

    private void HurtPlayer()
    {
        if (!canHurtPlayer)
            return;

        playerFuel.LoseFuel(fuelDamage);


    }
}
