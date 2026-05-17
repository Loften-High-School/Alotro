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
    public bool sortByRank = true;

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
        SortHand();
        DisplayHand();
    }

    public void PlaySelectedCards()
    {
        List<CardData> selected = new List<CardData>();

        for (int i = 0; i < hand.Count; i++)
        {
            if (hand[i].isSelected)
                selected.Add(hand[i]);
        }

        // 🧠 Evaluate hand
        HandRank result = PokerHandEvaluator.EvaluateHand(selected);

        Debug.Log("You played: " + result);

        // 🗑️ Remove selected cards
        for (int i = hand.Count - 1; i >= 0; i--)
        {
            if (hand[i].isSelected)
            {
                hand.RemoveAt(i);
                PGI.hand -= 1;
            }
        }

        DisplayHand();
        StartCoroutine(handScript.RepeatProcedure()); // Remove later on
        PGI.handsLeft -= 1;
        SortHand();
        DisplayHand();
    }

    public void DiscardSelectedCards()
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
        PGI.discardsLeft -= 1;
        SortHand();
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

    int GetRankValue(CardData card)
    {
        // Ace is high
        if (card.value == 1) return 14;
        return card.value;
    }

    int GetSuitOrder(string suit)
    {
        switch (suit)
        {
            case "Spades": return 1;
            case "Hearts": return 2;
            case "Clubs": return 3;
            case "Diamonds": return 4;
        }
        return 99;
    }

public void ChangeSortMethod(bool sortByRank)
    {
        this.sortByRank = sortByRank;
        SortHand();
        DisplayHand();
    }

    public void SortByRank()
    {
        hand.Sort((a, b) => GetRankValue(b).CompareTo(GetRankValue(a)));
        DisplayHand();
    }

    public void SortBySuit()
    {
        hand.Sort((a, b) =>
        {
            int suitCompare = GetSuitOrder(a.suit).CompareTo(GetSuitOrder(b.suit));

            if (suitCompare == 0)
            {
                // Sort by rank DESC inside suit
                return GetRankValue(b).CompareTo(GetRankValue(a));
            }

            return suitCompare;
        });

        DisplayHand();
    }

    public void SortHand()
    {
        if (sortByRank)
        {
            SortByRank();
        }
        else
        {
            SortBySuit();
        }
    }

    public int GetSelectedCount()
    {
        int count = 0;

        for (int i = 0; i < hand.Count; i++)
        {
            if (hand[i].isSelected)
                count++;
        }

        return count;
    }
}