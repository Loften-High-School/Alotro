using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "JokerDatabase", menuName = "Cards/JokerDatabase")]
public class JokerDatabase : ScriptableObject
{
    public List<JokerData> jokers;

    public JokerData GetJoker(int index)
    {
        if (index < 0 || index >= jokers.Count)
        {
            Debug.LogWarning($"Invalid Joker index: {index}");
            return null;
        }

        return jokers[index];
    }
}
