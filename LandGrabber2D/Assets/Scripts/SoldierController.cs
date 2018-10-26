using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierController : MonoBehaviour {
    public float speedMove = 1f;
    public float damege = 1;
    public DEFINE.TEAM team;
    public Transform endTarget;
    private Transform curTarget;
    private int wayPointIndex = 0;
    private float bias = 0.1f;
    float b;
    private Vector3 lastPos;
    private Vector3 dir;

    private Animator animMove;
    GameObject emptyGO;
    // Use this for initialization
    void Start () {
        lastPos = transform.position;
        animMove = GetComponent<Animator>();
        b = Random.Range(-bias, bias);
        changeWayMove(WayPointController.wayPoints[wayPointIndex]);
    }
	
	// Update is called once per frame
	void Update () {
        transform.Translate(dir.normalized * speedMove * Time.deltaTime, Space.World);
        if(Vector3.Distance(transform.position, curTarget.position) <= 0.2f)
        {
            lastPos = curTarget.position;
            getNextWayPoint();
        }
	}
    private void getNextWayPoint()
    {
        if(wayPointIndex == WayPointController.wayPoints.Length - 1)
        {
            changeWayMove(endTarget);
            return;
        }
        wayPointIndex++;
        changeWayMove(WayPointController.wayPoints[wayPointIndex]);

        
    }

    private void changeWayMove(Transform nextTrans)
    {
        Destroy(emptyGO);
        emptyGO = new GameObject();
        Transform nextPoint = emptyGO.transform;
        nextPoint.position = new Vector3(
            nextTrans.position.x,
            nextTrans.position.y,
            nextTrans.position.z);
        if(wayPointIndex == 0)
        {
            curTarget = nextPoint;
            dir = curTarget.position - lastPos;
            return;
        }
        dir = nextPoint.position - lastPos;
        float alpha = Vector2.Angle(new Vector2(1, 0), new Vector2(dir.x, dir.y));
        if(dir.y < 0)
        {
            alpha = 360 - alpha;
        }
        float radian = alpha * Mathf.PI / 180;
        nextPoint.position = new Vector3(nextPoint.position.x + b * Mathf.Abs(Mathf.Sin(radian)), nextPoint.position.y + b * Mathf.Abs(Mathf.Cos(radian)), nextPoint.position.z);
        curTarget = nextPoint;
        dir = nextPoint.position - lastPos;
        alpha = Vector2.Angle(new Vector2(1, 0), new Vector2(dir.x, dir.y));
        if (dir.y < 0)
        {
            alpha = 360 - alpha;
        }
        playAnimationMove(alpha);

    }
    private void OnDestroy()
    {
        Destroy(emptyGO);
    }
    private void playAnimationMove(float alpha)
    {
        if(alpha <= 22.5 || alpha >= 15 * 22.5)
        {
            animMove.Play("animMoveE");
        }
        else if(alpha < 3 * 22.5 && alpha > 1 * 22.5)
        {
            animMove.Play("animMoveEN");
        }
        else if (alpha <= 5 * 22.5 && alpha >= 3 * 22.5)
        {
            animMove.Play("animMoveN");
        }
        else if (alpha < 7 * 22.5 && alpha > 5 * 22.5)
        {
            animMove.Play("animMoveWN");
        }
        else if (alpha <= 9 * 22.5 && alpha >= 7 * 22.5)
        {
            animMove.Play("animMoveW");
        }
        else if (alpha < 11 * 22.5 && alpha > 9 * 22.5)
        {
            animMove.Play("animMoveWS");
        }
        else if (alpha <= 13 * 22.5 && alpha >= 11 * 22.5)
        {
            animMove.Play("animMoveS");
        }
        else if (alpha < 15 * 22.5 && alpha > 13 * 22.5)
        {
            animMove.Play("animMoveES");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if(collision.gameObject.tag.Equals("Castle"))
        {
            CastleController comCollision = collision.gameObject.GetComponent<CastleController>();
            if (comCollision.Team == team)
            {
                if(endTarget.Equals(collision.gameObject.transform))// nếu chạm vào nhà mình là đích
                {
                    comCollision.AddSoidier();
                    Destroy(gameObject);
                }
                else // nếu chạm vào nhà mình kp đích
                {
                    print("đi qua");
                }
            }
            else// nếu chạm vào nhà địch
            {
                print("đánh");
                if (comCollision.CurHealth <= 0)
                {
                    comCollision.Team = team;
                    comCollision.CurHealth = 0f;
                    comCollision.level = 1;
                    comCollision.changeSkin();
                }
                else
                {
                    comCollision.ReceiveDamege(damege);
                }
                Destroy(gameObject);
            }
            
        }
        

        
    }
}
