using System.Collections.Generic;
using UnityEngine;

public class JokerSystem : MonoBehaviour
{
    public JokerManager jokerManager;

    // CHIPS
    public int ApplyChips(int baseChips, CardData card)
    {
        int value = baseChips;

        foreach (var joker in jokerManager.ownedJokers)
        {
            value = ApplyJokerChipEffect(joker, value, card);
        }

        return value;
    }

    // MULT
    public int ApplyMult(int baseMult, CardData card)
    {
        int value = baseMult;

        foreach (var joker in jokerManager.ownedJokers)
        {
            value = ApplyJokerMultEffect(joker, value, card);
        }

        return value;
    }

    // X MULT
    public double ApplyXMult(double baseXMult, CardData card)
    {
        double value = baseXMult;

        foreach (var joker in jokerManager.ownedJokers)
        {
            value = ApplyJokerXMultEffect(joker, value, card);
        }

        return value;
    }

    private int ApplyJokerChipEffect(JokerData joker, int value, CardData card)
    {
        switch (joker.type)
        {
            case JokerType.AddChips:
                return value + (int)joker.value;

            default:
                return value;
        }
    }
    
    private int ApplyJokerMultEffect(JokerData joker, int value, CardData card)
    {
        switch (joker.type)
        {
            case JokerType.AddMult:
                return value + (int)joker.value;

            case JokerType.FirstCardBonus:
                return value + 2;

            default:
                return value;
        }
    }

    private double ApplyJokerXMultEffect(JokerData joker, double value, CardData card)
    {
        switch (joker.type)
        {
            case JokerType.MultiplyX:
                return value * joker.value;

            default:
                return value;
        }
    }
}