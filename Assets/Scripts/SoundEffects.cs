using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffects : MonoBehaviour
{
    public static SoundEffects Instance;

    public AudioSource audioSource;
    public AudioClip chop;
    public AudioClip pestleSound;
    public AudioClip wrongSound;
    public AudioClip flip;
    public AudioClip doorSound;
    public AudioClip greenSound;
    public AudioClip yellowSound;
    public AudioClip redSound;
    public AudioClip doneSound;
    public AudioClip furnaceItemInteractionSound;
    public AudioClip cutSound;
    public AudioClip splashSound;
    public AudioClip sturSound;
    public AudioClip sleepSound;

    private void Awake()
    {
        Instance = this;
    }
    private void PlaySound()
    {
        if(!audioSource.isPlaying)
            audioSource.Play();
    }
    public void SleepSound()
    {
        audioSource.clip = sleepSound;
        PlaySound();
    }
    public void ChopSound()
    {
        audioSource.clip = chop;
        PlaySound();
    }
    public void PestleSound()
    {
        audioSource.clip = pestleSound;
        PlaySound();
    }
    public void WrongSound()
    {
        audioSource.clip = wrongSound;
        PlaySound();
    }
    public void FlipSound()
    {
        audioSource.clip = flip;
        PlaySound();
    }
    public void DoorSound()
    {
        audioSource.clip = doorSound;
        PlaySound();
    }
    public void GreenSound()
    {
        audioSource.clip = greenSound;
        PlaySound();
    }
    public void YellowSound()
    {
        audioSource.clip = yellowSound;
        PlaySound();
    }
    public void RedSound()
    {
        audioSource.clip = redSound;
        PlaySound();
    }
    public void DoneSound()
    {
        audioSource.clip = doneSound;
        PlaySound();
    }
    public void FurnaceItemSound()
    {
        audioSource.clip = furnaceItemInteractionSound;
        PlaySound();
    }
    public void CutSound()
    {
        audioSource.clip = cutSound;
        PlaySound();
    }
    public void SplashSound()
    {
        audioSource.clip = splashSound;
        PlaySound();
    }
    public void SturSound()
    {
        audioSource.clip = sturSound;
        PlaySound();
    }
}
