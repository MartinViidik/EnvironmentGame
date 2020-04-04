using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Geyser : MonoBehaviour
{
    private float inactiveGeyserMinDuration = 2f;
    private float inactiveGeyserMaxDuration = 4f;
    private float geyserWarningDuration = 1f;
    private float activeGeyserDuration = 3f;

    private bool isGeyserActive = false;
    private bool hurtPlayerDuringActivity = false;

    private ParticleSystem geyserParticleSystem;
    private int geyserEmissionRate = 90;

    [SerializeField]
    private Animator geyserAnimator;

    private PlayerMovement playerMovement;

    private float collisionUpThrow = 180;
    private float collisionDefaultUpThrow = 180;
    private float geyserUpThrowStartPosition;
    private bool geyserThrowUpLocationSet = false;

    private void Start()
    {
        geyserParticleSystem = GetComponent<ParticleSystem>();
        geyserParticleSystem.emissionRate = 0;
        StartCoroutine(ActivateGeyserAfterDuration());
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
    }

    private IEnumerator ActivateGeyserAfterDuration()
    {
        
        float duration = Random.Range(inactiveGeyserMinDuration, inactiveGeyserMaxDuration);
        yield return new WaitForSeconds(duration);

        geyserAnimator.SetBool("isWarning", true);
        yield return new WaitForSeconds(geyserWarningDuration);
        geyserAnimator.SetBool("isWarning", false);

        isGeyserActive = true;
        geyserParticleSystem.emissionRate = geyserEmissionRate;
        StartCoroutine(EndGeyserActivityAfterDuration());
    }

    private IEnumerator EndGeyserActivityAfterDuration()
    {
        yield return new WaitForSeconds(activeGeyserDuration);
        isGeyserActive = false;
        hurtPlayerDuringActivity = false;
        geyserParticleSystem.emissionRate = 0;
        StartCoroutine(ActivateGeyserAfterDuration());
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && isGeyserActive)
        {
            HurtPlayer();
        }
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")// && !hurtPlayerDuringActivity)
        {
            if (isGeyserActive)
            {

                ThrowPlayerUpwards(collision);
                HurtPlayer();
            }
            else
            {
                geyserThrowUpLocationSet = false;
                collisionUpThrow = collisionDefaultUpThrow;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            geyserThrowUpLocationSet = false;
            collisionUpThrow = collisionDefaultUpThrow;
        }
    }

    //private void LetPlayerFall(Collider2D collision)
    //{
    //    collisionUpThrow = collisionDefaultUpThrow;
    //    if (geyserThrowUpLocationSet)
    //    {
    //        geyserThrowUpLocationSet = false;
            
    //    }
    //}

    private void ThrowPlayerUpwards(Collider2D collision)
    {
        if (!geyserThrowUpLocationSet)
        {
            geyserUpThrowStartPosition = transform.position.y + 0.5f;
            playerMovement.FallDown(geyserUpThrowStartPosition);
            playerMovement.FlyBackwards();
            geyserThrowUpLocationSet = true;
        }
        if (collisionUpThrow > 2)
        {
            collisionUpThrow += 2.5f - 2f*(playerMovement.gameObject.transform.position.y - geyserUpThrowStartPosition);
        }
        collision.attachedRigidbody.AddForce(Vector2.up * collisionUpThrow);
    }

    private void HurtPlayer()
    {
        hurtPlayerDuringActivity = true;
        playerMovement.ThrowPlayerBack();
    }


}
