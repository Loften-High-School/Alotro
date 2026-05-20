using UnityEngine;
using System.Collections;

public class Hand : MonoBehaviour
{

    public PlayerGameInfo PGI; // Hand Size
    public Deck Deck; // Cards in Deck
    public HandManager handManager; // Hand Manager
    public RunGame runGame; // Run Game Script

    public float waitTime;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(RepeatProcedure());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator RepeatProcedure()  
    {
        for (int i = 0; i < PGI.handSize; i++)
        {
            CheckHand();
            yield return new WaitForSecondsRealtime(waitTime);
        }
    }

    void CheckHand()
    {
        if (PGI.hand < PGI.handSize && (runGame.phase == 1 || runGame.phase == 2))
        {
            DrawCard();
        }
    }

    void DrawCard()
    {
        if (Deck.deck.Count > 0)
        {
            PGI.hand += 1;
        
            CardData card = Deck.deck[0];
            Deck.deck.RemoveAt(0);

            handManager.AddCard(card);
        }
        else
        {
            Debug.Log("Deck is empty");
        }
    }
}
