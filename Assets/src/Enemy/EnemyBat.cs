using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBat : Enemy {
    public float speed;
    public float startWaitTime;
    public float waitTime;

    public Transform movePos;
    public Transform leftDownPos;
    public Transform RightUpPos;

	// Use this for initialization
	void Start () {
        base.Start();
        waitTime = startWaitTime;
        movePos.position = GetRamdomPos();
	}
	
	// Update is called once per frame
	void Update () {
        base.Update();
        transform.position = Vector2.MoveTowards(transform.position, movePos.position, speed * Time.deltaTime);
        if (Vector2.Distance(transform.position,movePos.position)<0.1f)
        {
            if (waitTime <= 0)
            {
                movePos.position = GetRamdomPos();
                waitTime = startWaitTime;
            }
            else
            {
                waitTime -= Time.deltaTime;
            }
        }
	}

    //获得随机位置
    Vector2 GetRamdomPos()
    {
        Vector2 rndPos = new Vector2(Random.Range(leftDownPos.position.x, RightUpPos.position.x), Random.Range(leftDownPos.position.y, RightUpPos.position.y));
        return rndPos;
    }
}
