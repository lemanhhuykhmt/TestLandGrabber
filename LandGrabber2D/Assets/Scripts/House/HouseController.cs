using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class HouseController : MonoBehaviour {

    private GameObject gameController;
    public GameObject soldier;
    public GameObject bigPoint;

    public float popuRating = 1.0f;
    protected float popuCountDown = 0;
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
            UpdateSoldier();
            showUpgradeButton();
            changeTextNumber();
        }
    }
    protected virtual void UpdateSoldier()
    {
        curSoldier = (int)CurHealth;
        imgHealthCur.fillAmount = curHealth / maxSoldier;
    }
    public int CurSoldier
    {
        set
        {
            curSoldier = value;
            UpdateHealth();
            showUpgradeButton();
            changeTextNumber();
        }
        get
        {
            return curSoldier;
        }
    }
    protected virtual void UpdateHealth()
    {
        curHealth = curSoldier;
        imgHealthCur.fillAmount = curHealth / maxSoldier;
    }
    public int Level
    {
        set
        {
            level = value;
            UpdateLevel();
        }
        get
        {
            return level;
        }
    }
    protected virtual void UpdateLevel()
    {
            maxSoldier = level * 10;
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
    public Text txtNumberSoldier;


    // Use this for initialization
    protected virtual void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController");
        setMouseEnter(false);
        setAnimSelected(false);
        UpdateHealth();
        btnUpgrade.gameObject.SetActive(false);
        showUpgradeButton();
        changeTextNumber();

    }

    // Update is called once per frame
    void Update()
    {
        UpdatePopulation();
    }
    protected virtual void UpdatePopulation()
    {
        if (popuCountDown <= 0 && curSoldier < maxSoldier)
        {
            CurSoldier++;
            popuCountDown = popuRating;
        }
        else if (popuCountDown < 0 && curSoldier > maxSoldier)
        {
            CurSoldier--;
            popuCountDown = popuRating;
        }
        else if (popuCountDown < 0)
        {
            popuCountDown = popuRating;
        }
        popuCountDown -= Time.deltaTime;
    }
    IEnumerator doubleClick()
    {

        yield return new WaitForSeconds(clickdelay);
        if (clicked == 1)
        {
            if (Team == DEFINE.TEAM.MYTEAM)
            {
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
                    if (gameController.GetComponent<GameController>().HasConnected(transform.position) == true)
                    {
                        gameController.GetComponent<GameController>().target = gameObject.transform.position;
                        gameController.GetComponent<GameController>().Attack();
                    }
                }
            }
        }
        else if (clicked > 1)
        {
            if (gameController.GetComponent<GameController>().HasConnected(transform.position) == true)
            {
                gameController.GetComponent<GameController>().target = gameObject.transform.position;
                gameController.GetComponent<GameController>().Attack();
            }
        }
        yield return new WaitForSeconds(0.05f);
        clicked = 0;
    }
    void OnMouseDown()
    {
        clicked++;
        if (clicked == 1)
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

    public void Attack(Vector3 target)
    {
        setAnimSelected(false);
        StartCoroutine(spoil(target));
    }
    IEnumerator spoil(Vector3 target)
    {
        int solidierOut = curSoldier / 2;
        if (curSoldier == 1)
        {
            solidierOut = 1;
        }
        CurSoldier = curSoldier - solidierOut;
        //get way
        List<WayPoint> ways = getWaysMove(target);
        for (int i = 0; i < solidierOut; ++i)
        {
            // spoi lính
            yield return new WaitForSecondsRealtime(0.1f);
            GameObject soil = Instantiate(soldier, transform.position, Quaternion.identity);
            soil.GetComponent<SoldierController>().endTarget = target;
            soil.GetComponent<SoldierController>().team = Team;
            soil.GetComponent<SoldierController>().waysMove = ways;
        }
    }
    private List<WayPoint> getWaysMove(Vector3 target)
    {
        BigPoint begin = gameController.GetComponent<GameController>().graph.GetPointByPos(transform.position);
        BigPoint end = gameController.GetComponent<GameController>().graph.GetPointByPos(target);
        return gameController.GetComponent<GameController>().graph.Dijkstra(begin, end);
    }
    public void ReceiveDamege(float damege)
    {
        CurHealth = CurHealth - damege;
    }
    public void AddSoidier()
    {
        CurSoldier++;
        //updateHealth();
    }
    public void UpgradeLevel()
    {
        if (level >= maxLevel)
        {
            return;
        }
        Level++;
        CurSoldier = curSoldier / 2;
        //updateHealth();
        changeSkin();
    }
    public void ChangeTeam(DEFINE.TEAM team)
    {
        Team = team;
        gameController.GetComponent<GameController>().graph.GetPointByPos(transform.position).Team = team;
        CurHealth = 0f;
        Level = 1;
        changeSkin();
    }
    protected abstract void changeSkin();
    private void showUpgradeButton()
    {
        if (curSoldier >= maxSoldier / 2 && level < maxLevel)
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
        if (active)
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
        if (Team == DEFINE.TEAM.MYTEAM)
        {
            imgMouseEnter.GetComponent<Image>().sprite = Resources.Load<Sprite>("selected/our_selector");
        }
        else
        {
            if (gameController.GetComponent<GameController>().HasConnected(transform.position) == false)
            {
                imgMouseEnter.gameObject.SetActive(false);
            }
            imgMouseEnter.GetComponent<Image>().sprite = Resources.Load<Sprite>("selected/enemy_selector");
        }
    }
    private void changeTextNumber()
    {
        txtNumberSoldier.text = curSoldier + " / " + maxSoldier;
    }


    ////////////
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag.Equals("Soldier"))
        {
            SoldierController comCollision = collision.gameObject.GetComponent<SoldierController>();
            if (comCollision.team == Team)
            {
                if (comCollision.endTarget.Equals(this.transform.position))// nếu chạm vào nhà mình là đích
                {
                    this.AddSoidier();
                    Destroy(comCollision.gameObject);
                }
                else // nếu chạm vào nhà mình kp đích
                {
                    // print("đi qua");
                }
            }
            else// nếu chạm vào nhà địch
            {
                if (CurHealth <= 0)
                {
                    ChangeTeam(comCollision.team);
                }
                else
                {
                    ReceiveDamege(comCollision.damege);
                }
                Destroy(comCollision.gameObject);
            }

        }
    }
}
