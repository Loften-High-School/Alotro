using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using System;
using TMPro;
using System.Linq;
using Unity.Mathematics;

public class HandManager : MonoBehaviour
{
    [Header("Scripts")]
    public Hand handScript;
    public PlayerGameInfo PGI;
    public HandRewardDatabase rewardDB;
    public PlayHand PH;
    public JokerManager JM;

    [Header("References")]
    public GameObject cardPrefab;
    public GameObject floatingTextPrefab;
    public GameObject floatingSquarePrefab;
    public Transform uiCanvas;
    public Transform handArea;
    public Transform playZone;

    [Space]
    public TMP_Text currentHand;

    [Header("Temp in script"), Space]
    public bool loseGame;

    [Header("Hand Settings")]
    public List<CardData> hand = new List<CardData>();
    public bool sortByRank = true;

    public float spacing = 150f;
    public float curveHeight = 50f;
    public float rotationAmount = 5f;
    public float liftAmount = 30f;

    [Header("Play Settings")]
    public float playSpacing = 150f;

    public void DrawStartingHand()
    {
        PGI.roundScore = 0;
        PGI.handsLeft = 4;
        PGI.discardsLeft = 4;

        StartCoroutine(handScript.DrawCard());
    }

    public void AddCard(CardData card)
    {
        PGI.deck -= 1;
        hand.Add(card);
        SortHand();
        DisplayHand();
    }

    public void PlaySelectedCards()
    {
        List<GameObject> selectedObjects = new List<GameObject>();

        foreach (Transform child in handArea)
        {
            CardDisplay cd = child.GetComponent<CardDisplay>();

            if (cd != null && cd.IsSelected())
            {
                selectedObjects.Add(child.gameObject);
            }
        }

        List<CardData> cardDataList = new List<CardData>();

        foreach (GameObject obj in selectedObjects)
        {
            CardDisplay display = obj.GetComponent<CardDisplay>();

            if (display != null)
            {
                cardDataList.Add(display.cardData);
            }
        }

        HandResult result = PokerHandEvaluator.EvaluateHand(cardDataList);

        Debug.Log($"<color=cyan>Checks: </color>Hand:  <color=cyan>{result.rank}</color>");

        foreach (var card in result.scoringCards)
        {
            Debug.Log($"<color=cyan>Checks: </color>Scoring: <color=cyan>{card.value}</color>  of <color=cyan>{card.suit}</color>");
        }

        HandReward reward = rewardDB.GetReward(result.rank);

        StartCoroutine(ResolvePlayAnimated(selectedObjects, result.scoringCards, reward));
    }

    public List<CardData> GetSelectedCardsData()
    {
        List<CardData> selected = new List<CardData>();

        for (int i = 0; i < hand.Count; i++)
        {
            if (hand[i].isSelected)
            {
                selected.Add(hand[i]);
            }
        }

        return selected;
    }

    int GetCardMult(CardData card)
    {
        return 0; // test value for now
    }

    double GetCardXMult(CardData card)
    {
        return 0; // test value for now
    }

    public string GetHandDisplayName(HandRank rank)
    {
        switch (rank)
        {
            case HandRank.HighCard: return "High Card";
            case HandRank.Pair: return "Pair";
            case HandRank.TwoPair: return "Two Pair";
            case HandRank.ThreeOfAKind: return "Three of a Kind";
            case HandRank.Straight: return "Straight";
            case HandRank.Flush: return "Flush";
            case HandRank.FullHouse: return "Full House";
            case HandRank.FourOfAKind: return "Four of a Kind";
            case HandRank.StraightFlush: return "Straight Flush";
            case HandRank.FiveOfAKind: return "Five of a Kind";
            case HandRank.FlushHouse: return "Flush House";
            case HandRank.FlushFive: return "Flush Five";
            case HandRank.RoyalFlush: return "Royal Flush";
            default: return "";
        }
    }

    public void UpdateLiveHandPreview()
    {
        List<CardData> selected = new List<CardData>();

        for (int i = 0; i < hand.Count; i++)
        {
            if (hand[i].isSelected)
            {
                selected.Add(hand[i]);
            }
        }

        if (selected.Count == 0)
        {
            currentHand.text = "";
            PGI.chips = 0;
            PGI.mult = 0;
            return;
        }

        HandResult rank = PokerHandEvaluator.EvaluateHand(selected);

        currentHand.text = GetHandDisplayName(rank.rank);
        PGI.chips = rewardDB.GetReward(rank.rank).chips;
        PGI.mult = rewardDB.GetReward(rank.rank).mult;
    }

