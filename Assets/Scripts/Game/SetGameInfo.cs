using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SetGameInfo : MonoBehaviour
{

    public PlayerGameInfo PGI;

    public TMP_Text blindScore;
    public TMP_Text roundScore;
    public TMP_Text hand;
    public TMP_Text discard;
    public TMP_Text money;
    public TMP_Text ante;
    public TMP_Text round;

    public TMP_Text jokerSlots;
    public TMP_Text consumeSize;
    public TMP_Text handSize;
    public TMP_Text deckSize;

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
        hand.text = PGI.hands.ToString();
        discard.text = PGI.discards.ToString();
        money.text = PGI.money.ToString();
        ante.text = PGI.ante.ToString();
        round.text = PGI.round.ToString();

        jokerSlots.text = PGI.jokers + "/" + PGI.jokerSlots;
        consumeSize.text = PGI.consumeables + "/" + PGI.consumeSize;
        handSize.text = PGI.hand + "/" + PGI.handSize;
        deckSize.text = PGI.deck + "/" + PGI.deckSize;

        // Update Hand Info (Chips, Mult, and Current Hand) (not implemented yet)
    }
}
