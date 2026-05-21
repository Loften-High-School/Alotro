using NUnit.Framework;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerGameInfo : MonoBehaviour
{

    [Space] [Header("References")] [Space]
    public HandManager HandManager;
    public Deck deckScript;
    
    [Space] [Header("Game Info")] [Space]
    // All Game info
    public int handsLeft;
    public int discardsLeft;
    public int ante;
    public int round;
    public double roundScore; // Your Score
    public double blindScore; // The Score you need to beat to win the round
    public string blindType; // Small Blind, Big Blind, Boss Blind
    public int nextBlind;
    public int money;

    [Header("Hand Info")] [Space]
    // public string currentHand; // not implemented yet
    public double chips;
    public double mult;

    [Header("Playing Cards")] [Space]
    public int handSize;
    public int hand;
    public int deckSize;
    public int deck;
    public int jokerSlots;
    public int jokers;
    public int consumeSize;
    public int consumeables;

    [Header("Covers")] [Space]
    public GameObject smallBlindCover;
    public GameObject bigBlindCover;
    public GameObject bossBlindCover;
    
    [Space]
    public Button playButton;
    public Button discardButton;
    public GameObject playCover;
    public GameObject discardCover;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Deault Game Info
        /*
        hands = 4;
        discards = 3;
        handSize = 8;
        deckSize = 52;
        jokerSlots = 5;
        consumeSize = 2;
        ante = 1;
        round = 1;
        roundScore = 0;
        money = 4;
        */
    }

    // Update is called once per frame
    void Update()
    {
        if (blindType == "Small Blind")
        {
            smallBlindCover.SetActive(false);
            bigBlindCover.SetActive(true);
            bossBlindCover.SetActive(true);
        }
        else if (blindType == "Big Blind")
        {
            smallBlindCover.SetActive(true);
            bigBlindCover.SetActive(false);
            bossBlindCover.SetActive(true);
        }
        else if (blindType == "Boss Blind")
        {
            smallBlindCover.SetActive(true);
            bigBlindCover.SetActive(true);
            bossBlindCover.SetActive(false);
        }

        if (nextBlind == 1)
        {
            blindType = "Small Blind";
        }
        else if (nextBlind == 2)
        {
            blindType = "Big Blind";
        }
        else if (nextBlind == 3)
        {
            blindType = "Boss Blind";
        }

        if (discardsLeft == 0 || !HasSelectedCards()) // Button inactive
        {
            discardCover.SetActive(true);
            discardButton.interactable = false;
            discardButton.image.color = new Color(210f / 255f, 210f / 255f, 210f / 255f, 255f / 255f); // Gray out the button #D2D2D2
        }
        else // Button active
        {
            discardCover.SetActive(false);
            discardButton.interactable = true;
            discardButton.image.color = new Color(236f / 255f, 83f / 255f, 83f / 255f, 255f / 255f); // Reset button color #EC5353
        }

        if (!HasSelectedCards()) // Button inactive
        {
            playCover.SetActive(true);
            playButton.interactable = false;
            playButton.image.color = new Color(210f / 255f, 210f / 255f, 210f / 255f, 255f / 255f); // Gray out the button #D2D2D2
        }
        else // Button active
        {
            playCover.SetActive(false);
            playButton.interactable = true;
            playButton.image.color = new Color(0f / 255f, 147f / 255f, 255f / 255f, 255f / 255f); // Reset button color #0093FF
        }
    }

    public void SetBlind(string type)
    {
        blindType = type;
        round ++;
        HandManager.Invoke(nameof(HandManager.DrawStartingHand), 1f);
    }

    public void SkipBlind(int next)
    {
        nextBlind = next;
    }

    bool HasSelectedCards()
    {
        foreach (CardData card in HandManager.hand)
        {
            if (card.isSelected)
                return true;
        }
        return false;
    }
}
