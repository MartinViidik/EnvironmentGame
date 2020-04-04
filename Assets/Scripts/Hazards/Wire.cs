using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wire : MonoBehaviour
{
    private bool canHurtPlayer = true;
    private float fuelDamage = 0.06f;
    private float collisionUpThrow = 40;
    private float collisionDefaultUpThrow = 40;
    private float wireUpThrowStartPosition;
    private bool wireThrowUpLocationSet = false;
    private float wireKnockbackSpeed = 60f;

    private bool isWireActive = true;

    [SerializeField]
    private Animator wireAnimator;

    private PlayerMovement playerMovement;
    private PlayerFuel playerFuel;



    private void Start()
    {
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        playerFuel = GameObject.FindGameObjectWithTag("PlayerFuel").GetComponent<PlayerFuel>();
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
        }
    }

    private void ThrowPlayerUpwards(Collider2D collision)
    {
        if (!wireThrowUpLocationSet)
        {
            wireUpThrowStartPosition = transform.position.y + 0.5f;
            playerMovement.FallDown(wireUpThrowStartPosition);
            playerMovement.FlyBackwards(wireKnockbackSpeed);
            wireThrowUpLocationSet = true;
        }
        if (collisionUpThrow > 2)
        {
            collisionUpThrow += 2.5f - 2f * (playerMovement.gameObject.transform.position.y - wireUpThrowStartPosition);
        }
        collision.attachedRigidbody.AddForce(Vector2.up * collisionUpThrow);
    }

    private void HurtPlayer()
    {
        if (canHurtPlayer)
            playerFuel.LoseFuel(fuelDamage);
    }
}
