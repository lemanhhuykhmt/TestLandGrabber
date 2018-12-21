using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphController : MonoBehaviour {

    // Use this for initialization
    public GameObject gameController;
    private void Awake()
    {
        gameController.GetComponent<GameController>().graph = new GraphPoint();
        int numberE = transform.childCount;
        List<GameObject> listWay = new List<GameObject>();
        foreach(Transform child in transform)
        {
            listWay.Add(child.gameObject);
        }
        for (int i = 0; i < numberE; ++i)
        {
            List<GameObject> listPoint = new List<GameObject>();
            foreach (Transform child in listWay[i].transform)
            {
                listPoint.Add(child.gameObject);
            }
            int numberPoint = transform.GetChild(i).childCount;
            WayPoint newWay = new WayPoint();
            for (int j = 0; j < numberPoint; ++j)
            {
                if (j == 0)
                {
                    newWay.Begin = new BigPoint() {
                        Position = transform.GetChild(i).GetChild(j).position,
                        Team = listPoint[j].GetComponent<BigPointController>().team};
                    if(!gameController.GetComponent<GameController>().graph.V.Contains(newWay.Begin))
                    {
                        gameController.GetComponent<GameController>().graph.V.Add(newWay.Begin);
                    }
                }
                else if(j == numberPoint - 1)
                {
                    newWay.End = new BigPoint() {
                        Position = transform.GetChild(i).GetChild(j).position,
                        Team = listPoint[j].GetComponent<BigPointController>().team
                    };
                    if (!gameController.GetComponent<GameController>().graph.V.Contains(newWay.End))
                    {
                        gameController.GetComponent<GameController>().graph.V.Add(newWay.End);
                    }
                }
                else
                {
                    newWay.ListPoints.Add(new LittlePoint() { Position = transform.GetChild(i).GetChild(j).position });
                }
            }
            gameController.GetComponent<GameController>().graph.AddE(newWay);
        }
       
    }
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
