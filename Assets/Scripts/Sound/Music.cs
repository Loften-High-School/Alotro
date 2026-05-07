using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.UI;
using TMPro;
using System;

public class Music : MonoBehaviour
{

    public AudioSource audioSource;
    public AudioClip[] soundTrack;
    public int currentTrack = 1;

    public AudioSource soundEffectsSource;
    public AudioClip[] soundEffects;

    public Slider gameSpeedSlider;
    public TMP_Text gameSpeedText;

    public Slider MasterVolumeSlider;
    public TMP_Text MasterVolumeText;
    public Slider MusicVolumeSlider;
    public TMP_Text MusicVolumeText;
    public Slider GameVolumeSlider;
    public TMP_Text GameVolumeText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        soundTrack = Resources.LoadAll<AudioClip>("Audio");

        // Starts playing the first track
        PlayNextTrack();

        gameSpeedSlider.value = 1;
        MasterVolumeSlider.value = 100;
        MusicVolumeSlider.value = 10;
        GameVolumeSlider.value = 100;

        audioSource.volume = MasterVolumeSlider.value * MusicVolumeSlider.value;
        soundEffectsSource.volume = MasterVolumeSlider.value * GameVolumeSlider.value;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("1"))
        {
            currentTrack = 1;
            PlayNextTrack();
        }
        if (Input.GetKeyDown("2"))
        {
            currentTrack = 2;
            PlayNextTrack();
        }
        if (Input.GetKeyDown("3"))
        {
            currentTrack = 3;
            PlayNextTrack();
        }
        if (Input.GetKeyDown("4"))
        {
            currentTrack = 4;
            PlayNextTrack();
        }
        if (Input.GetKeyDown("5"))
        {
            currentTrack = 5;
            PlayNextTrack();
        }

        // Set the text boxes to the current values of the sliders
        gameSpeedText.text = gameSpeedSlider.value.ToString("0.0") + "x";
        MasterVolumeText.text = Math.Round(MasterVolumeSlider.value * 100f).ToString();
        MusicVolumeText.text = Math.Round(MusicVolumeSlider.value * 100f).ToString();
        GameVolumeText.text = Math.Round(GameVolumeSlider.value * 100f).ToString();

        UpdateVolume();

    }

    public void UpdateVolume()
    {
        // Set the volume of the audio sources based on the slider values
        audioSource.volume = MasterVolumeSlider.value * MusicVolumeSlider.value;
        soundEffectsSource.volume = MasterVolumeSlider.value * GameVolumeSlider.value;
    }

    public void PlayNextTrack()
    {
        audioSource.clip = soundTrack[currentTrack - 1];
        audioSource.Play();
    }

    public void PlaySoundEffect(int index)
    {
        if (index >= 0 && index < soundEffects.Length)
        {
            soundEffectsSource.PlayOneShot(soundEffects[index]);
        }
        else
        {
            Debug.LogWarning("Sound effect index out of range: " + index);
        }
    }
}
