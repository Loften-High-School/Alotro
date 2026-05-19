using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using Unity.VisualScripting;

public class SetGameInfo : MonoBehaviour
{

    [Space]
    public PlayerGameInfo PGI;
    public CashOut CO;
    public HandManager HM;
    public RunGame RG;

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
    List<int> targetScore = new List<int> {300, 800, 2000, 5000, 11000, 20000, 35000, 100000};
    public double blindTarget;

    [Header("Choose Blind")] [Space]
    public TMP_Text smallBlindTXT;
    public TMP_Text bigBlindTXT;
    public TMP_Text bossBlindTXT;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        HM.UpdateLiveHandPreview();
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

        if (RG.phase == 0)
        {
            SelectBlindTXT();
        }
    }

    void GetBlindScore()
    {
        if (PGI.blindType == "Small Blind")
        {
            blindTarget = targetScore[PGI.ante - 1];
        }
        else if (PGI.blindType == "Big Blind")
        {
            blindTarget = targetScore[PGI.ante - 1] * 1.5;
        }
        else if (PGI.blindType == "Boss Blind")
        {
            blindTarget = targetScore[PGI.ante - 1] * 2;
        }

        PGI.blindScore = blindTarget;
    }

    void SelectBlindTXT()
    {
        smallBlindTXT.text = targetScore[PGI.ante - 1].ToString();
        bigBlindTXT.text = (targetScore[PGI.ante - 1] * 1.5).ToString();
        bossBlindTXT.text = (targetScore[PGI.ante - 1] * 2).ToString();
    }
}
