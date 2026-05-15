using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class SetGameInfo : MonoBehaviour
{

    [Space]
    public PlayerGameInfo PGI;
    public CashOut CO;

    [Header("Game Info")] [Space]
    public TMP_Text blindScore;
    public TMP_Text roundScore;
    public TMP_Text hand;
    public TMP_Text discard;
    public TMP_Text money;
    public TMP_Text ante;
    public TMP_Text round;

    [Header("Hand Info")] [Space]
    // public TMP_Text currentHand; // not implemented yet
    public TMP_Text chips;
    public TMP_Text mult;

    [Header("Playing Cards")] [Space]
    public TMP_Text jokerSlots;
    public TMP_Text consumeSize;
    public TMP_Text handSize;
    public TMP_Text deckSize;

    [Header("Other")] [Space]
    public TMP_Text cashOut;

    [Header("Variables"), Space]
    public List<int> targetScore = new List<int> {300, 600, 2000, 6000, 11000, 25000, 50000, 100000};
    public double blindTarget;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Update Game Info
        blindScore.text = PGI.blindScore.ToString();
        roundScore.text = PGI.roundScore.ToString();
        hand.text = PGI.handsLeft.ToString();
        discard.text = PGI.discardsLeft.ToString();
        money.text = "$" +PGI.money;
        ante.text = PGI.ante.ToString();
        round.text = PGI.round.ToString();

        // Update Hand Info (Chips, Mult, and Current Hand) (not implemented yet)
        chips.text = PGI.chips.ToString();
        mult.text = PGI.mult.ToString();

        jokerSlots.text = PGI.jokers + "/" + PGI.jokerSlots;
        consumeSize.text = PGI.consumeables + "/" + PGI.consumeSize;
        handSize.text = PGI.hand + "/" + PGI.handSize;
        deckSize.text = PGI.deck + "/" + PGI.deckSize;

        cashOut.text = "Cash Out: $" + (CO.blindBonus + PGI.handsLeft);

        GetBlindScore();
    }

    void GetBlindScore()
    {
        if (PGI.blindType == "Small Blind")
        {
            blindTarget = targetScore[(PGI.ante - 1)];
        }
        else if (PGI.blindType == "Big Blind")
        {
            blindTarget = targetScore[(PGI.ante - 1)] * 1.5;
        }
        else if (PGI.blindType == "Boss Blind")
        {
            blindTarget = targetScore[(PGI.ante - 1)] * 2;
        }

        PGI.blindScore = blindTarget;
    }
}
