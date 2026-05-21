using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "JokerDatabase", menuName = "Cards/JokerDatabase")]
public class JokerDatabase : ScriptableObject
{
    public List<JokerData> allJokers;

    public JokerData GetJoker(int index)
    {
        if (index >= 0 && index < allJokers.Count)
            return allJokers[index];
        return null;
    }
}
