using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioMaster : MonoBehaviour
{

    public static AudioMaster instance;

    AudioSource masterSource;
    AudioClip[] songList;
    int songPlaying = 0;
    float songTime = 0;
    public AudioClip[] levelSongs;
    private void Start()
    {
        SetupSongList(levelSongs);
        PlayLevelSong(0);
    }
    private void Awake()
    {
        if (!instance)
        {
            instance = this;
            masterSource = GetComponent<AudioSource>();

            LoadAudioPrefs();

            masterSource.volume = MusicVolume;
        }
        else
        {
            Destroy(this);
        }
    }

    public delegate void VolumeChangeEvent();
    public event VolumeChangeEvent OnVolumeChange;

    /// <summary>
    /// All Pause Menu Sliders should have this assigned to the OnValueChanged list
    /// See Level 1-1 for example
    /// </summary>
    public void UpdateVolumes()
    {
        if (OnVolumeChange != null)
        {
            OnVolumeChange();            
        }
        masterSource.volume = MusicVolume;
    }

    [SerializeField]
    [Range(0, 1f)]
    float _MasterVolume = .7f;

    [SerializeField]
    [Range(0, 1f)]
    float _MusicVolume = .7f;

    [SerializeField]
    [Range(0, 1f)]
    float _SFXVolume = .7f;

    public float MasterVolume
    {
        get { return _MasterVolume; }
        set { _MasterVolume = value; }
    }

    public float MusicVolume
    {
        get { return MasterVolume > 0 && _MusicVolume > 0 ? (_MasterVolume + _MusicVolume) / 2 : 0; }
        set { _MusicVolume = value; }
    }

    public float SFXVolume
    {
        get { return MasterVolume > 0 && _SFXVolume > 0 ? (_MasterVolume + _SFXVolume) / 2 : 0; }
        set { _SFXVolume = value; }
    }

    public float RawMusicValue() { return _MusicVolume;  }
    public float RawSFXValue() { return _SFXVolume; }

    private void NextSong()
    {
        songPlaying++;
        if (songPlaying >= songList.Length) songPlaying = 0;
    }

    public void SetupSongList(AudioClip[] songs, int playFirst = 0)
    {
        songList = songs;
        songPlaying = playFirst;
    }

    public void PlayLevelSong(int songIndex)
    {
        if (songList.Length > 0)
        {
            masterSource.volume = MusicVolume;
            masterSource.clip = songList[songIndex];

            masterSource.loop = true;

            masterSource.Play();
        }
    }
    public void StopSong()
    {
        masterSource.Stop();
    }

    public void PauseSong()
    {
        songTime = masterSource.time;
        int pausedSong = songPlaying;
        masterSource.Stop();
        masterSource.clip = songList[1]; // element 1 is the option screen song
        masterSource.time = 0;
        masterSource.Play();
    }

    public void unPauseSong()
    {
        masterSource.Stop();
        masterSource.clip = songList[songPlaying];
        masterSource.time = songTime;
        masterSource.Play();
    }

    public void SaveAudioPrefs()
    {
        PlayerPrefs.SetFloat("MasterVolume", _MasterVolume);
        PlayerPrefs.SetFloat("MusicVolume", _MusicVolume);
        PlayerPrefs.SetFloat("SFXVolume", _SFXVolume);
    }

    public void LoadAudioPrefs()
    {
        _MasterVolume =  PlayerPrefs.GetFloat("MasterVolume", _MasterVolume);
        _MusicVolume = PlayerPrefs.GetFloat("MusicVolume", _MusicVolume);
        _SFXVolume = PlayerPrefs.GetFloat("SFXVolume", _SFXVolume);
    }
}
