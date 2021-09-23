using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float RepelSpeed;
    public int health;
    public float ChaseRadius;
    public float HitRadius;
    public bool isRight;
    [Header("敌人状态   0：巡逻  1：追击  2：攻击")]
    public int State = 0;
    private int damage;
    private bool isHit;
    private AnimatorStateInfo info;   //播放动画的进度
    private Vector2 direction;
    private Animator anim;
    public Transform playerTransform;
    public PlayerController pi;
    private Rigidbody2D rd2;
    public GameObject Boom;
    public GameObject dropCoin;

    public void Awake()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        pi = GameObject.FindGameObjectWithTag("Player").GetComponentInParent<PlayerController>();
        rd2 = GetComponent<Rigidbody2D>();
    }

    public void Start()
    {
        anim = GetComponent<Animator>();
        damage = pi.attackpower;
    }

    public void Update()
    {
        info = anim.GetCurrentAnimatorStateInfo(0);     //持续获取动画进度
        if (health <= 0)
        {
            Instantiate(Boom, transform.position, Boom.transform.rotation);
            Destroy(gameObject);
        }
        else if (isHit)
        {
            rd2.velocity = direction * RepelSpeed;
            if (info.normalizedTime >= 0.6f)
            {
                isHit = false;
            }
        }
    }

    public void GetHit(Vector2 direction)
    {
        transform.localScale = new Vector3(direction.x, 1, 1);
        isHit = true;
        this.direction = direction;
        GetDamage(damage);
        anim.SetTrigger("hit");
    }

    public void CheckRight(Transform target)
    {

        Vector3 dir = target.position - transform.position;
        float dot1 = Vector3.Dot(transform.right, dir.normalized);  //点乘判断左右，dot>0在右，<0在左、
        if (dot1 > 0)
        {
            isRight = true;
        }
        else
        {
            isRight = false;
        }

    }

    public void GetDamage(int damage)
    {
        health -= damage;
        
    }

    public void ChasePlayer()
    {

    }

    public void Patrol()
    {

    }

    public void AttackPlayer()
    {

    }

    public void GetState()
    {

    }


}
