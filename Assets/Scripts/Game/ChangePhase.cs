using UnityEngine;

public class ChangePhase : MonoBehaviour
{

    [Space]
    public RunGame RG;
    public PlayHand PH;

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

    }

    public void ChangeGamePhase()
    {
        switch (RG.phase)
        {
            case 0: // Choose Blind Phase
                PhaseCB.SetActive(true);
                PhasePB.SetActive(false);
                PhaseBB.SetActive(false);
                PhaseS.SetActive(false);

                ChooseBlind.SetActive(true);
                BottomBar.SetActive(false);
                CashOut.SetActive(false);
                Shop.SetActive(false);
                Booster.SetActive(false);
                break;
            case 1: // Play Blind Phase
                PhaseCB.SetActive(false);
                PhasePB.SetActive(true);
                PhaseBB.SetActive(false);
                PhaseS.SetActive(false);

                ChooseBlind.SetActive(false);
                BottomBar.SetActive(true);
                CashOut.SetActive(false);
                Shop.SetActive(false);
                Booster.SetActive(false);
                break;
            case 2: // Play Boss Blind Phase
                PhaseCB.SetActive(false);
                PhasePB.SetActive(false);
                PhaseBB.SetActive(true);
                PhaseS.SetActive(false);

                ChooseBlind.SetActive(false);
                BottomBar.SetActive(true);
                CashOut.SetActive(false);
                Shop.SetActive(false);
                Booster.SetActive(false);
                break;
            case 3: // Cash Out Phase
                PhaseCB.SetActive(false);
                PhasePB.SetActive(false);
                PhaseBB.SetActive(false);
                PhaseS.SetActive(false);

                ChooseBlind.SetActive(false);
                BottomBar.SetActive(false);
                CashOut.SetActive(true);
                Shop.SetActive(false);
                Booster.SetActive(false);
                break;
            case 4: // Shop Phase
                PhaseCB.SetActive(false);
                PhasePB.SetActive(false);
                PhaseBB.SetActive(false);
                PhaseS.SetActive(true);

                ChooseBlind.SetActive(false);
                BottomBar.SetActive(false);
                CashOut.SetActive(false);
                Shop.SetActive(true);
                Booster.SetActive(false);
                break;
            case 5: // Booster Phase
                PhaseCB.SetActive(false);
                PhasePB.SetActive(false);
                PhaseBB.SetActive(false);
                PhaseS.SetActive(true);

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
