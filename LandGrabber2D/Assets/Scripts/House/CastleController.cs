
using UnityEngine;
using UnityEngine.UI;

public class CastleController : HouseController
{


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
        imgSprite.GetComponent<Image>().sprite = sp;

    }
}
