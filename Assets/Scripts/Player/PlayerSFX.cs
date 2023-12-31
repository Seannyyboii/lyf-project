using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSFX : MonoBehaviour
{
    private AudioSource audioSource;
    [SerializeField] private AudioClip[] deflectSound;
    [SerializeField] private AudioClip[] damageSounds;
    [SerializeField] private AudioClip[] reloadEndSounds;
    [SerializeField] private AudioClip[] reloadClickSound;
    [SerializeField] private AudioClip[] drawSound;

    // Start is called before the first frame update
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void Deflect()
    {
        if (deflectSound.Length > 0)
        {
            // Sets a footstep variable as a random audio clip in the footstepSounds AudioClip array and plays the audio clip
            AudioClip deflect = deflectSound[Random.Range(0, deflectSound.Length)];
            audioSource.PlayOneShot(deflect);
        }
    }

    public void Damage()
    {
        if (damageSounds.Length > 0)
        {
            // Sets a footstep variable as a random audio clip in the footstepSounds AudioClip array and plays the audio clip
            AudioClip damage = damageSounds[Random.Range(0, damageSounds.Length)];
            audioSource.PlayOneShot(damage);
        }
    }

    private void ReloadClick()
    {
        // Sets a footstep variable as a random audio clip in the footstepSounds AudioClip array and plays the audio clip
        AudioClip reloadClick = reloadClickSound[Random.Range(0, reloadClickSound.Length)];
        audioSource.PlayOneShot(reloadClick);
    }

    private void Draw()
    {
        // Sets a footstep variable as a random audio clip in the footstepSounds AudioClip array and plays the audio clip
        AudioClip draw = drawSound[Random.Range(0, drawSound.Length)];
        audioSource.PlayOneShot(draw);
    }
}
