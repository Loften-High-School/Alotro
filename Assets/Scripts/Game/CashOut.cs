using UnityEngine;
using TMPro;

public class CashOut : MonoBehaviour
{

    [Space]
    public PlayerGameInfo PGI;
    public Shop shop;

    [Space]
    public int blindBonus;

    [Header("Text")] [Space]
    public TMP_Text neededScoreText;
    public TMP_Text handsText;

    public TMP_Text blindBonusText;
    public TMP_Text handsBonusText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (PGI.blindType == "Small Blind")
        {
            blindBonus = 3;
        }
        else if (PGI.blindType == "Big Blind")
        {
            blindBonus = 4;
        }
        else if (PGI.blindType == "Boss Blind")
        {
            blindBonus = 5;
        }

        neededScoreText.text = PGI.blindScore.ToString();
        handsText.text = PGI.handsLeft.ToString();
        
        for (int i = 0; i < PGI.handsLeft; i++)
        {
            if (handsBonusText.text.Length < PGI.handsLeft)
            {
                handsBonusText.text += "$";
            }
        }

        for (int i = 0; i < blindBonus; i++)
        {
            if (blindBonusText.text.Length < blindBonus)
            {
                blindBonusText.text += "$";
            }
        }
    }

    public void CashOutRound()
    {
        PGI.money += blindBonus + PGI.handsLeft;
        shop.rerollCost = 5;
        Debug.Log($"<color=yellow>Values: </color>Cashed Out! Current Money: $<color=yellow>{PGI.money}</color>");
    }
}
