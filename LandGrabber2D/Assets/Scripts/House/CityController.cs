
using UnityEngine;
using UnityEngine.UI;

public class CityController : HouseController {

    protected override void changeSkin()
    {
        string lv = "l" + level;
        string team = "p" + (int)Team;
        string name = "city";
        string defaul = "s4";
        //print(name + "_" + team + "_" + defaul + "_" + lv);castle_p0_s4_l1
        Sprite sp = Resources.Load<Sprite>("House/" + name + "_" + team + "_" + defaul + "_" + lv);
        imgSprite.GetComponent<Image>().sprite = sp;
    }
    protected override void UpdateHealth()
    {
        curHealth = curSoldier;
        imgHealthCur.fillAmount = curHealth / maxSoldier;
    }
    protected override void UpdateSoldier()
    {
        curSoldier = (int)(curHealth);
        imgHealthCur.fillAmount = curHealth / maxSoldier;
    }
    protected override void UpdateLevel()
    {
        maxSoldier = level * 10;
    }
}
