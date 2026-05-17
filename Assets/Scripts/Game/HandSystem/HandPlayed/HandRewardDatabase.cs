using System.Collections.Generic;
using UnityEngine;

public class HandRewardDatabase : MonoBehaviour
{
    public List<HandReward> rewards = new List<HandReward>();

    public HandReward GetReward(HandRank rank)
    {
        return rewards.Find(r => r.handRank == rank);
    }
}