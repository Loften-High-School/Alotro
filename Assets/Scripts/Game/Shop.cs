using UnityEngine;

public class Shop : MonoBehaviour
{

    [Space]
    public PlayerGameInfo PGI;

    [Space]
    public int rerollCost = 5;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Reroll()
    {
        if (PGI.money >= rerollCost)
        {
            PGI.money -= rerollCost;
            Debug.Log("Rerolled Shop!");
        }
        else
        {
            Debug.Log("Not enough money to reroll!");
        }
    }
}