    IEnumerator ResolvePlayAnimated(List<GameObject> cards, List<CardData> scoringCards, HandReward reward)
    {
        currentHand.text = GetHandDisplayName(reward.handRank);

        for (int i = hand.Count - 1; i >= 0; i--)
        {
            if (hand[i].isSelected)
            {
                hand.RemoveAt(i);
                PGI.hand -= 1;
            }
        }

        PGI.handsLeft --;

        float center = (cards.Count - 1) / 2f;

        // -------------------------
        // MOVE CARDS TO PLAY ZONE (SPREAD OUT)
        // -------------------------
        for (int i = 0; i < cards.Count; i++)
        {
            RectTransform rt = cards[i].GetComponent<RectTransform>();

            rt.SetParent(playZone);

            float x = (i - center) * playSpacing;

            rt.anchoredPosition = new Vector2(x, 0);
            rt.localRotation = Quaternion.identity;
        }

        DisplayHand();

        JokerSystem.HandContext context = new JokerSystem.HandContext
        {
            handRank = reward.handRank,
            scoringCards = scoringCards
        };

        yield return new WaitForSeconds(1f);

        // -------------------------
        // SCORING CHAIN
        // -------------------------

        JM.OnPlayedJoker(scoringCards, context);

        for (int i = 0; i < scoringCards.Count; i++)
        {
            CardData card = scoringCards[i];

            float chipValue = 0;
            chipValue = JM.ApplyChipsOnScoredJokers(card, context);
            int multValue = 0;
            double xMultValue = 0;

            // -------------------------
            // CHIPS
            // -------------------------
            PGI.chips += chipValue;
            GameObject chips = cards.First(c =>
                c.GetComponent<CardDisplay>().cardData == card
            );

            ShowFloatingText(chips.transform, "+" + chipValue);

            yield return new WaitForSeconds(1.3f);

            // -------------------------
            // MULT (ADDITIVE)
            // -------------------------
            if (multValue != 0)
            {
                PGI.mult += multValue;
                ShowFloatingText(cards[i].transform, "+" + multValue);
                GameObject mult = cards.First(c =>
                    c.GetComponent<CardDisplay>().cardData == card
                );

                ShowFloatingText(mult.transform, "+" + multValue);

                yield return new WaitForSeconds(1.3f);
            }

            // -------------------------
            // XMULT (MULTIPLICATIVE)
            // -------------------------
            if (xMultValue != 0 && xMultValue != 1)
            {
                PGI.mult = Math.Round(PGI.mult * xMultValue, 2);
                ShowFloatingText(cards[i].transform, "x" + xMultValue);
                GameObject xMult = cards.First(c =>
                    c.GetComponent<CardDisplay>().cardData == card
                );

                ShowFloatingText(xMult.transform, "+" + xMultValue);

                yield return new WaitForSeconds(1.3f);
            }

            yield return new WaitForSeconds(0.3f);
        }

        // -------------------------
        // FINAL SCORE CALCULATION
        // -------------------------
        currentHand.text = Math.Round(PGI.chips * PGI.mult, 2).ToString();
        yield return new WaitForSeconds(1f);
        
        PGI.roundScore += Math.Round(PGI.chips * PGI.mult, 2);
        currentHand.text = "";

        Debug.Log($"<color=yellow>Values: </color>FINAL SCORE: <color=yellow>{PGI.roundScore}</color>");

        yield return new WaitForSeconds(0.3f);

        PH.FindScore();

        // -------------------------
        // CLEANUP VISUALS
        // -------------------------

        PGI.chips = 0;
        PGI.mult = 1;

        for (int i = 0; i < cards.Count; i++)
        {
            Destroy(cards[i]);
        }

        UpdateLiveHandPreview();
        StartCoroutine(handScript.DrawCard());
        SortHand();
        DisplayHand();
    }

    void ShowFloatingText(Transform target, string text)
    {
        GameObject obj = Instantiate(floatingTextPrefab, uiCanvas.transform);
        GameObject square = Instantiate(floatingSquarePrefab, uiCanvas.transform);

        obj.transform.localScale = Vector3.one;
        square.transform.localScale = Vector3.one;

        TMPro.TextMeshProUGUI tmp = obj.GetComponent<TMPro.TextMeshProUGUI>();
        tmp.text = text;

        RectTransform canvasRect = uiCanvas.GetComponent<RectTransform>();
        RectTransform textRect = obj.GetComponent<RectTransform>();
        RectTransform squareRect = square.GetComponent<RectTransform>();

        // get SCREEN position of card
        Vector3 screenPos = RectTransformUtility.WorldToScreenPoint(null, target.position);

        // convert screen → canvas
        Vector2 anchoredPos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvasRect,
            screenPos,
            null,
            out anchoredPos
        );

        // PERFECT ALIGNMENT ABOVE CARD
        textRect.anchoredPosition = anchoredPos + new Vector2(0, 150);
        squareRect.anchoredPosition = anchoredPos + new Vector2(0, 150);

        StartCoroutine(FloatUp(textRect));
        StartCoroutine(FloatUp(squareRect));

        

        Destroy(obj, 0.3f);
    }

    IEnumerator FloatUp(RectTransform rt)
    {
        float time = 0f;
        Vector2 start = rt.anchoredPosition;
        Vector2 end = start + new Vector2(0, 50);

        while (time < 1f)
        {
            time += Time.deltaTime * 2f;
            if (rt != null)
            {
                rt.anchoredPosition = Vector2.Lerp(start, end, time);
            }
            yield return null;
        }
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

        StartCoroutine(handScript.DrawCard());
        PGI.discardsLeft -= 1;
        SortHand();
        DisplayHand();
    }

    public void OutOfCards() // Lose condition for when you completly run out of cards
    {
        if (PGI.deck <= 0 && hand.Count <= 0)
        {
            loseGame = true;
            StopCoroutine(handScript.DrawCard());
            Debug.Log("<color=red>Lost: </color>Out of Cards");
        }
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

    public void ClearHand()
    {
        #if UNITY_EDITOR
        UnityEditor.Selection.activeGameObject = null;
        #endif

        // Destroy all card objects in hand area
        for (int i = handArea.childCount - 1; i >= 0; i--)
        {
            Transform child = handArea.GetChild(i);
            Destroy(child.gameObject, 0.01f);
        }

        // Clear data
        hand.Clear();
        PGI.hand = 0;

        Debug.Log("<color=cyan>Check: </color>Hand cleared");
    }

    int GetRankValue(CardData card)
    {
        // Ace is high
        if (card.value == Rank.Ace) return 14;
        return (int)card.value;
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
            int suitCompare = GetSuitOrder(a.suit.ToString()).CompareTo(GetSuitOrder(b.suit.ToString()));

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