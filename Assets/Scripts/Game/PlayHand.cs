using UnityEngine;

public class PlayHand : MonoBehaviour
{

    [Space]
    public PlayerGameInfo PGI;

    [Space]
    public bool winRound;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FindScore()
    {
        PGI.roundScore = PGI.chips * PGI.mult;

        if (PGI.roundScore > PGI.blindScore)
        {
            winRound = true;
            Debug.Log("You Win the Round!");
        }
        else
        {
            winRound = false;
            Debug.Log("You Lose the Round!");
        }
    }
}
