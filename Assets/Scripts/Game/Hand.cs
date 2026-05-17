using UnityEngine;
using System.Collections;

public class Hand : MonoBehaviour
{

    public PlayerGameInfo PGI; // Hand Size
    public Deck Deck; // Cards in Deck
    public HandManager handManager; // Hand Manager

    public float waitTime = 0.3f;

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
        while (true) 
        {
            CheckHand();
            yield return new WaitForSeconds(waitTime); // Wait for 1 second
        }
    }

    void CheckHand()
    {
        if (PGI.hand < PGI.handSize)
        {
            DrawCard();
        }
    }

    void DrawCard()
    {
        PGI.hand += 1;
        
        CardData card = Deck.deck[0];
        Deck.deck.RemoveAt(0);

        handManager.AddCard(card);
    }
}
