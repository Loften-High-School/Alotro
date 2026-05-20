using UnityEngine;

public class ChangePhase : MonoBehaviour
{

    [Space]
    public RunGame RG;
    public PlayHand PH;
    public HandManager HM;
    public PlayerGameInfo PGI;

    [Header("Game Board")] [Space]
    // Central GameObjects
    public GameObject ChooseBlind; // WIP
    public GameObject BottomBar;
    public GameObject CashOut; // WIP
    public GameObject Shop; // WIP
    public GameObject Booster; // WIP
    
    [Header("Phase Info")] [Space]
    // Phase GameObjects
    public GameObject PhaseCB; // Phase Choose Blind
    public GameObject PhaseSB; // Phase Small Blind
    public GameObject PhaseBigB; // Phase Big Blind
    public GameObject PhaseBB; // Phase Play Boss Blind
    public GameObject PhaseS; // Phase Shop

    public GameObject ScoreNeeded;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ChangeGamePhase()
    {
        switch (RG.phase)
        {
            case 0: // Choose Blind Phase
                PhaseCB.SetActive(true);
                PhaseSB.SetActive(false);
                PhaseBigB.SetActive(false);
                PhaseBB.SetActive(false);
                PhaseS.SetActive(false);

                ScoreNeeded.SetActive(false);

                ChooseBlind.SetActive(true);
                BottomBar.SetActive(false);
                CashOut.SetActive(false);
                Shop.SetActive(false);
                Booster.SetActive(false);
                break;
            case 1: // Play Blind Phase
                PhaseCB.SetActive(false);
                PhaseBB.SetActive(false);
                PhaseS.SetActive(false);

                if (PGI.nextBlind == 1)
                {
                    PhaseSB.SetActive(true);
                    PhaseBigB.SetActive(false);
                }
                else if (PGI.nextBlind == 2)
                {
                    PhaseSB.SetActive(false);
                    PhaseBigB.SetActive(true);
                }

                ScoreNeeded.SetActive(true);

                ChooseBlind.SetActive(false);
                BottomBar.SetActive(true);
                CashOut.SetActive(false);
                Shop.SetActive(false);
                Booster.SetActive(false);
                break;
            case 2: // Play Boss Blind Phase
                PhaseCB.SetActive(false);
                PhaseSB.SetActive(false);
                PhaseBigB.SetActive(false);
                PhaseBB.SetActive(true);
                PhaseS.SetActive(false);

                ScoreNeeded.SetActive(true);

                ChooseBlind.SetActive(false);
                BottomBar.SetActive(true);
                CashOut.SetActive(false);
                Shop.SetActive(false);
                Booster.SetActive(false);
                break;
            case 3: // Cash Out Phase
                PhaseCB.SetActive(false);
                PhaseSB.SetActive(false);
                PhaseBigB.SetActive(false);
                PhaseBB.SetActive(false);
                PhaseS.SetActive(false);

                ScoreNeeded.SetActive(false);

                ChooseBlind.SetActive(false);
                BottomBar.SetActive(false);
                CashOut.SetActive(true);
                Shop.SetActive(false);
                Booster.SetActive(false);
                break;
            case 4: // Shop Phase
                PhaseCB.SetActive(false);
                PhaseSB.SetActive(false);
                PhaseBigB.SetActive(false);
                PhaseBB.SetActive(false);
                PhaseS.SetActive(true);

                ScoreNeeded.SetActive(false);

                ChooseBlind.SetActive(false);
                BottomBar.SetActive(false);
                CashOut.SetActive(false);
                Shop.SetActive(true);
                Booster.SetActive(false);
                break;
            case 5: // Booster Phase
                PhaseCB.SetActive(false);
                PhaseSB.SetActive(false);
                PhaseBigB.SetActive(false);
                PhaseBB.SetActive(false);
                PhaseS.SetActive(true);

                ScoreNeeded.SetActive(false);

                ChooseBlind.SetActive(false);
                BottomBar.SetActive(false);
                CashOut.SetActive(false);
                Shop.SetActive(false);
                Booster.SetActive(true);
                break;
        }
    }

    public void NextPhase(int phase)
    {
        RG.phase = phase;
        ChangeGamePhase();
    }
}
