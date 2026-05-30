using System.Collections.Generic;
using System.Linq;

public enum HandRank
{
    HighCard,
    Pair,
    TwoPair,
    ThreeOfAKind,
    Straight,
    Flush,
    FullHouse,
    FourOfAKind,
    StraightFlush,
    FiveOfAKind,
    FlushHouse,
    FlushFive,
    RoyalFlush
}

public static class PokerHandEvaluator
{
    public static HandResult EvaluateHand(List<CardData> cards)
    {
        HandResult result = new HandResult();

        if (cards == null || cards.Count == 0)
        {
            return new HandResult
            {
                rank = HandRank.HighCard,
                scoringCards = new List<CardData>()
            };
        }

        bool isFiveCardHand = cards.Count == 5;

        // Sort cards by value
        var sorted = cards.OrderBy(c => GetRankValue(c)).ToList();

        // Group by value
        var valueGroups = cards.GroupBy(c => c.value)
                               .OrderByDescending(g => g.Count())
                               .ToList();

        // Group by suit
        var suitGroups = cards.GroupBy(c => c.suit)
                              .OrderByDescending(g => g.Count())
                              .ToList();

        bool isFlush = suitGroups.First().Count() == cards.Count;
        bool isStraight = IsStraight(sorted);
        bool isRoyal =
                sorted.Any(c => c.value == Rank.Ten) &&
                sorted.Any(c => c.value == Rank.Jack) &&
                sorted.Any(c => c.value == Rank.Queen) &&
                sorted.Any(c => c.value == Rank.King) &&
                sorted.Any(c => c.value == Rank.Ace);

        // =========================
        // SPECIAL HANDS FIRST
        // =========================

        // Flush Five (5 same value AND flush)
        if (valueGroups.Count >= 1 && valueGroups[0].Count() == 5 && isFlush)
        {
            result.rank = HandRank.FlushFive;
            result.scoringCards = cards;
            return result;
        }

        // Flush House (Full House but all same suit)
        if (isFlush && valueGroups[0].Count() == 3 && valueGroups[1].Count() == 2)
        {
            result.rank = HandRank.FlushHouse;
            result.scoringCards = cards;
            return result;
        }
        
        // Five of a Kind
        if (valueGroups.Count >= 1 && valueGroups[0].Count() == 5)
        {
            result.rank = HandRank.FiveOfAKind;
            result.scoringCards = valueGroups[0].ToList();
            return result;
        }

        // Straight Flush
        if (isStraight && isFlush && isFiveCardHand)
        {
            // Royal Flush check
            if (isRoyal)
            {
                result.rank = HandRank.RoyalFlush;
            }
            else
            {
                result.rank = HandRank.StraightFlush;
            }

            result.scoringCards = cards;
            return result;
        }

        

        // =========================
        // STANDARD HANDS
        // =========================

        // Four of a Kind
        if (valueGroups.Count >= 1 && valueGroups[0].Count() == 4)
        {
            result.rank = HandRank.FourOfAKind;
            result.scoringCards = valueGroups[0].ToList();
            return result;
        }

        // Full House
        if (valueGroups.Count >= 2)
        {
            if (valueGroups[0].Count() == 3 && valueGroups[1].Count() == 2)
            {
                result.rank = HandRank.FullHouse;
                result.scoringCards = valueGroups[0].Concat(valueGroups[1]).ToList();
                return result;
            }
        }

        // Flush
        if (isFlush && isFiveCardHand)
        {
            result.rank = HandRank.Flush;
            result.scoringCards = cards;
            return result;
        }

        // Straight
        if (isStraight && isFiveCardHand)
        {
            result.rank = HandRank.Straight;
            result.scoringCards = cards;
            return result;
        }

        // Three of a Kind
        if (valueGroups.Count >= 1 && valueGroups[0].Count() == 3)
        {
            result.rank = HandRank.ThreeOfAKind;
            result.scoringCards = valueGroups[0].ToList();
            return result;
        }

        // Two Pair
        if (valueGroups.Count >=2 && valueGroups[0].Count() == 2 && valueGroups[1].Count() == 2)
        {
            result.rank = HandRank.TwoPair;
            result.scoringCards = valueGroups[0].Concat(valueGroups[1]).ToList();
            return result;
        }

        // Pair
        if (valueGroups.Count >= 1 && valueGroups[0].Count() == 2)
        {
            result.rank = HandRank.Pair;
            result.scoringCards = valueGroups[0].ToList();
            return result;
        }

        // High Card
        var highCard = sorted.Last();

        result.rank = HandRank.HighCard;
        result.scoringCards = new List<CardData> { highCard };

        return result;
    }

    static int GetRankValue(CardData card)
    {
        if (card.value == Rank.Ace) return 14;

        return (int)card.value;
    }

    private static bool IsStraight(List<CardData> sorted)
    {
        var values = sorted
            .Select(GetRankValue)
            .Distinct()
            .OrderBy(v => v)
            .ToList();

        if (values.Count != sorted.Count)
            return false;

        for (int i = 0; i < values.Count - 1; i++)
        {
            if (values[i + 1] != values[i] + 1)
                return false;
        }

        return true;
    }
}