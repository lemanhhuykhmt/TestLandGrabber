using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphPoint {
    private List<BigPoint> v;
    private List<WayPoint> e;

    public List<BigPoint> V
    {
        get
        {
            return v;
        }
    }

    public List<WayPoint> E
    {
        get
        {
            return e;
        }
    }


    public GraphPoint()
    {
        e = new List<WayPoint>();
        v = new List<BigPoint>();
    }
    public void AddE(WayPoint w)
    {
        WayPoint iWay = new WayPoint();
        iWay.Begin = w.End;
        iWay.End = w.Begin;
        foreach(var item in w.ListPoints)
        {
            iWay.ListPoints.Add(item);
        }
        iWay.ListPoints.Reverse();
        e.Add(iWay);
        e.Add(w);
    }
    public WayPoint GetAWay(BigPoint begin, BigPoint end)
    {
        foreach(var item in e)
        {
            if(item.Begin.Equals(begin) && item.End.Equals(end))
            {
                return item;
            }
        }
        return null;
    }
}
