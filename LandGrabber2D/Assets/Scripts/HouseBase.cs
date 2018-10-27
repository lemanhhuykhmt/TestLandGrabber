using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class HouseBase {
    private DEFINE.TEAM Team = DEFINE.TEAM.NEUTRAL;
    private int curSoldier = 30;
    private int maxSoldier = 40;
    private float curHealth = 60;
    private int level = 1;
    private int maxLevel;

    public int CurSoldier
    {
        get
        {
            return curSoldier;
        }

        set
        {
            curSoldier = value;
        }
    }
    public int MaxSoldier
    {
        get
        {
            return maxSoldier;
        }

        set
        {
            maxSoldier = value;
        }
    }
    public float CurHealth
    {
        get
        {
            return curHealth;
        }

        set
        {
            curHealth = value;
        }
    }
    public int Level
    {
        get
        {
            return level;
        }

        set
        {
            level = value;
        }
    }
    public int MaxLevel
    {
        get
        {
            return maxLevel;
        }

        set
        {
            maxLevel = value;
        }
    }
    public HouseBase()
    {

    }
    
     
}
