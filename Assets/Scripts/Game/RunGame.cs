using UnityEngine;

public class RunGame : MonoBehaviour
{

    /* Game Phases
    0 = Select Blind
    1 = Play Normal Blind
    2 = Play Boss Blind
    3 = Cash Out
    4 = Shop
    5 = Booster Phase
    */
    public int phase = 0;

    // Central GameObjects
    public GameObject ChooseBlind; // WIP
    public GameObject BottomBar;
    public GameObject CashOut; // WIP
    public GameObject Shop; // WIP
    public GameObject Booster; // WIP
    
    // Phase GameObjects
    public GameObject PhaseCB; // Phase Choose Blind
    public GameObject PhasePB; // Phase Play Blind
    public GameObject PhaseBB; // Phase Play Boss Blind
    public GameObject PhaseS; // Phase Shop

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (phase == 0) // Choose Blind Phase
        {
            PhaseCB.SetActive(true);
            PhasePB.SetActive(false);
            PhaseBB.SetActive(false);
            PhaseS.SetActive(false);

            ChooseBlind.SetActive(true);
            BottomBar.SetActive(false);
            CashOut.SetActive(false);
            Shop.SetActive(false);
            Booster.SetActive(false);
        }
        else if (phase == 1) // Play Blind Phase
        {
            PhaseCB.SetActive(false);
            PhasePB.SetActive(true);
            PhaseBB.SetActive(false);
            PhaseS.SetActive(false);

            ChooseBlind.SetActive(false);
            BottomBar.SetActive(true);
            CashOut.SetActive(false);
            Shop.SetActive(false);
            Booster.SetActive(false);
        }
        else if (phase == 2) // Play Boss Blind Phase
        {
            PhaseCB.SetActive(false);
            PhasePB.SetActive(false);
            PhaseBB.SetActive(true);
            PhaseS.SetActive(false);

            ChooseBlind.SetActive(false);
            BottomBar.SetActive(true);
            CashOut.SetActive(false);
            Shop.SetActive(false);
            Booster.SetActive(false);
        }
        else if (phase == 3) // Cash Out Phase
        {
            PhaseCB.SetActive(false);
            PhasePB.SetActive(false);
            PhaseBB.SetActive(false);
            PhaseS.SetActive(false);

            ChooseBlind.SetActive(false);
            BottomBar.SetActive(false);
            CashOut.SetActive(true);
            Shop.SetActive(false);
            Booster.SetActive(false);
        }
        else if (phase == 4) // Shop Phase
        {
            PhaseCB.SetActive(false);
            PhasePB.SetActive(false);
            PhaseBB.SetActive(false);
            PhaseS.SetActive(true);

            ChooseBlind.SetActive(false);
            BottomBar.SetActive(false);
            CashOut.SetActive(false);
            Shop.SetActive(true);
            Booster.SetActive(false);
        }
        else if (phase == 5) // Booster Phase
        {
            PhaseCB.SetActive(false);
            PhasePB.SetActive(false);
            PhaseBB.SetActive(false);
            PhaseS.SetActive(true);

            ChooseBlind.SetActive(false);
            BottomBar.SetActive(false);
            CashOut.SetActive(false);
            Shop.SetActive(false);
            Booster.SetActive(true);
        }
    }
}
