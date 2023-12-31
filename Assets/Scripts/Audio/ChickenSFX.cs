using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenSFX : MonoBehaviour
{
    private AudioSource audioSource;
    [SerializeField] private AudioClip[] hmmSound;
    [SerializeField] private AudioClip[] flapSounds;
    [SerializeField] private AudioClip[] longFlapSounds;
    [SerializeField] private AudioClip[] firstLandSounds;
    [SerializeField] private AudioClip[] secondLandSounds;
    [SerializeField] private AudioClip[] jumpSounds;


    // Start is called before the first frame update
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void HmmSounds()
    {
        if (hmmSound.Length > 0)
        {
            // Sets a hmm variable as a random audio clip in the hmmSound AudioClip array and plays the audio clip
            AudioClip hmm = hmmSound[Random.Range(0, hmmSound.Length)];
            audioSource.PlayOneShot(hmm);
        }
    }

    public void FlapSounds()
    {
        if (flapSounds.Length > 0)
        {
            // Sets a flap variable as a random audio clip in the flapSounds AudioClip array and plays the audio clip
            AudioClip flap = flapSounds[Random.Range(0, flapSounds.Length)];
            audioSource.PlayOneShot(flap);
        }
    }

    public void LongFlapSounds()
    {
        if (longFlapSounds.Length > 0)
        {
            // Sets a flap variable as a random audio clip in the longFlapSounds AudioClip array and plays the audio clip
            AudioClip flap = longFlapSounds[Random.Range(0, longFlapSounds.Length)];
            audioSource.PlayOneShot(flap);
        }
    }

    public void FirstLandSounds()
    {
        if (firstLandSounds.Length > 0)
        {
            // Sets a firstLand variable as a random audio clip in the firstLandSounds AudioClip array and plays the audio clip
            AudioClip firstLand = firstLandSounds[Random.Range(0, firstLandSounds.Length)];
            audioSource.PlayOneShot(firstLand);
        }
    }

    public void SecondLandSounds()
    {
        if (secondLandSounds.Length > 0)
        {
            // Sets a secondLand variable as a random audio clip in the secondLandSounds AudioClip array and plays the audio clip
            AudioClip secondLand = secondLandSounds[Random.Range(0, secondLandSounds.Length)];
            audioSource.PlayOneShot(secondLand);
        }
    }

    public void JumpSounds()
    {
        if (jumpSounds.Length > 0)
        {
            // Sets a jump variable as a random audio clip in the jumpSounds AudioClip array and plays the audio clip
            AudioClip jump = jumpSounds[Random.Range(0, jumpSounds.Length)];
            audioSource.PlayOneShot(jump);
        }
    }
}
