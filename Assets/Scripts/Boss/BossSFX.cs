using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSFX : MonoBehaviour
{
    private AudioSource audioSource;
    [SerializeField] private AudioClip[] roarSound;
    [SerializeField] private AudioClip[] stepSounds;
    [SerializeField] private AudioClip[] chargeAttackSounds;
    [SerializeField] private AudioClip[] damageSounds;
    [SerializeField] private AudioClip[] criticalDamageSounds;
    [SerializeField] private AudioClip[] stompSounds;
    [SerializeField] private AudioClip[] heavyStompSounds;
    [SerializeField] private AudioClip[] attackSound;
    [SerializeField] private AudioClip[] drawSound;
    [SerializeField] private AudioClip[] withdrawSound;

    // Start is called before the first frame update
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void RoarSounds()
    {
        if(roarSound.Length > 0)
        {
            // Sets a footstep variable as a random audio clip in the footstepSounds AudioClip array and plays the audio clip
            AudioClip roar = roarSound[Random.Range(0, roarSound.Length)];
            audioSource.PlayOneShot(roar);
        }
    }

    public void StepSounds()
    {
        if(stepSounds.Length > 0)
        {
            // Sets a footstep variable as a random audio clip in the footstepSounds AudioClip array and plays the audio clip
            AudioClip step = stepSounds[Random.Range(0, stepSounds.Length)];
            audioSource.PlayOneShot(step);
        }
    }

    public void ChargeAttackSounds()
    {
        if(chargeAttackSounds.Length > 0)
        {
            // Sets a footstep variable as a random audio clip in the footstepSounds AudioClip array and plays the audio clip
            AudioClip attack = chargeAttackSounds[Random.Range(0, chargeAttackSounds.Length)];
            audioSource.PlayOneShot(attack);
        }
    }

    public void DamageSounds()
    {
        if (damageSounds.Length > 0)
        {
            // Sets a footstep variable as a random audio clip in the footstepSounds AudioClip array and plays the audio clip
            AudioClip damage = damageSounds[Random.Range(0, damageSounds.Length)];
            audioSource.PlayOneShot(damage);
        }
    }

    public void CriticalDamageSounds()
    {
        if (criticalDamageSounds.Length > 0)
        {
            // Sets a footstep variable as a random audio clip in the footstepSounds AudioClip array and plays the audio clip
            AudioClip critDamage = criticalDamageSounds[Random.Range(0, criticalDamageSounds.Length)];
            audioSource.PlayOneShot(critDamage);
        }
    }

    public void StompSounds()
    {
        if (stompSounds.Length > 0)
        {
            // Sets a footstep variable as a random audio clip in the footstepSounds AudioClip array and plays the audio clip
            AudioClip stomp = stompSounds[Random.Range(0, stompSounds.Length)];
            audioSource.PlayOneShot(stomp);
        }
    }

    public void HeavyStompSounds()
    {
        if (heavyStompSounds.Length > 0)
        {
            // Sets a footstep variable as a random audio clip in the footstepSounds AudioClip array and plays the audio clip
            AudioClip heavyStomp = heavyStompSounds[Random.Range(0, heavyStompSounds.Length)];
            audioSource.PlayOneShot(heavyStomp);
        }
    }

    public void Attack()
    {
        if (attackSound.Length > 0)
        {
            // Sets a footstep variable as a random audio clip in the footstepSounds AudioClip array and plays the audio clip
            AudioClip attack = attackSound[Random.Range(0, attackSound.Length)];
            audioSource.PlayOneShot(attack);
        }
    }
}
