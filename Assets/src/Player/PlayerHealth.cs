using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {
    public PlayerController pi;
    public Image HealthBar;
    [Header("屏幕震动")]
    public int HitPause;
    public float HitStrength;
    public float ShakeTime;
    [Header("人物受伤闪烁")]
    public int BlinkNum;
    public float second;
    public float WuDiSecond;
    private Rigidbody2D rigidbody2;
    private Renderer myReneder;
    private bool isWuDi;
    private void Awake()
    {
        pi = GetComponent<PlayerController>();
        rigidbody2 = GetComponent<Rigidbody2D>();
        myReneder = GetComponentInChildren<Renderer>();
        //HealthBar = GetComponent<Image>();
    }

    private void Start()
    {
        pi.Hp = pi.HpMax;
        isWuDi = false;
    }

    public void getDamage(int damage)
    {
        if (pi.Hp <= 0)
        {
            pi.Hp = 0;
            pi.die = true;
        }
        if (!isWuDi)
        {
            pi.Hp -= damage;
        }
    }

    private void Update()
    {
        HealthBar.fillAmount = (float)pi.Hp / (float)pi.HpMax;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag=="Enemy")
        {
            pi.hit = true;
            pi.canAttack = false;
            AttackScene.Instance.HitPause(HitPause);
            AttackScene.Instance.CameraShake(ShakeTime, HitStrength);
            pi.ph.getDamage(pi.damage);
            BlinkPlayer(BlinkNum, second);
            PlayerInvincible(WuDiSecond);
            if (transform.localScale.x > 0)
            {
                rigidbody2.velocity = new Vector2(-8, 5);
            }
            else if (transform.localScale.x < 0)
            {
                rigidbody2.velocity = new Vector2(8, 5);
            }
        }
    }

    private void BlinkPlayer(int numBlink,float seconds)
    {
        StartCoroutine(DoBlink(numBlink, seconds));
    }

    IEnumerator DoBlink(int numBlink, float seconds)
    {
        isWuDi = true;
        for(int i = 0; i < numBlink*2; i++)
        {
            myReneder.enabled = !myReneder.enabled;
            yield return new WaitForSeconds(seconds);
        }
        myReneder.enabled = true;
        isWuDi = false;
        pi.canAttack = true;
    }
    private void PlayerInvincible(float seconds)
    {
        StartCoroutine(DoInvincible(seconds));
    }

    IEnumerator DoInvincible(float seconds)
    {
        pi.CapsuleCol.enabled = false;
        pi.PolygonCol.enabled = false;
        yield return new WaitForSeconds(seconds);
        pi.CapsuleCol.enabled = true;
        pi.PolygonCol.enabled = true;
    }
}
