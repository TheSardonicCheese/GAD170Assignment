﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    
    public Stats myStats;
    // Start is called before the first frame update
    void Start()
    {
        myStats = GetComponent<Stats>();
    }

    
}
