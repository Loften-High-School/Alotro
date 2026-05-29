using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class JokerData
{
    public string jokerName;
    public string description;
    public Sprite artwork;
    public int price;
    public string rarity;

    public float value;

    public JokerType type;
    public Activation activation;
    
    public CardCondition condition;
    public Rank targetRank;
    public Suit targetSuit;

    public bool isSelected;
}