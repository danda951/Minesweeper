using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManagerScript : MonoBehaviour
{
    public AudioSource myFX;
    public AudioClip hoverFX;
    public AudioClip clickFX;
    public AudioClip addFlagFX;
    public AudioClip removeFlagFX;
    public AudioClip removeSingleCoverFX;
    public AudioClip removeMultipleCoverFX;
    public AudioClip explosionFX;

    public void PlayHoverSound()
    {
        myFX.PlayOneShot(hoverFX);
    }

    public void PlayClickSound()
    {
        myFX.PlayOneShot(clickFX);
    }

    public void PlayAddFlagSound()
    {
        myFX.PlayOneShot(addFlagFX);
    }

    public void PlayRemoveFlagSound()
    {
        myFX.PlayOneShot(removeFlagFX);
    }

    public void PlayRemoveSingleCoverFX()
    {
        myFX.PlayOneShot(removeSingleCoverFX);
    }

    public void PlayRemoveMultipleCoverFX()
    {
        myFX.PlayOneShot(removeMultipleCoverFX);
    }

    public void PlayExplosionFX()
    {
        myFX.PlayOneShot(explosionFX);
    }
}
