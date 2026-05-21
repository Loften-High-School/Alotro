using UnityEngine;
using System.Collections;
using Unity.VisualScripting;
using System;

public class Hand : MonoBehaviour
{

    public PlayerGameInfo PGI; // Hand Size
    public Deck Deck; // Cards in Deck
    public HandManager handManager; // Hand Manager
    public RunGame runGame; // Run Game Script

    public float waitTime;

    public bool test = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (test)
        {
            StartCoroutine(TestDrawCard());
            test = false;
        }
    }

    public IEnumerator DrawCard()  
    {
        for (int i = 0; i < PGI.handSize; i++)
        {
            if (PGI.hand < PGI.handSize && (runGame.phase == 1 || runGame.phase == 2))
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
                    handManager.OutOfCards();
                    Debug.Log("<color=cyan>Check </color>Deck is empty");
                    yield break;
                }
                Debug.Log("<color=orange>Testing out of cards break </color>");
            }
            yield return new WaitForSeconds(waitTime);
        }
    }

    public IEnumerator TestDrawCard()
    {
        handManager.ClearHand();

        for (int i = 0; i < PGI.handSize; i++)
        {
            if (PGI.hand < PGI.handSize && (runGame.phase == 1 || runGame.phase == 2))
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
                    handManager.OutOfCards();
                    Debug.Log("<color=cyan>Check </color>Deck is empty");
                    yield break;
                }
            }
            yield return new WaitForSeconds(waitTime);
        }
    }
}
