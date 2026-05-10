using System.Collections.Generic;
using NUnit.Framework.Constraints;
using UnityEngine;

public class Deck : MonoBehaviour
{

    [Space]
    public PlayerGameInfo playerGameInfo;

    /*
    5 digit # [suit, number 2 digits, enhancements, seal]
    Suit 
    1 = spades
    2 = clubs
    3 = hearts
    4 = diamonds
    Number
    01-ace 02-10 11-jack 12-queen 13-king
    Enhancements
    normal = 0, bonus, mult, wild. glass, steel, stone, gold, lucky
    Seals
    none = 0, gold, red, blue, purple
    */
    [HideInInspector]
    public int cardInfo = 00000;

    [Space] [Tooltip("All cards in the deck. Format: \n[suit, number 2 digits, enhancements, seal]")]
    public List<int> deck = new List<int>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        while (deck.Count < playerGameInfo.deckSize)
        {
            deck.AddRange(new int[] { 10100, 20100, 30100, 40100 }); // Adds aces
        }

        // Default Deck
        // deck.AddRange(new int[] { 10100, 10200, 10300, 10400, 10500, 10600, 10700, 10800, 10900, 11000, 11100, 11200, 11300 }); // Adds spades
        // deck.AddRange(new int[] { 20100, 20200, 20300, 20400, 20500, 20600, 20700, 20800, 20900, 21000, 21100, 21200, 21300 }); // Adds clubs
        // deck.AddRange(new int[] { 30100, 30200, 30300, 30400, 30500, 30600, 30700, 30800, 30900, 31000, 31100, 31200, 31300 }); // Adds hearts
        // deck.AddRange(new int[] { 40100, 40200, 40300, 40400, 40500, 40600, 40700, 40800, 40900, 41000, 41100, 41200, 41300 }); // Adds diamonds
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
