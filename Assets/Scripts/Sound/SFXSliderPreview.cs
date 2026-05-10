using UnityEngine;
using UnityEngine.EventSystems;

public class SFXSliderPreview : MonoBehaviour, IPointerUpHandler
{
    public Music Music;

    public AudioSource soundEffectsSource;
    public AudioClip previewClip;

    public void OnPointerUp(PointerEventData eventData)
    {
        Music.UpdateVolume();

        // Play preview sound once when mouse is released
        soundEffectsSource.PlayOneShot(previewClip);
    }
}