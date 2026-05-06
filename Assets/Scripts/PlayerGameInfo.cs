using UnityEngine;

public class PlayerGameInfo : MonoBehaviour
{

    // All Game info
    public int hands;
    public int discards;
    public int handSize;
    public int deckSize;
    public int jokerSlots;
    public int consumeSize;
    public int ante;
    public int round;
    public int roundScore;
    public int money;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Deault Game Info
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
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
