using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    public AudioClip deathSound;
    public AudioClip[] hurtSound;
    private AudioSource ac;

    public AudioClip[] pickupSound;

    private void Awake()
    {
        ac = GetComponent<AudioSource>();
    }

    public void PlayPickupSound()
    {
        if (ac.isPlaying)
        {
            ac.Stop();
        }
        ac.PlayOneShot(pickupSound[Random.Range(0, pickupSound.Length)]);
    }

    public void PlayHurtSound()
    {
        if (!ac.isPlaying)
        {
            ac.PlayOneShot(hurtSound[Random.Range(0, hurtSound.Length)]);
        }
    }
    public void PlayDeathSound()
    {
        if (ac.isPlaying)
        {
            ac.Stop();
        }
        ac.PlayOneShot(deathSound);
    }
}
