using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class JokerManager : MonoBehaviour
{
    [Header("Scripts"), Space]
    public JokerDatabase jokerDatabase;
    public PlayerGameInfo PGI;
    public JokerSystem jokerSystem;

    [Header("Objects"), Space]
    public Transform jokerArea; // UI parent (like handArea)
    public GameObject jokerPrefab;

    [Header("Variables"), Space]
    public List<JokerData> ownedJokers = new List<JokerData>();
    public List<GameObject> jokerObjects = new List<GameObject>();

    public float jokerSpacing = 150f;

    public bool test = false;
    
    public void GiveJoker(int index) // DEBUG: give joker by index
    {
        if (index < 0 || index >= jokerDatabase.jokers.Count)
        {
            Debug.LogError("Invalid joker index");
            return;
        }

        if (PGI.jokers >= PGI.jokerSlots)
        {
            Debug.Log("<color=red>Error: </color>Max Jokers reached!");
            return;
        }

        JokerData data = jokerDatabase.GetJoker(index);

        if (data == null)
        {
            Debug.LogError($"Joker is NULL at index {index}");
            return;
        }
        InitializeJoker(data);
        SpawnJoker(data);
    }

    public void SpawnJoker(JokerData data) // Spawns UI object
    {
        GameObject obj = Instantiate(jokerPrefab, jokerArea, false);
        ownedJokers.Add(data);
        jokerObjects.Add(obj);
        PGI.jokers ++;

        JokerDisplay display = obj.GetComponent<JokerDisplay>();
        display.Setup(data);

        LayoutGroup lg = jokerArea.GetComponent<LayoutGroup>();
        if (lg != null) lg.enabled = false;

        ArrangeJokers();
    }

    public void RemoveJoker(int index)
    {
        if (index < 0 || index >= jokerObjects.Count) return;

        Destroy(jokerObjects[index]);

        jokerObjects.RemoveAt(index);
        ownedJokers.RemoveAt(index);
        PGI.jokers --;
    }

    public void ArrangeJokers()
    {
        int count = jokerArea.childCount;

        if (count == 0) return;

        float totalWidth = (count - 1) * jokerSpacing;
        float startX = -totalWidth / 2f;

        for (int i = 0; i < count; i++)
        {
            RectTransform rt = jokerArea.GetChild(i).GetComponent<RectTransform>();

            if (rt == null) continue;

            float x = startX + (i * jokerSpacing);

            rt.anchoredPosition = new Vector2(x, 0);
            rt.localRotation = Quaternion.identity;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) GiveJoker(0);
        if (Input.GetKeyDown(KeyCode.Alpha2)) GiveJoker(1);
        if (Input.GetKeyDown(KeyCode.Alpha3)) GiveJoker(2);
        if (Input.GetKeyDown(KeyCode.Alpha4)) GiveJoker(3);

        if (test == true) 
        {
            for (int i = ownedJokers.Count; i >= 0; i--) 
            {    
                RemoveJoker(i);
            }
            test = false;
        }
        
    }

    public float ApplyChipsOnScoredJokers(CardData card, JokerSystem.HandContext context) // Get Rid Of Soon
    {
        float chips = GetBaseChipValue(card);

        foreach (var joker in ownedJokers)
        {
            if (joker.activation != Activation.OnScored)
            continue;

            // IMPORTANT: check card condition
            if (!jokerSystem.DoesCardMatch(joker, card, context))
                continue;

            switch (joker.type)
            {
                case JokerType.AddChips:
                    chips += joker.value;
                    break;

                case JokerType.AddMult:
                    // handle later
                    break;
            }
        }

        return chips;
    }
    
    public void OnPlayedJoker(List<CardData> cards, JokerSystem.HandContext context)
    {

        foreach (var joker in ownedJokers)
        {
            if (joker.activation != Activation.OnPlayed && 
                joker.activation != Activation.Mixed)
                continue;
            
            if (!DoesJokerMeetCondition(joker, context))
            {
                Debug.Log("<color=cyan>Checks: </color>Meet <color=red>Not</color> Conditions");
                continue;
            }

            Debug.Log("<color=cyan>Checks: </color>Meet Conditions"); // From DoesJokerMeetCondition()
            switch (joker.type)
            {
                case JokerType.Effect:
                    OnPlayedEffects(joker); // Later add arguments like: joker, card, context
                    break;
                
                case JokerType.Economy:
                    PGI.money += (int)joker.value;
                    break;
            }
        }
    }

    void OnPlayedEffects(JokerData joker) // Not here yet
    {
        
    }

    public void InitializeJoker(JokerData joker)
    {
        if (joker.useRandomTargetHand)
        {
            joker.targetHand = GetRandomHandRank();
        }
    }

    private HandRank GetRandomHandRank()
    {
        var values = System.Enum.GetValues(typeof(HandRank));
        return (HandRank)values.GetValue(Random.Range(0, values.Length));
    }

    bool DoesJokerMeetCondition(JokerData joker , JokerSystem.HandContext context)
    {
        switch (joker.condition)
        {
            case CardCondition.Any:
                return true;
            
            case CardCondition.FaceCard:
                return context.scoringCards.Any(c =>
                c.value == Rank.Jack ||
                c.value == Rank.Queen ||
                c.value == Rank.King);

            case CardCondition.Odd:
                return context.scoringCards.Any(c =>
                c.value == Rank.Ace ||
                c.value == Rank.Nine ||
                c.value == Rank.Seven ||
                c.value == Rank.Five ||
                c.value == Rank.Three);
            
            case CardCondition.Even:
                return context.scoringCards.Any(c =>
                c.value == Rank.Two ||
                c.value == Rank.Four ||
                c.value == Rank.Six ||
                c.value == Rank.Eight ||
                c.value == Rank.Ten);
            
            case CardCondition.SpecificRank:
                return context.scoringCards.Any(c =>
                c.value == joker.targetRank);

            case CardCondition.SpecificSuit:
                return context.scoringCards.Any(c =>
                c.suit == joker.targetSuit);
            
            case CardCondition.SpecificHand:
            
                if (context.handRank != joker.targetHand)
                    return false;

                if (joker.requiresAce)
                    return context.scoringCards.Any(c => c.value == Rank.Ace);

                return true;
                
            default:
                return true;
        }
    }

    public float GetBaseChipValue(CardData card)
    {
        switch (card.value)
        {
            case Rank.Ace: return 11;
            case Rank.King: return 10;
            case Rank.Queen: return 10;
            case Rank.Jack: return 10;
            default: return (int)card.value;
        }
    }
}