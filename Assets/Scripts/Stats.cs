using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    public float maxSatiety;
    public float satiety;
    //treated as health
    public int metabolism;
    //treated as speed
    public int hunger;
    //treated as attack
    public int rawness;
    //treated as defense
    public int dexterity;
    //treated as accuracy/evasion
    public int luck;
    //treated as luck
    public int currentLevel;
    public float totalXP;
    public float xpRequired;
    public float xpGained;

    public bool isDefeated;
    public bool isSpicy = false;
    public bool isSalty = false;
    public bool isSweet = false;
    public bool isSavoury = false;
    public bool isBitter = false;


    public enum StatusEffect
    {
        none,
        spicy,
        //damage over time
        salty,
        //reduce rawness
        sweet,
        //reduces dexterity
        savoury,
        //reduce hunger
        bitter,
        //reduce luck
    }

    public StatusEffect myStatus;
    public StatusEffect attackEffect;

    public void Attacked(int incDmg, Stats.StatusEffect incEffect, int atkrDex, int atkrLuck)
    {
        //(attacker dex/ defender dex) * attacker luck
       
        if (Random.Range(1,100) <= ((atkrDex / dexterity) * atkrLuck ))
        {
            satiety -= incDmg - (incDmg * (100 / (rawness + 100)));
            myStatus = incEffect;
        }
        else
        {
            Debug.Log(" The attack missed!");
        }
        switch (myStatus)
        {
            case StatusEffect.spicy:
                isSpicy = true;
                SeasoningEffect();
                break;
            case StatusEffect.salty:
                isSalty = true;
                SeasoningEffect();
                break;
            case StatusEffect.sweet:
                isSweet = true;
                SeasoningEffect();
                break;
            case StatusEffect.savoury:
                isSavoury = true;
                SeasoningEffect();
                break;
            case StatusEffect.bitter:
                isBitter = true;
                SeasoningEffect();
                break;
        }
        if (satiety <= 0)
            isDefeated = true;
    }

    public void Seasoned(Stats.StatusEffect incEffect)
    {
        myStatus = incEffect;
    }
    public void SeasoningEffect()
    {
        if(isSpicy == true)
        {
            satiety -= 20;
        }
        if(isSalty == true)
        {
            rawness = rawness / 2;
        }
        if(isSweet == true)
        {
            dexterity = dexterity / 2;
        }
        if(isSavoury == true)
        {
            hunger = hunger / 2;
        }
        if(isBitter == true)
        {
            luck = luck / 2;
        }
    }
    void XPandLvlUp()
    {
        if (currentLevel >= 5)
        {
            xpRequired = currentLevel * currentLevel + 3;
        }
        else
        {
            xpRequired = currentLevel * 3 + 4;
        }

        if (totalXP >= xpRequired)
        {
            currentLevel++;
            maxSatiety += 20;
            satiety = maxSatiety;
            metabolism += 10;
            hunger += 10;
            rawness += 10;
            luck += 3;
            Debug.Log("Level Up!");
        }
    }

    public void GainXPGnoc()
    {
        totalXP += 1f / (.5f * currentLevel);
        XPandLvlUp();

    }
    public void GainXPRav()
    {
        totalXP += 5f / (.5f * currentLevel);
        XPandLvlUp();
    }
    public void GainXPMon()
    {
        totalXP += 10f / (.5f * currentLevel);
        XPandLvlUp();
    }

}