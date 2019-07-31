﻿using System.Collections;
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

    public bool isDefeated;

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

        if (satiety <= 0)
            isDefeated = true;
    }

}