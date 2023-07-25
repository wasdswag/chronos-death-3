using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public abstract class AudioPlayer : MonoBehaviour, IEventsListener
{
    public AudioSource sound;

    private void Awake()
    {
        sound = GetComponent<AudioSource>();
        SubscribeEvents();
    }

   

    private void OnDestroy() => UnSubscribeEvents();

    public abstract void SubscribeEvents();
    public abstract void UnSubscribeEvents();

    public abstract void PlayClip(AudioClip clip);

}
