﻿using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SoundController : MonoBehaviour
{
    /// <summary>Static reference to the only occurence of this object. Used because Unity doesn't let you set info for static components</summary>
    public static SoundController Instance;

    public GameObject AudioSourceHolder;
    public AudioSource AudioSourceSFX;
    public AudioSource AudioSourceBGM;
    [Header("Temporary for Gate 2")] //and then she left it like this forever
    public AudioSource AudioSourceBGM_Menu;
    public AudioSource AudioSourceBGM_Fight;
    public AudioSource AudioSourceAmbience;
    public AudioClip[] SFXClips;
    public AudioClip[] BGMClips;
    public AudioClip[] AmbienceClips;
    public float startVolumeBGM = 1f;
    public float inGameVolumeBGM = 1f;

    [SerializeField]
    private Slider audioSlider;

    private int aktTrackNumber;

    public enum SFX
    {
        impact,
        impact_heavy,
        impact_light,
        impact_normal,
        woosh_heavy,
        woosh_light,
        woosh_normal,
        powerCharge1,
        powerCharge2,
        unleashedMode,
        health_below25,
        ui_cancel,
        ui_loadoutEquip,
        ui_select,
        ui_switchBetweenSlots,
        qte_block_good,
        qte_block_perfect,
        qte_timing_fail,
        qte_timing_good,
        numberofSFX
    }

    public enum BGM
    {
        fight1,
        fight2,
        menu1,
        numberofBGM
    }

    public enum Ambience
    {
        numberofAmbience
    }

    private void Start()
    {
        if (Instance != null)
        {
            Application.Quit();
        }

        Instance = this;
        //controls automatic BGM atm
        this.AudioSourceBGM.volume = this.startVolumeBGM;
        //aktTrackNumber = -1;    

        if (SFXClips.Length < (int)SFX.numberofSFX)
        {
            Debug.LogError("Not enough SFX assigned");
        }
        if (BGMClips.Length < (int)BGM.numberofBGM)
        {
            Debug.LogError("Not enough BGM assigned");
        }
        if (AmbienceClips.Length < (int)Ambience.numberofAmbience)
        {
            Debug.LogError("Not enough Ambience sounds assigned");
        }
    }

    private void Update()
    {
        //todo method to start/stop/change BGM

        ////goes through list of BGM
        //if (!AudioSourceBGM.isPlaying)
        //{
        //    aktTrackNumber = ++aktTrackNumber > this.BGMClips.Length - 1 ? 0 : aktTrackNumber;
        //    this.AudioSourceBGM.clip = this.BGMClips[aktTrackNumber];
        //    this.AudioSourceBGM.Play();
        //}
    }

    public void StartSFX(SFX sound, float volume = 1f)
    {
        this.AudioSourceSFX.PlayOneShot(this.SFXClips[(int)sound], volume);
    }

    public void StartAmbience(Ambience sound, float volume = 1f)
    {
        this.AudioSourceSFX.PlayOneShot(this.AmbienceClips[(int)sound], volume);
    }

    public void StartBGM(BGM sound, float volume = 1f)
    {
        AudioSourceBGM.loop = true;
        this.AudioSourceBGM.PlayOneShot(this.BGMClips[(int)sound], volume);
    }

    public void StartLoopingBGM(BGM sound, float volume = 1f)
    {
        //var retAudioSource = AudioSourceBGM;
        AudioSourceBGM.loop = true;
        AudioSourceBGM.clip = this.BGMClips[(int)sound];
        AudioSourceBGM.volume = volume;
        AudioSourceBGM.Play();
        //this.StartCoroutine(StopLoopingSoundDelayed(retAudioSource, 1f));
        //return retAudioSource;
    }

    public AudioSource StartLoopingAmbience(Ambience sound, float volume)
    {
        var retAudioSource = AudioSourceHolder.AddComponent<AudioSource>();
        retAudioSource.loop = true;
        retAudioSource.clip = this.AmbienceClips[(int)sound];
        retAudioSource.volume = volume;
        retAudioSource.Play();
        //this.StartCoroutine(StopLoopingSoundDelayed(retAudioSource, 1f));
        return retAudioSource;
    }

    public AudioSource StartLoopingSFX(SFX sound, float volume)
    {
        var retAudioSource = AudioSourceHolder.AddComponent<AudioSource>();
        retAudioSource.loop = true;
        retAudioSource.clip = this.AmbienceClips[(int)sound];
        retAudioSource.volume = volume;
        retAudioSource.Play();
        //this.StartCoroutine(StopLoopingSoundDelayed(retAudioSource, 1f));
        return retAudioSource;
    }

    //public AudioSource StartLoopingBGM(BGM sound, float volume)
    //{
    //    var retAudioSource = AudioSourceHolder.AddComponent<AudioSource>();
    //    retAudioSource.loop = true;
    //    retAudioSource.clip = this.BGMClips[(int)sound];
    //    retAudioSource.volume = volume;
    //    retAudioSource.Play();
    //    //this.StartCoroutine(StopLoopingSoundDelayed(retAudioSource, 1f));
    //    return retAudioSource;
    //}

    private static IEnumerator StopLoopingSoundDelayed(AudioSource retAudioSource, float delay)
    {
        yield return new WaitForSeconds(delay);
        StopLoopingSound(ref retAudioSource);
    }

    public static void StopLoopingSound(ref AudioSource inpAudioSource)
    {
        UnityEngine.Object.Destroy(inpAudioSource);
    }

    public IEnumerator StartSound(SFX sound, AudioClip sound2, float volume = 1f) {
        this.AudioSourceSFX.PlayOneShot(this.SFXClips[(int)sound], volume);
        yield return new WaitForSecondsRealtime(this.SFXClips[(int)sound].length + 0.5f);
        this.AudioSourceSFX.PlayOneShot(sound2, volume);
    }

    public void AudioSlider()
    {
        AudioSourceBGM.volume = audioSlider.value/5;
    }
}