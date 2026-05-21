using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class JokerDisplay : MonoBehaviour, IPointerClickHandler
{
    public Image artwork;

    private JokerData jokerData;
    private HandManager handManager;

    private RectTransform rectTransform;

    public float liftAmount = 30f;

    public void Init(JokerData data, HandManager manager)
    {
        jokerData = data;
        handManager = manager;

        rectTransform = GetComponent<RectTransform>();

        artwork.sprite = data.sprite;
        artwork.preserveAspect = true;

        UpdateVisual();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!jokerData.isSelected)
        {
            // block selection if already at 5
            if (handManager.GetSelectedCount() >= 5)
                return;
        }

        jokerData.isSelected = !jokerData.isSelected;
        UpdateVisual();
        if (handManager != null)
        {
            handManager.UpdateLiveHandPreview();
        }
    }

    void UpdateVisual()
    {
        // lift effect
        if (jokerData.isSelected)
        {
            rectTransform.anchoredPosition += new Vector2(0, liftAmount);
        }
        else
        {
            rectTransform.anchoredPosition -= new Vector2(0, liftAmount);
        }
    }

    public bool IsSelected()
    {
        return jokerData.isSelected;
    }
}