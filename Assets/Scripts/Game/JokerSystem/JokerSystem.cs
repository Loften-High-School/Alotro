using System.Collections.Generic;
using UnityEngine;

public class JokerSystem : MonoBehaviour
{
    public JokerManager jokerManager;

    public int chips;
    public int mult;

    public void RunJokerPhase(Activation phase, CardData card, HandRank rank)
    {
        foreach (var joker in jokerManager.ownedJokers)
        {
            if (joker == null) continue;
            if (joker.activation != phase && joker.activation != Activation.Mixed) continue;

            ApplyJoker(joker, phase, card, rank);
        }
    }

    void ApplyJoker(JokerData joker, Activation phase, CardData card, HandRank rank)
    {
        switch (joker.type)
        {
            case JokerType.AddChips:
                
                HandleAddChips(joker, phase, card, rank);
                break;

            case JokerType.AddMult:
                mult += (int)joker.value;
                break;

            case JokerType.XMult:
                mult = (int)(mult * joker.value);
                break;
        }
    }

    void HandleAddChips(JokerData joker, Activation phase, CardData card, HandRank rank)
    {
        switch (joker.activation)
        {
            case Activation.Independent:
                if (phase == Activation.Independent)
                    chips += (int)joker.value;
                break;

            case Activation.OnScored:
                if (phase == Activation.OnScored)
                    chips += (int)joker.value;
                break;

            case Activation.Mixed:  
                // Example:
                if (phase == Activation.OnScored)
                    joker.value += joker.value;

                if (phase == Activation.Independent)
                    chips += (int)joker.value * 2;

                break;
        }
    }

    public bool DoesCardMatch(JokerData joker, CardData card)
    {
        switch (joker.condition)
        {
            case CardCondition.Any:
                return true;

            case CardCondition.FaceCard:
                return card.value == Rank.Jack ||
                       card.value == Rank.Queen ||
                       card.value == Rank.King;

            case CardCondition.Odd:
                return card.value == Rank.Ace ||
                       card.value == Rank.Nine ||
                       card.value == Rank.Seven ||
                       card.value == Rank.Five ||
                       card.value == Rank.Three;

            case CardCondition.Even:
                return card.value == Rank.Two ||
                    card.value == Rank.Four ||
                    card.value == Rank.Six ||
                    card.value == Rank.Eight ||
                    card.value == Rank.Ten;

            case CardCondition.SpecificRank:
                return card.value == joker.targetRank;

            case CardCondition.SpecificSuit:
                return card.suit == joker.targetSuit;

            default:
                return false;
        }
    }
}