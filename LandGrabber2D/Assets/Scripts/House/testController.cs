using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testController : HouseController {


    // override
    protected override void UpdateHealth()
    {
        curHealth = curHealth + 2 * (curSoldier - (int)(curHealth / 2 + 0.5));
        imgHealthCur.fillAmount = curHealth / maxSoldier / 2;
    }
    protected override void UpdateSoldier()
    {
        curSoldier = (int)(curHealth / 2 + 0.5);
        imgHealthCur.fillAmount = curHealth / maxSoldier / 2;
    }
    protected override void UpdateLevel()
    {
        maxSoldier = level * 10;
    }

    protected override void changeSkin()
    {
        string lv = "l" + level;
        string team = "p" + (int)Team;
        string name = "castle";
        string defaul = "s4";
        Sprite sp = Resources.Load<Sprite>("House/" + name + "_" + team + "_" + defaul + "_" + lv);
        GetComponent<SpriteRenderer>().sprite = sp;

    }
}
