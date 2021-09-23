using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {
    public float damage;
    public float time;
    public float starttime;
    private Animator anim;
    public PolygonCollider2D polygonCollider2;
    [Header("打击感")]
    public float shakeTime;
    public int LightPause;
    public float lightStrength;
    private PlayerController pi;
    public bool isAttack;
    public AudioClip AttackAudio1;
    private AudioSource AttackAudio2;
    private bool AttackSomeOne;

	void Awake () {
        AttackSomeOne = false;
        pi = GameObject.FindGameObjectWithTag("Player").GetComponentInParent<PlayerController>();
        anim = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Animator>();
        polygonCollider2 = GetComponent<PolygonCollider2D>();
        AttackAudio2 = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        Attack();
	}

    void Attack()
    {
        if (pi.attack&&pi.canAttack)
        {
            isAttack = true;
            anim.SetTrigger("attack");
            if (!AttackSomeOne)
            {
                AudioSource.PlayClipAtPoint(AttackAudio1, transform.position, 1f);
            }
            StartCoroutine(StarAttack());
        }
    }
    IEnumerator StarAttack()
    {
        yield return new WaitForSeconds(starttime);
        polygonCollider2.enabled = true;
        StartCoroutine(DisableCollider());
    }

    IEnumerator DisableCollider()
    {
        yield return new WaitForSeconds(time);
        polygonCollider2.enabled = false;
        AttackSomeOne = false;
    }

    public void AttackOver()
    {
        isAttack = false;
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        AttackSomeOne = true;
        if (collision.CompareTag("Enemy"))
        {
            AttackAudio2.Play();
            AttackScene.Instance.HitPause(LightPause);
            AttackScene.Instance.CameraShake(shakeTime, lightStrength);
            if (pi.transform.localScale.x > 0)
            {
               collision.GetComponent<Enemy>().GetHit(Vector2.right);
            }
            else if (pi.transform.localScale.x < 0)
            {
                collision.GetComponent<Enemy>().GetHit(Vector2.left);
            }
        }
    }

}
