using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OozeEnemy : Enemy
{
    public float PatrolSpeed;
    public float ChaseSpeed;
    public float waitTime;
    public Transform[] movePos;
    private int i = 0;
    private bool movingRight = true;
    private float wait;

    public void Awake()
    {
        base.Awake();
    }

    public void Start()
    {
        base.Start();
        wait = waitTime;
    }


    public void Update()
    {
        base.Update();
        GetState();
    }

    public void GetState()
    {
        base.GetState();
        if (playerTransform != null)
        {
            CheckRight(playerTransform);
            switch (State)
            {
                case 0:
                    Patrol();
                    if ((transform.position - playerTransform.position).sqrMagnitude < ChaseRadius)
                    {
                        State = 1;
                    }
                    break;
                case 1:
                    if (isRight == true)
                    {
                        ChasePlayer(ChaseSpeed, Vector2.left);
                    }
                    else
                    {
                        ChasePlayer(ChaseSpeed, Vector2.right);
                    }
                    if ((transform.position - playerTransform.position).sqrMagnitude < HitRadius)
                    {
                        State = 2;
                    }
                    break;
                case 2:
                    AttackPlayer();
                    break;
                default:
                    break;
            }
        }
        
    }

    public void ChasePlayer(float speed, Vector2 direction)
    {
        base.ChasePlayer();
        if (playerTransform != null)
        {
            Vector3 targetPos = transform.position;
            targetPos.x = playerTransform.position.x;
            transform.position = Vector2.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
            transform.localScale = new Vector3(direction.x, 1, 1);
        }
    }

    public void Patrol()
    {
        base.Patrol();
        transform.position = Vector2.MoveTowards(transform.position, movePos[i].position, PatrolSpeed * Time.deltaTime);
        if (Vector2.Distance(transform.position, movePos[i].position) < 0.1f)
        {
            if (waitTime > 0)
            {
                waitTime -= Time.deltaTime;
            }
            else
            {
                if (movingRight)
                {
                    transform.eulerAngles = new Vector3(0, -180, 0);
                    movingRight = false;
                }
                else
                {
                    transform.eulerAngles = new Vector3(0, 0, 0);
                    movingRight = true;
                }

                //切换移动位置
                if (i == 0)
                {
                    i = 1;
                }
                else
                {
                    i = 0;
                }
                waitTime = wait;
            }
        }
    }
    

}
