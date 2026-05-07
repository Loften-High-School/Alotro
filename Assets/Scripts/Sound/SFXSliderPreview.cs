using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SFXSliderPreview : MonoBehaviour, IPointerUpHandler
{
    public AudioSource soundEffectsSource;
    public AudioClip previewClip;

    public Music musicScript;

    public void OnPointerUp(PointerEventData eventData)
    {
        // Force update the music script to apply the new slider values before playing the preview sound
        musicScript.UpdateVolume();

        // Check if the sound effects source is not currently playing a sound
        //if (!soundEffectsSource.isPlaying)
        {
            // Play preview sound once when mouse is released
            soundEffectsSource.PlayOneShot(previewClip);
        }
    }
}