using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPoint {
    private BigPoint begin;
    private BigPoint end;
    private List<LittlePoint> listPoints;

    public BigPoint End
    {
        get
        {
            return end;
        }

        set
        {
            end = value;
        }
    }

    public BigPoint Begin
    {
        get
        {
            return begin;
        }

        set
        {
            begin = value;
        }
    }

    public List<LittlePoint> ListPoints
    {
        get
        {
            return listPoints;
        }
    }

    public WayPoint()
    {
        listPoints = new List<LittlePoint>();
    }
}
