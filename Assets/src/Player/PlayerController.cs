using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float HorizonMove;
    public float FaceDirection;
    public string JumpBtn;
    public bool jump;
    public bool fall;
    public bool attack;      //是否按下攻击键
    public bool hit;
    public float Hp;
    public float HpMax;
    public bool canAttack;  //能否攻击
    public CapsuleCollider2D CapsuleCol;
    public BoxCollider2D BoxCol;
    public PolygonCollider2D PolygonCol;
    public int damage;  //受到伤害值
    public int attackpower; //攻击力
    public PlayerHealth ph;
    public bool InputEnable;
    public bool die=false;

    private void Awake()
    {
        ph = GetComponent<PlayerHealth>();
        CapsuleCol = GetComponent<CapsuleCollider2D>();
        BoxCol = GetComponentInChildren<BoxCollider2D>();
        PolygonCol = GameObject.FindGameObjectWithTag("Seasor").GetComponent<PolygonCollider2D>();
        
    }

    public void Start()
    {
        InputEnable = true;
        canAttack = true;
    }

    // Update is called once per frame
    void Update()
    {
        HorizonMove = Input.GetAxis("Horizontal");
        FaceDirection = Input.GetAxisRaw("Horizontal");
        jump = Input.GetKeyDown(JumpBtn);
        attack = Input.GetButtonDown("Attack");
    }
}
