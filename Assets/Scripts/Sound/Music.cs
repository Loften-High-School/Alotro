using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class Music : MonoBehaviour
{

    [Header("Music")]
    public AudioSource audioSource;
    public AudioClip[] soundTrack;
    public int currentTrack = 1;

    [Header("Sound Effects")]
    public AudioSource soundEffectsSource;
    public AudioClip[] soundEffects;

    [Header("Slider")]
    public Slider gameSpeedSlider;
    public Slider masterSlider;
    public Slider musicSlider;
    public Slider gameSlider;
    
    [Header("Text")]
    public TMP_Text gameSpeedText;
    public TMP_Text masterText;
    public TMP_Text musicText;
    public TMP_Text gameText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        soundTrack = Resources.LoadAll<AudioClip>("Audio");

        // Starts playing the first track
        PlayNextTrack();

        gameSpeedSlider.value = 1;
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
        masterText.text = Math.Round(masterSlider.value * 100).ToString();
        musicText.text = Math.Round(musicSlider.value * 100).ToString();
        gameText.text = Math.Round(gameSlider.value * 100).ToString();

        UpdateVolume();

    }

    public void UpdateVolume()
    {
        // Set the volume of the audio sources based on the slider values
        audioSource.volume = masterSlider.value * musicSlider.value;
        soundEffectsSource.volume = masterSlider.value * gameSlider.value;
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
