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
    public static bool muted = false;

    public void PlayHoverSound()
    {
        if (muted) return;
        myFX.PlayOneShot(hoverFX);
    }

    public void PlayClickSound()
    {
        if (muted) return;
        myFX.PlayOneShot(clickFX);
    }

    public void PlayAddFlagSound()
    {
        if (muted) return;
        myFX.PlayOneShot(addFlagFX);
    }

    public void PlayRemoveFlagSound()
    {
        if (muted) return;
        myFX.PlayOneShot(removeFlagFX);
    }

    public void PlayRemoveSingleCoverFX()
    {
        if (muted) return;
        myFX.PlayOneShot(removeSingleCoverFX);
    }

    public void PlayRemoveMultipleCoverFX()
    {
        if (muted) return;
        myFX.PlayOneShot(removeMultipleCoverFX);
    }

    public void PlayExplosionFX()
    {
        if (muted) return;
        myFX.PlayOneShot(explosionFX);
    }
}
