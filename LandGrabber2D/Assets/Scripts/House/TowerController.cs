
using UnityEngine;
using UnityEngine.UI;

public class TowerController : HouseController
{
    public Transform firePoint;
    public float range = 2.0f;
    public float Range
    {
        set
        {
            range = value;
            imgTowerZone.rectTransform.sizeDelta = new Vector2(range * 2, range * 2);
        }
        get
        {
            return range;
        }
    }
    public GameObject target;
    public float fireRate = 1f;
    public float fireCountDown = 0f;
    public LineRenderer lineRenderer;
    public Image imgTowerZone;


    protected override void Start()
    {
        base.Start();
        InvokeRepeating("UpdateTarget", 0.0f, 0.5f);
        imgTowerZone.rectTransform.sizeDelta = new Vector2(range * 2, range * 2);
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
        fireRate += 1;
        Range += 0.25f;
        firePoint.position = new Vector3(firePoint.position.x, firePoint.position.y + 0.2f, firePoint.position.z);
        if(level == 1)
        {
            fireRate = 1.0f;
            Range = 2f;
            firePoint.position = new Vector3(this.transform.position.x - 0.05f, this.transform.position.y + 0.8f, firePoint.position.z);
        }
    }
    protected override void UpdatePopulation()
    {
        if (popuCountDown <= 0 && curSoldier < maxSoldier)
        {

        }
        else if (popuCountDown < 0 && curSoldier > maxSoldier)
        {
            CurSoldier -= 2;
            popuCountDown = popuRating;
        }
        else if (popuCountDown < 0)
        {
            popuCountDown = popuRating;
        }
        popuCountDown -= Time.deltaTime;


        if (target == null)
        {
            if (lineRenderer.enabled == true)
            {
                lineRenderer.enabled = false;
            }
            return;
        }
        if (fireCountDown <= 0f)
        {
            Shoot();
            fireCountDown = 1 / fireRate;
        }
        fireCountDown -= Time.deltaTime;
    }
    private void Shoot()
    {
        if (lineRenderer.enabled == false)
        {
            lineRenderer.enabled = true;
        }
        lineRenderer.SetPosition(0, new Vector3(firePoint.position.x, firePoint.position.y, -1));
        lineRenderer.SetPosition(1, new Vector3(target.transform.position.x, target.transform.position.y, -1));
        Destroy(target);
        target = null;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(this.transform.position, range);
    }
    protected override void changeSkin()
    {
        string lv = "l" + level;
        string team = "p" + (int)Team;
        string name = "tower";
        string defaul = "s4";
        //print(name + "_" + team + "_" + defaul + "_" + lv);castle_p0_s4_l1
        Sprite sp = Resources.Load<Sprite>("House/" + name + "_" + team + "_" + defaul + "_" + lv);
        imgSprite.GetComponent<Image>().sprite = sp;
    }
    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Soldier");
        float minDistance = Mathf.Infinity;
        GameObject nearstEnemy = null;
        foreach (GameObject enemy in enemies)
        {
            if (enemy.GetComponent<SoldierController>().team == this.Team) continue;
            float distance = Vector3.Distance(this.transform.position, enemy.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                nearstEnemy = enemy;
            }
        }

        if (nearstEnemy != null && minDistance <= range)
        {
            target = nearstEnemy;
        }
        else
        {
            target = null;
        }
    }
}
