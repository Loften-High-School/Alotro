using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CardDisplay : MonoBehaviour, IPointerClickHandler
{
    public Image artwork;
    public Image highlight;

    private CardData cardData;
    private HandManager handManager;

    private RectTransform rectTransform;
    private Vector2 originalPos;

    public float liftAmount = 30f;

    public void Init(CardData data, HandManager manager)
    {
        cardData = data;
        handManager = manager;

        rectTransform = GetComponent<RectTransform>();

        artwork.sprite = data.sprite;
        artwork.preserveAspect = true;

        UpdateVisual();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        cardData.isSelected = !cardData.isSelected;
        UpdateVisual();
    }

    void UpdateVisual()
    {
        if (highlight != null)
            highlight.enabled = cardData.isSelected;

        // lift effect
        if (cardData.isSelected)
        {
            rectTransform.anchoredPosition += new Vector2(0, liftAmount);
        }
        else
        {
            rectTransform.anchoredPosition -= new Vector2(0, liftAmount);
        }
    }
}