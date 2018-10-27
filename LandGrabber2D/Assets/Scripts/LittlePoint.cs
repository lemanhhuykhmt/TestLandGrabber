using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LittlePoint {

    private Vector3 position;

    public Vector3 Position
    {
        get
        {
            return position;
        }

        set
        {
            position = value;
        }
    }

    public LittlePoint()
    {
        position = new Vector3();
    }
    public LittlePoint(Vector3 newPos)
    {
        position = newPos;
    }
}
