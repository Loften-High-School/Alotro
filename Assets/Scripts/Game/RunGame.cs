using UnityEditor.EditorTools;
using UnityEngine;

public class RunGame : MonoBehaviour
{

    [Space]
    public ChangePhase CP;
    //public PlayHand PH;

    /* Game Phases
    0 = Select Blind
    1 = Play Normal Blind
    2 = Play Boss Blind
    3 = Cash Out
    4 = Shop
    5 = Booster Phase
    6 = Win Game (not implemented yet)
    7 = End Game (not implemented yet)
    */
    [Space] [Tooltip("0 = Select Blind\n1 = Play Normal Blind\n2 = Play Boss Blind\n3 = Cash Out\n4 = Shop\n5 = Booster Phase\n6 = Win Game (not implemented yet)\n7 = End Game (not implemented yet)")]
    public int phase = 1;

   
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CP.ChangeGamePhase();
    }
}
