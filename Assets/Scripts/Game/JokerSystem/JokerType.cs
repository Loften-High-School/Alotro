public enum JokerType
{
    AddChips, //19
    AddMult, // 33
    XMult, // 35
    Effect, // 37
    Retrigger, // 6
    Economy, // 18
    AddChipMult // 2
}

public enum Activation
{
    Null, // (copies other joker) 2
    OnPlayed, // 4
    OnScored, // 28
    OnHeld, // 5
    Independent, // 57
    Mixed, // 13
    OnOtherJokers, // 1
    OnDiscard, // 4
    NA // (Passive) 36
}

public enum CardCondition
{
    Any,
    FaceCard,
    Odd,
    Even,
    SpecificRank,
    SpecificSuit,
    SpecificHand
}