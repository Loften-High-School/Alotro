using UnityEngine;

public class PlayHand : MonoBehaviour
{

    [Space]
    public PlayerGameInfo PGI;
    public ChangePhase CP;
    public HandManager handManager;
    public Deck deckScript;

    [Space]
    public bool winRound;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (winRound)
        {
            handManager.ClearHand();
            Debug.Log("You Win the Round!");
            CP.NextPhase(3); // Cash Out Phase
            PGI.nextBlind ++;
            if (PGI.nextBlind == 4) // Loop back to small blind
            {
                PGI.nextBlind = 1;
                PGI.ante ++;
            }
            deckScript.BuildDeck();
            winRound = false;
        }
    }

    public void FindScore()
    {
        if (PGI.roundScore > PGI.blindScore)
        {
            winRound = true;
            Debug.Log("You Won");
        }
        else if (PGI.roundScore < PGI.blindScore && PGI.handsLeft == 0)
        {
            winRound = false;
            Debug.Log("You Lose the Round!");
        }
    }
}
