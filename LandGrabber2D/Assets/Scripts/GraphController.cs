using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphController : MonoBehaviour {

    public static GraphPoint graph;
    // Use this for initialization
    private void Awake()
    {
        graph = new GraphPoint();
        int numberE = transform.childCount;
        for (int i = 0; i < numberE; ++i)
        {
            int numberPoint = transform.GetChild(i).childCount;
            WayPoint newWay = new WayPoint();
            for (int j = 0; j < numberPoint; ++j)
            {
                if(j == 0)
                {
                    newWay.Begin = new BigPoint() { Position = transform.GetChild(i).GetChild(j).position};
                    if(!graph.V.Contains(newWay.Begin))
                    {
                        graph.V.Add(newWay.Begin);
                    }
                }
                else if(j == numberPoint - 1)
                {
                    newWay.End = new BigPoint() { Position = transform.GetChild(i).GetChild(j).position };
                    if (!graph.V.Contains(newWay.End))
                    {
                        graph.V.Add(newWay.End);
                    }
                }
                else
                {
                    newWay.ListPoints.Add(new LittlePoint() { Position = transform.GetChild(i).GetChild(j).position });
                }
            }
            graph.AddE(newWay);


        }
        print("ato");
    }
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
