using UnityEngine;

public class PlayerGameInfo : MonoBehaviour
{

    [Space] [Header("Game Info")] [Space]
    // All Game info
    public int hands;
    public int discards;
    public int ante;
    public int round;
    public int roundScore; // Your Score
    public int blindScore; // The Score you need to beat to win the round
    public int money;

    [Header("Hand Info")] [Space]
    // public string currentHand; // not implemented yet
    public int chips;
    public int mult;

    [Header("Playing Cards")] [Space]
    public int handSize;
    public int hand;
    public int deckSize;
    public int deck;
    public int jokerSlots;
    public int jokers;
    public int consumeSize;
    public int consumeables;

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
        
    }
}
