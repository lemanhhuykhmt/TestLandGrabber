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
    public BigPoint GetPointByPos(Vector3 pos)
    {
        foreach(var item in v)
        {
            if(item.Position.Equals(pos))
            {
                return item;
            }
        }
        return null;
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
    public int GetDistance(BigPoint begin, BigPoint end)
    {
        if(begin.Equals(end))
        {
            return 0;
        }
        int distance = int.MaxValue;
        WayPoint way = GetAWay(begin, end);
        if(way != null)
        {
            distance = way.GetLen();
        }
        return distance;
    }
    public List<BigPoint> GetNeighbourhood(BigPoint point)
    {
        List<BigPoint> neigh = new List<BigPoint>();
        foreach(var item in e)
        {
            if(point.Equals(item.Begin))
            {
                neigh.Add(GetPointByPos(item.End.Position));
            }
        }
        return neigh;
    }
    public bool HasConnected(BigPoint begin, BigPoint end)
    {
        List<BigPoint> seen = new List<BigPoint>();
        Queue<BigPoint> q = new Queue<BigPoint>();
        q.Enqueue(begin);
        while(q.Count != 0)
        {
            BigPoint cur = q.Dequeue();
            seen.Add(cur);
            if (cur.Equals(end))
            {
                return true;
            }
            if (cur.Team != begin.Team && cur.Team != DEFINE.TEAM.NONE)
            {
                continue;
            }
            List<BigPoint> neigh = GetNeighbourhood(cur);
            foreach(var n in neigh)
            {
                if(!seen.Contains(n))// && (cur.Team == n.Team || n.Team == DEFINE.TEAM.NEUTRAL)
                {
                    q.Enqueue(n);
                }
            }
        }
        return false;
    }
    public List<WayPoint> Dijkstra(BigPoint s, BigPoint e)
    {
        Hashtable d = new Hashtable(); // d[v] = khoang cach tu v den s
        List<WayPoint> ways = new List<WayPoint>(); // cac quang duong ngan nhat
        List<BigPoint> notUsed = new List<BigPoint>(); // danh sach dinh chua su dung(chua ngan nhat)
        Hashtable previous = new Hashtable(); // preious[x] = dinh truoc x
        // khoi tao
        foreach (var i in v)
        {
            d.Add(i, GetDistance(s, i)); // khoi tao khoang cach min tu s den cac dinh
            
            if (i.Team == s.Team || i.Team == DEFINE.TEAM.NONE || i.Equals(e))
            {
                notUsed.Add(i);
                previous.Add(i, s);
            }
        }
        notUsed.Remove(s); 
        // tim duong di
        while(notUsed.Contains(e))// khi chua toi dich
        {
            // tim dinh co khoang cach ngan nhat hien tai
            BigPoint u = notUsed[0];
            foreach(var z in notUsed)
            {
                if (z.Team != s.Team && z.Team != DEFINE.TEAM.NONE && !z.Equals(e))
                {
                    continue;
                }
                if (int.Parse(d[z].ToString()) < int.Parse(d[u].ToString()))
                {
                    u = z;
                }
            }
            notUsed.Remove(u);
            List<BigPoint> neigh = GetNeighbourhood(u);
            foreach(var n in neigh)
            {
                if(int.Parse(d[n].ToString()) > (int.Parse(d[u].ToString()) + GetDistance(u, n)))
                {
                    d[n] = int.Parse(d[u].ToString()) + GetDistance(u, n);
                   // previous.Add(n, u);
                    previous[n] = u;
                }
            }
        }
        // tinh toan duong di
        BigPoint b = e;
        BigPoint a = previous[e] as BigPoint;
        ways.Add(GetAWay(a, b));
        while(!a.Equals(s))
        {
            a = previous[a] as BigPoint;
            b = previous[b] as BigPoint;
            ways.Add(GetAWay(a, b));
        }
        ways.Reverse();
        return ways;
    }
}
