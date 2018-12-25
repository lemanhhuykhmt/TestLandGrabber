using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigPointController : MonoBehaviour {
    public DEFINE.TEAM team;
    // Use this for initialization
    void Awake() {
        GameObject[] listHouse = GameObject.FindGameObjectsWithTag("House");
        foreach(var house in listHouse)
        {
            if(house.transform.position.x == this.transform.position.x && house.transform.position.y == this.transform.position.y)
            {
                team = house.GetComponent<HouseController>().Team;
                return;
            }
        }
        team = DEFINE.TEAM.NONE;
	}
	
}
