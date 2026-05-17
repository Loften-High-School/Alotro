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
    public static HandRank EvaluateHand(List<CardData> cards)
    {
        if (cards == null || cards.Count == 0)
            return HandRank.HighCard;

        List<int> values = cards.Select(c => GetRankValue(c)).ToList();
        List<string> suits = cards.Select(c => c.suit).ToList();

        values.Sort();

        bool flush = suits.All(s => s == suits[0]);
        bool straight = IsStraight(values);

        var groups = values.GroupBy(v => v)
                           .Select(g => g.Count())
                           .OrderByDescending(x => x)
                           .ToList();

        int maxGroup = groups[0];

        bool isRoyal = IsRoyal(values);

        // -------------------------
        // 🔥 SPECIAL BALATRO HANDS
        // -------------------------

        if (flush && isRoyal) return HandRank.RoyalFlush;

        if (flush && groups[0] == 5)
            return HandRank.FlushFive;

        if (groups[0] == 5)
            return HandRank.FiveOfAKind;

        if (flush && groups[0] == 3 && groups.Contains(2))
            return HandRank.FlushHouse;

        // -------------------------
        // STANDARD POKER HANDS
        // -------------------------

        if (straight && flush) return HandRank.StraightFlush;

        if (groups[0] == 4) return HandRank.FourOfAKind;

        if (groups[0] == 3 && groups.Count > 1 && groups[1] == 2)
            return HandRank.FullHouse;

        if (flush) return HandRank.Flush;

        if (straight) return HandRank.Straight;

        if (groups[0] == 3) return HandRank.ThreeOfAKind;

        if (groups[0] == 2 && groups.Count > 1 && groups[1] == 2)
            return HandRank.TwoPair;

        if (groups[0] == 2) return HandRank.Pair;

        return HandRank.HighCard;
    }

    // -------------------------
    // HELPERS
    // -------------------------

    static bool IsStraight(List<int> values)
    {
        values = values.Distinct().ToList();
        values.Sort();

        if (values.Count < 5) return false;

        for (int i = 1; i < values.Count; i++)
        {
            if (values[i] != values[i - 1] + 1)
                return false;
        }

        return true;
    }

    static bool IsRoyal(List<int> values)
    {
        // A, K, Q, J, 10
        return values.Contains(14) &&
               values.Contains(13) &&
               values.Contains(12) &&
               values.Contains(11) &&
               values.Contains(10);
    }

    static int GetRankValue(CardData card)
    {
        if (card.value == 1) return 14; // Ace high
        return card.value;
    }
}