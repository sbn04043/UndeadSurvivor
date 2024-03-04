using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    enum Sound { Bgm, Sfx };
    Sound[] sounds;

    [Header("BGM")]
    public AudioClip bgmClip;
    public float bgmVolume;
    public AudioSource bgmPlayer;
    public AudioHighPassFilter bgmEffect;

    [Header("SFX")]
    public AudioClip[] sfxClips;
    public float sfxVolume;
    public int channels;
    public AudioSource[] sfxPlayers;
    int channelIndex;

    public enum Sfx { Dead, Hit0, Hit1, LevelUp = 3, Lose, Melee0, Melee1, Range = 7, Select, Win }

    private void Awake()
    {
        instance = this;
        sounds = (Sound[])Enum.GetValues(typeof(Sound));
        Init();
    }

    void Init()
    {
        if (!PlayerPrefs.HasKey("AudioData"))
        {
            PlayerPrefs.SetInt("AudioData", 1);
            foreach (Sound sound in sounds)
            {
                PlayerPrefs.SetFloat(sound.ToString(), 0.5f);
            }
        }

        bgmVolume = PlayerPrefs.GetFloat("Bgm"); 
        GameObject bgmObject = new GameObject("BgmPlayer");
        bgmObject.transform.parent = transform;
        bgmPlayer = bgmObject.AddComponent<AudioSource>();
        bgmPlayer.playOnAwake = false;
        bgmPlayer.loop = true;
        bgmPlayer.volume = bgmVolume;
        bgmPlayer.clip = bgmClip;
        bgmEffect = Camera.main.GetComponent<AudioHighPassFilter>();

        sfxVolume = PlayerPrefs.GetFloat("Sfx");
        GameObject sfxObject = new GameObject("SfxPlayer");
        sfxObject.transform.parent = transform;
        sfxPlayers = new AudioSource[channels * 2];
        channelIndex = 0;

        for (int index = 0; index < sfxPlayers.Length; index++)
        {
            sfxPlayers[index] = sfxObject.AddComponent<AudioSource>();
            sfxPlayers[index].playOnAwake = false;
            sfxPlayers[index].bypassListenerEffects = true;
            sfxPlayers[index].volume = sfxVolume;
        }
    }

    public void PlayBgm(bool isPlay)
    {
        if (isPlay)
        {
            bgmPlayer.Play();
        } 
        else
        {
            bgmPlayer.Stop();
        }
    }

    public void EffectBgm(bool isPlay)
    {
        bgmEffect.enabled = isPlay;
    }

    public void PlaySfx(Sfx sfx)
    {
        while(true)
        {
            if (sfxPlayers[channelIndex].isPlaying)
            {
                channelIndex++;
                channelIndex %= sfxPlayers.Length;
                continue;
            }

            int ranInt = 0;
            if (sfx == Sfx.Hit0 || sfx == Sfx.Melee0)
            {
                ranInt = UnityEngine.Random.Range(0, 2);
            }

            sfxPlayers[channelIndex].clip = sfxClips[(int)sfx + ranInt];
            sfxPlayers[channelIndex].Play();
            break;
        }
    }

    public void ChangeSound()
    {
        bgmPlayer.volume = PlayerPrefs.GetFloat("Bgm");
        for (int index = 0; index < sfxPlayers.Length; index++)
        {
            sfxPlayers[index].volume = PlayerPrefs.GetFloat("Sfx");
        }
    }
}
