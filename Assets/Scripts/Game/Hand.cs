using UnityEngine;
using System.Collections;

public class Hand : MonoBehaviour
{

    public PlayerGameInfo PGI; // Hand Size
    public Deck Deck; // Cards in Deck

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

    IEnumerator RepeatProcedure()  
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
    }
}
