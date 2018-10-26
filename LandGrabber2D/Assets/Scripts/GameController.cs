using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public List<GameObject> selected;
    public GameObject target;
    // Use this for initialization

    void Start () {
        selected = new List<GameObject>();
    }
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonDown(1))
        {
            selected.Clear();
        }
    }
    public void Attack()
    {
        if(selected.Count > 0 && target != null)
        { 
            //kiểm tra tất cả các nhà có thể đến đích k
            foreach(var myHouse in selected)
            {
                if(!myHouse.Equals(target))
                {
                    myHouse.GetComponent<CastleController>().Attack(target);
                }
            }
            selected = new List<GameObject>();
            target = null;
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
}
