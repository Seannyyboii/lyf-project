using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    public static AudioManager instance;

    // Start is called before the first frame update
    void Awake()
    {
        // An if statement is used to check if there is no instance of the AudioManager component in the scene.
        // if so, the instance would be set to the GameObject that this script is attached to
        // else, the existing instance would be destroyed

        if (instance == null) instance = this;
        else Destroy(gameObject);

        // The DontDestroyOnLoad function is called for the GameObject that this script is attached
        // which would prevent the GameObject to from being destroyed when moving in between scenes
        DontDestroyOnLoad(gameObject);

        // A foreach statement is used to iterate through the Sound[] array for a sound variable
        foreach(Sound sound in sounds)
        {
            // An AudioSource component is added to the GameObject that this script is attached
            sound.source = gameObject.AddComponent<AudioSource>();

            // The clip, volume, pitch, and loop variables within the AudioSource are then set
            sound.source.clip = sound.clip;

            sound.source.volume = sound.volume;
            sound.source.pitch = sound.pitch;

            sound.source.loop = sound.loop;
        }
    }

    public void Play(string name)
    {
        // Ternary operators are used to check if the name matches the sound in the Sound[] array
        Sound sound = Array.Find(sounds, sound => sound.name == name);

        // If the sound does not exist, return
        if (sound == null) return;

        // If the sound does exist, the sound would be played
        sound.source.Play();
    }

    public void PlayOneShot(string name)
    {
        // Ternary operators are used to check if the name matches the sound in the Sound[] array
        Sound sound = Array.Find(sounds, sound => sound.name == name);

        // If the sound does not exist, return
        if (sound == null) return;

        // If the sound does exist, the sound would be played
        sound.source.PlayOneShot(sound.clip);
    }

    public void Stop(string name)
    {
        // Ternary operators are used to check if the name matches the sound in the Sound[] array
        Sound sound = Array.Find(sounds, sound => sound.name == name);

        // If the sound does not exist, return
        if (sound == null) return;

        // If the sound does exist, the sound would be played
        sound.source.Stop();
    }
}
