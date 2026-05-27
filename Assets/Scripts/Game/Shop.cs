using UnityEngine;

public class Shop : MonoBehaviour
{

    [Space]
    public PlayerGameInfo PGI;

    [Space]
    public int rerollCost;

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
            rerollCost += 1;
            Debug.Log("Rerolled Shop!");
        }
        else
        {
            Debug.Log("Not enough money to reroll!");
        }
    }
}
