using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour
{
    public HandManager handManager;

    public Sprite[] cardSprites;

    public List<CardData> deck = new List<CardData>();

    void Start()
    {
        BuildDeck();
        ShuffleDeck();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            DrawCard();
        }
    }

    void BuildDeck()
    {
        deck.Clear();

        string[] suits = { "Hearts", "Diamonds", "Clubs", "Spades" };

        int index = 0;

        for (int s = 0; s < suits.Length; s++)
        {
            for (int v = 1; v <= 13; v++)
            {
                CardData card = new CardData();
                card.value = v;
                card.suit = suits[s];

                if (index < cardSprites.Length)
                {
                    card.sprite = cardSprites[index];
                }

                deck.Add(card);
                index++;
            }
        }
    }

    void ShuffleDeck()
    {
        for (int i = 0; i < deck.Count; i++)
        {
            CardData temp = deck[i];
            int randomIndex = Random.Range(i, deck.Count);
            deck[i] = deck[randomIndex];
            deck[randomIndex] = temp;
        }
    }

    public void DrawCard()
    {
        if (deck.Count == 0) return;

        CardData card = deck[0];
        deck.RemoveAt(0);

        handManager.AddCard(card);
    }
}