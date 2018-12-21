using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigPoint : LittlePoint{

    private DEFINE.TEAM team;

    public DEFINE.TEAM Team
    {
        get
        {
            return team;
        }

        set
        {
            team = value;
        }
    }

    public bool Equals(BigPoint obj)
    {
        if(Position.x != obj.Position.x)
        {
            return false;
        }
        if (Position.y != obj.Position.y)
        {
            return false;
        }
        if (Position.z != obj.Position.z)
        {
            return false;
        }
        return true;
    }

    public override bool Equals(object obj)
    {
        BigPoint b = (BigPoint)obj;
        if (Position.x != b.Position.x)
        {
            return false;
        }
        if (Position.y != b.Position.y)
        {
            return false;
        }
        if (Position.z != b.Position.z)
        {
            return false;
        }
        return true;
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

}
