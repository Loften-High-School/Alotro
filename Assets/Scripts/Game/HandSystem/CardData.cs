using UnityEngine;

[System.Serializable]
public class CardData
{
    public int value;
    public string suit;
    public Sprite sprite;

    public bool isSelected;
}

[System.Serializable]
public class JokerData
{
    public string name;
    public string description;
    public Sprite sprite;

    public bool isSelected;
}