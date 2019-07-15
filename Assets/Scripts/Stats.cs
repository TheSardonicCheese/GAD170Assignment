using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    public int satiety;
    //treated as health
    public int metabolism;
    //treated as speed
    public int hunger;
    //treated as attack
    public int rawness;
    //treated as defense
    public int luck;
    //treated as luck

    public bool isDefeated;

    public enum StatusEffect
    {
        none,
        spicy,
        //like a burned effect, not sure about the rest yet
        salty,
        sweet,
        savoury,
        bitter,

    }

    public StatusEffect myStatus;
    public StatusEffect attackEffect;

    public void Attacked(int incDmg, Stats.StatusEffect incEffect)
    {
        satiety -= incDmg - (incDmg * (100 / (rawness + 100)));
        myStatus = incEffect;
        if (satiety <= 0)
            isDefeated = true;
    }

}