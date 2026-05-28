using UnityEngine;

[CreateAssetMenu(fileName = "Joker", menuName = "Jokers/Joker Data")]
public class JokerData : ScriptableObject
{
    public string jokerName;
    public string description;
    public Sprite artwork;
    public int price;
    public string rarity;

    public float value;

    public JokerType type;

    public bool isSelected;
}