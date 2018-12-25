using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DEFINE {
    public enum TEAM:int
    {
        NEUTRAL,
        RED,
        BLUE,
        GREEN,
        YELLOW,
        NONE
    }
    public enum DIRECTION:int
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
        float alpha = Vector2.Angle(new Vector2(1, 0), new Vector2(dir.x, dir.y));
        if (dir.y < 0)
        {
            alpha = 360 - alpha;
        }
        if (alpha <= 22.5 || alpha >= 15 * 22.5)
        {
            d = DIRECTION.East;
        }
        else if (alpha < 3 * 22.5 && alpha > 1 * 22.5)
        {
            d = DIRECTION.EastNorth;
        }
        else if (alpha <= 5 * 22.5 && alpha >= 3 * 22.5)
        {
            d = DIRECTION.North;
        }
        else if (alpha < 7 * 22.5 && alpha > 5 * 22.5)
        {
            d = DIRECTION.WestNorth;
        }
        else if (alpha <= 9 * 22.5 && alpha >= 7 * 22.5)
        {
            d = DIRECTION.West;
        }
        else if (alpha < 11 * 22.5 && alpha > 9 * 22.5)
        {
            d = DIRECTION.WestSouth;
        }
        else if (alpha <= 13 * 22.5 && alpha >= 11 * 22.5)
        {
            d = DIRECTION.South;
        }
        else if (alpha < 15 * 22.5 && alpha > 13 * 22.5)
        {
            d = DIRECTION.EastSouth;
        }
        return d;
    }
}
