using System.Collections.Generic;
using UnityEngine;

public abstract class JokerEffect : ScriptableObject
{
    public string effectName;

    // Called before scoring starts
    public virtual void OnHandPlayed(List<CardData> playedCards) {}

    // Called per card during scoring
    public virtual int ModifyChips(int baseChips, CardData card) => baseChips;

    public virtual int ModifyMult(int baseMult, CardData card) => baseMult;

    public virtual double ModifyXMult(double baseXMult, CardData card) => baseXMult;

    // Called after full hand resolves
    public virtual void OnHandScored() {}
}