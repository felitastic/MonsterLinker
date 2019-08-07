using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class logosfx : MonoBehaviour
{
    public void PlayGiggle(AudioClip sound)
    {
        SoundController.Instance.PlaySoundClip(sound);
    }
}
