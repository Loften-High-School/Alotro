using UnityEngine;
using UnityEngine.Timeline;

public class Music : MonoBehaviour
{

    private AudioSource audioSource;
    public AudioClip[] soundTrack;
    public int currentTrack = 1;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Gets the AudioSource Component
        audioSource = GetComponent<AudioSource>();

        // Starts playing the first track
        PlayNextTrack();
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
    }

    public void PlayNextTrack()
    {
        audioSource.clip = soundTrack[currentTrack - 1];
        audioSource.Play();
    }
}
