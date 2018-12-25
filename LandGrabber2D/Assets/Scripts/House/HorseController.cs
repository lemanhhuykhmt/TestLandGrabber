
using UnityEngine;
using UnityEngine.UI;

public class HorseController : HouseController
{

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

    protected override void changeSkin()
    {
        string lv = "l" + level;
        string team = "p" + (int)Team;
        string name = "stable";
        string defaul = "s4";
        Sprite sp = Resources.Load<Sprite>("House/" + name + "_" + team + "_" + defaul + "_" + lv);
        imgSprite.GetComponent<Image>().sprite = sp;
    }
}
