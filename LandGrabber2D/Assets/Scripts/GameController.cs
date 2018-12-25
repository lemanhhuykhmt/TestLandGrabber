using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public List<GameObject> selected;
    public Vector3 target;
    public GraphPoint graph;
    // Use this for initialization

    void Start () {
        selected = new List<GameObject>();
        
    }
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonDown(1))
        {
            foreach(var house in selected)
            {
                //house.GetComponent<CastleController>().setAnimSelected(false);
                house.GetComponent<HouseController>().setAnimSelected(false);
            }
            selected.Clear();
        }
    }
    public void Attack()
    {
        if(selected.Count > 0 && !target.Equals(new Vector3(float.NaN, float.NaN, float.NaN)))
        { 
            //kiểm tra tất cả các nhà có thể đến đích k
            foreach(var myHouse in selected)
            {
                myHouse.GetComponent<HouseController>().setAnimSelected(false);
                if(!myHouse.transform.position.Equals(target))
                {
                    //myHouse.GetComponent<CastleController>().Attack(target);
                    myHouse.GetComponent<HouseController>().Attack(target);
                }
            }
            selected = new List<GameObject>();
            target = new Vector3(float.NaN, float.NaN, float.NaN);
        }
    }

    public void AddHouse(GameObject newHouse)
    {
        if(selected.Contains(newHouse))
        {
            return;
        }
        selected.Add(newHouse);
    }

    public bool HasConnected(Vector3 posEnd)
    {
        BigPoint end = graph.GetPointByPos(posEnd);
        if (end == null) return false;
        foreach(var item in selected)
        {
            BigPoint begin = graph.GetPointByPos(item.transform.position);
            if(graph.HasConnected(begin, end) == false)
            {
                return false;
            }
        }
        return true;
    }

}
