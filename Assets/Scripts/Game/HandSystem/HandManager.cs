using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HandManager : MonoBehaviour
{
    [Header("Scripts")]
    public Hand handScript;
    public PlayerGameInfo PGI;
    public HandRewardDatabase rewardDB;

    [Header("References")]
    public GameObject cardPrefab;
    public GameObject floatingTextPrefab;
    public Transform uiCanvas;
    public Transform handArea;
    public Transform playZone;

    [Header("Hand Settings")]
    public List<CardData> hand = new List<CardData>();
    public bool sortByRank = true;

    public float spacing = 150f;
    public float curveHeight = 50f;
    public float rotationAmount = 5f;
    public float liftAmount = 30f;

    [Header("Play Settings")]
    public float playSpacing = 150f;

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

        // Evaluate hand
        HandRank result = PokerHandEvaluator.EvaluateHand(selected);

        HandReward reward = rewardDB.GetReward(result);
        PGI.chips += reward.chips;
        PGI.mult = reward.mult;

        foreach (CardData card in selected)
        {
            PGI.chips += GetCardChipValue(card);
        }

        PGI.roundScore = PGI.chips * PGI.mult;

        Debug.Log($"Hand: {result} | Chips: {PGI.chips} | Mult: {reward.mult} | Score: {PGI.roundScore}");

        PGI.handsLeft -= 1;
        StartCoroutine(ResolvePlay(selected));
    }

    System.Collections.IEnumerator ResolvePlay(List<CardData> selected)
    {
        List<GameObject> playedObjects = new List<GameObject>();
    
        foreach (Transform child in handArea)
        {
            CardDisplay cd = child.GetComponent<CardDisplay>();
    
            if (cd != null && cd.IsSelected())
            {
                playedObjects.Add(child.gameObject);
            }
        }

        float center = (playedObjects.Count - 1) / 2f;

        for (int i = 0; i < playedObjects.Count; i++)
        {
            RectTransform rt = playedObjects[i].GetComponent<RectTransform>();

            rt.SetParent(playZone);

            float x = (i - center) * playSpacing;

            
            rt.anchoredPosition = new Vector2(x, 0);
            rt.localRotation = Quaternion.identity;
        }
    
        yield return new WaitForSeconds(1.5f);

        for (int i = 0; i < playedObjects.Count; i++)
        {
            Destroy(playedObjects[i]);
        }
    
        for (int i = hand.Count - 1; i >= 0; i--)
        {
            if (hand[i].isSelected)
            {
                hand.RemoveAt(i);
                PGI.hand -= 1;
            }
        }
    
        StartCoroutine(handScript.RepeatProcedure());
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

    int GetCardChipValue(CardData card)
    {
        if (card.value == 1 || card.value == 13) return 11; // Ace or King
        if (card.value == 12 || card.value == 11) return 10; // Queen or Jack
        return card.value;
    }
}