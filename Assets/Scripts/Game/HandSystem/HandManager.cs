using System.Collections.Generic;
using UnityEngine;

public class HandManager : MonoBehaviour
{
    [Header("Scripts")]
    public Hand handScript;
    public PlayerGameInfo PGI;

    [Header("References")]
    public GameObject cardPrefab;
    public Transform handArea;

    [Header("Hand Settings")]
    public List<CardData> hand = new List<CardData>();

    public float spacing = 150f;
    public float curveHeight = 50f;
    public float rotationAmount = 5f;
    public float liftAmount = 30f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PlaySelectedCards();
        }
    }

    public void AddCard(CardData card)
    {
        hand.Add(card);
        DisplayHand();
    }

    public void PlaySelectedCards()
    {
        // Remove selected cards safely (backwards loop)
        for (int i = hand.Count - 1; i >= 0; i--)
        {
            if (hand[i].isSelected)
            {
                hand.RemoveAt(i);
                PGI.hand -= 1;
            }
        }

        DisplayHand();

        StartCoroutine(handScript.RepeatProcedure());

        DisplayHand();
    }

    public void DisplayHand()
    {
        // Clear old visuals
        foreach (Transform child in handArea)
        {
            Destroy(child.gameObject);
        }

        // Spawn cards
        for (int i = 0; i < hand.Count; i++)
        {
            GameObject obj = Instantiate(cardPrefab, handArea);
            CardDisplay display = obj.GetComponent<CardDisplay>();

            display.Init(hand[i], this);

            RectTransform rt = obj.GetComponent<RectTransform>();

            float center = (hand.Count - 1) / 2f;

            float x = (i - center) * spacing;
            float y = -Mathf.Abs(i - center) * curveHeight;
            float rot = (i - center) * -rotationAmount;

            // Lift selected cards
            if (hand[i].isSelected)
            {
                y += liftAmount;
            }

            rt.anchoredPosition = new Vector2(x, y);
            rt.localRotation = Quaternion.Euler(0, 0, rot);
        }
    }
}