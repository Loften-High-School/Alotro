using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CardDatabase", menuName = "Cards/CardDatabase")]
public class CardDatabase : ScriptableObject
{
    public List<CardData> allCards;

    public CardData GetCard(Rank value, Suit suit)
    {
        return allCards.Find(c => c.value == value && c.suit == suit);
    }
}