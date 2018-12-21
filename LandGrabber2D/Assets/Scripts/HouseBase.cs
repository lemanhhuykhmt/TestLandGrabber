using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class HouseBase{
    private DEFINE.TEAM team;
    private int curSoldier;
    private int maxSoldier;
    private float curHealth;
    private int level = 1;
    private int maxLevel;
    private float populationRating;

    public DEFINE.TEAM Team
    {
        set
        {
            team = value;
        }
        get
        {
            return team;
        }
    }
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
    public float PopulationRating
    {
        set
        {
            populationRating = value;

        }
        get
        {
            return populationRating;
        }
    }
    public HouseBase()
    {

    }

    public void ReceiveDamege(float damege)
    {
        CurHealth = CurHealth - damege;
        //updateCurSoldier();
    }
    public void AddSoidier()
    {
        CurSoldier++;
        //updateHealth();
    }
    public void UpgradeLevel()
    {
        if (level >= maxLevel)
        {
            return;
        }
        Level++;
        CurSoldier = curSoldier / 2;
        //updateHealth();
        changeSkin();
    }
    public abstract void ChangeTeam(DEFINE.TEAM team);
    protected abstract void changeSkin();

}
