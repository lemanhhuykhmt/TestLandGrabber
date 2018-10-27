using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CastleController : MonoBehaviour
{
    private GameObject gameController;
    public GameObject soldier;
    public DEFINE.TEAM Team = DEFINE.TEAM.NEUTRAL;
    public int curSoldier = 30;
    public int maxSoldier = 40;
    public float curHealth = 60;
    public float CurHealth
    {
        get
        {
            return curHealth;
        }
        set
        {
            curHealth = value;
        }
    }
    public int level = 1;
    private int maxLevel = 5;
    int clicked = 0;
    float clickdelay = 0.3f;
    [Header("Unity Stuff")]
    public Image imgHealthCur;
    public Button btnUpgrade;
    public Image imgSelected;
    public Image imgMouseEnter;


    
    // Use this for initialization
    void Start()
    {
        setMouseEnter(false);
        setAnimSelected(false);
        gameController = GameObject.FindGameObjectWithTag("GameController");
        imgHealthCur.fillAmount = curHealth / maxSoldier / 2;
        btnUpgrade.gameObject.SetActive(false);
        showUpgradeButton();
    }
    
    // Update is called once per frame
    void Update()
    {
    }
    IEnumerator doubleClick()
    {

        yield return new WaitForSeconds(clickdelay);
        if(clicked == 1)
        {
            if (Team == DEFINE.TEAM.MYTEAM)
            {
                print("selected myteam");
                gameController.GetComponent<GameController>().AddHouse(gameObject);
                setAnimSelected(true);
            }
            else
            {
                if (gameController.GetComponent<GameController>().selected.Count == 0)
                {

                }
                else
                {
                    print("selected target");
                    gameController.GetComponent<GameController>().target = gameObject;
                    gameController.GetComponent<GameController>().Attack();
                }
            }
        }
        else if (clicked > 1)
        {
            gameController.GetComponent<GameController>().target = gameObject;
            gameController.GetComponent<GameController>().Attack();
        }
        yield return new WaitForSeconds(0.05f);
        clicked = 0;
    }
    void OnMouseDown()
    {
        clicked++;
        if(clicked == 1)
        {
            StartCoroutine(doubleClick());
        }
        
    }
    private void OnMouseEnter()
    {
        setMouseEnter(true);
    }
    private void OnMouseExit()
    {
        setMouseEnter(false);
    }
    public bool HasConnect()
    {
        return true;
    }

    public void Attack(GameObject target)
    {
        // lấy waypoint
        // tính lượng lính sinh
        setAnimSelected(false);
        StartCoroutine(spoil(target));
    }
    IEnumerator spoil(GameObject target)
    {
        int solidierOut = curSoldier / 2;
        if (curSoldier == 1)
        {
            solidierOut = 1;
        }
        curSoldier = curSoldier - solidierOut;
        updateHealth();
        for (int i = 0; i < solidierOut; ++i)
        {
            // spoi lính
            yield return new WaitForSecondsRealtime(0.05f);
            GameObject soil = Instantiate(soldier, transform.position, Quaternion.identity);
            soil.GetComponent<SoldierController>().endTarget = target.transform;
            soil.GetComponent<SoldierController>().team = Team;
        }
        
    }
    public void ReceiveDamege(float damege)
    {
        CurHealth = CurHealth - damege;
        updateCurSoldier();
    }
    public void AddSoidier()
    {
        curSoldier++;
        updateHealth();
    }
    private void updateHealth()
    {
        curHealth = curHealth + 2 * (curSoldier - (int)(curHealth / 2 + 0.5));
        imgHealthCur.fillAmount = curHealth / maxSoldier / 2;
        showUpgradeButton();
    }
    private void updateCurSoldier()
    {
        curSoldier = (int)(CurHealth / 2 + 0.5);
        imgHealthCur.fillAmount = curHealth / maxSoldier / 2;
        showUpgradeButton();
    }
    public void UpgradeLevel()
    {
        if (level >= maxLevel)
        {
            return;
        }
        level++;
        curSoldier = curSoldier / 2;
        updateHealth();
        changeSkin();
    }
    public void changeSkin()
    {       
        string lv = "l" + level;
        string team = "p" + (int)Team;
        string name = "castle";
        string defaul = "s4";
        //print(name + "_" + team + "_" + defaul + "_" + lv);castle_p0_s4_l1
        Sprite sp = Resources.Load<Sprite>("House/" + name + "_" + team + "_" + defaul + "_" + lv);
        GetComponent<SpriteRenderer>().sprite = sp;
    }
    private void showUpgradeButton()
    {
        if(curSoldier >= maxSoldier / 2)
        {
            btnUpgrade.gameObject.SetActive(true);
        }
        else
        {
            btnUpgrade.gameObject.SetActive(false);
        }
    }


    public void setAnimSelected(bool active)
    {
        imgSelected.gameObject.SetActive(active); 
        if(active)
        {
            imgSelected.GetComponent<Animator>().Play("animSelected");
        }
        else
        {
            imgSelected.GetComponent<Animator>().Play("animNonSelected");
        }
    }
    private void setMouseEnter(bool isEnter)
    {
         
         imgMouseEnter.gameObject.SetActive(isEnter);
        if(Team == DEFINE.TEAM.MYTEAM)
        {
            imgMouseEnter.GetComponent<Image>().sprite = Resources.Load<Sprite>("selected/our_selector");
        }
        else
        {
            imgMouseEnter.GetComponent<Image>().sprite = Resources.Load<Sprite>("selected/enemy_selector");
        }
    }
}
