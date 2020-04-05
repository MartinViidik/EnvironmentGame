using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wire : MonoBehaviour
{
    [HideInInspector]
    public bool canHurtPlayer = true;
    [HideInInspector]
    public bool isWireActive = true;

    private float fuelDamage = 0.91f;

    #region THROWING UP
    private bool canThrowUpPlayer = false;
    private float collisionUpThrow = 210;
    private float throwUpDuration = 0.1f;
    private float collisionDefaultUpThrow = 120;
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
    private AudioSource ac;

    private void Start()
    {
        ac = GetComponent<AudioSource>();
        ac.pitch = Random.Range(0.75f, 1f);
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        playerFuel = GameObject.FindGameObjectWithTag("PlayerFuel").GetComponent<PlayerFuel>();
        playerSprite = playerMovement.gameObject.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>();
        wireThrowUpLocationSet = false;
    }


    public void Toggle(bool setWireActive)
    {
        if (setWireActive)
        {
            ac.volume = 1;
        } else {
            ac.volume = 0;
        }
        Debug.Log("linked wires was hurt player " + canHurtPlayer + " " + Time.time);
        canHurtPlayer = setWireActive;
        Debug.Log("linked wires now can hurt player " + canHurtPlayer + " " + Time.time);
        wireAnimator.SetBool("active", setWireActive);
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

                //ThrowPlayerUpwards(collision);
                HurtPlayer();
            }
            else
            {
                Debug.Log("WIRE NOT ACTIVE!");
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
            //canThrowUpPlayer = false;
        }
    }

    private void ThrowPlayerUpwards(Collider2D collision)
    {
        if (canThrowUpPlayer)
            return;
        if (!wireThrowUpLocationSet)
        {
            wireThrowUpLocationSet = true;
            wireUpThrowStartPosition = transform.position.y + 0.5f;
            Debug.Log("WIRE START POS " + Time.time) ;
            StartCoroutine(ThrowPlayerBackForDuration());
            playerMovement.FallDown(wireUpThrowStartPosition);
            //playerMovement.FlyBackwards(wireKnockbackSpeed);
            playerCollision = collision;
            StartCoroutine(ThrowPlayerUpForDuration());        
        }
       
    }

    private IEnumerator ThrowPlayerUpForDuration()
    {
        canThrowUpPlayer = true;
        yield return new WaitForSeconds(throwUpDuration);
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
                collisionUpThrow += 2.5f;// - 0.1f * (Mathf.Abs(playerMovement.gameObject.transform.position.y - wireUpThrowStartPosition));
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
