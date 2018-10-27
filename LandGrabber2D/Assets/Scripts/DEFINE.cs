using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DEFINE {
    public enum TEAM:int
    {
        NEUTRAL,
        MYTEAM,
        BLUE,
        GREEN,
        YELLOW,
        NONE
    }
    public enum DIRECTION
    {
        None,
        East,
        West,
        North,
        South,
        EastNorth,
        WestNorth,
        EastSouth,
        WestSouth
    }

    public static DIRECTION GetDir(Vector3 dir)
    {
        dir.z = 1;
        DIRECTION d = DIRECTION.None;
        float alpha = Vector3.Angle(new Vector3(1, 0, 1), dir);

        float n = alpha / (Mathf.PI / 8);
        if(n >= 15 || n <= 1)
        {
            d = DIRECTION.East;
        }
        else if(n > 1 && n < 3)
        {
            d = DIRECTION.EastNorth;

        }
        else if (n >= 3 && n <= 5)
        {
            d = DIRECTION.North;

        }
        else if (n > 5 && n < 7)
        {
            d = DIRECTION.WestNorth;
            
        }
        else if (n >= 7 && n <= 9)
        {
            d = DIRECTION.West;

        }
        else if (n > 9 && n < 11)
        {
            d = DIRECTION.WestSouth;

        }
        else if (n >= 11 && n <= 13)
        {
            d = DIRECTION.South;

        }
        else if (n > 13 && n < 15)
        {
            d = DIRECTION.EastSouth;

        }
        return d;
    }
}
