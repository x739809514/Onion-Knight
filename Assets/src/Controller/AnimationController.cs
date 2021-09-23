using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour {
    public PlayerController pi;
    public Animator anim;
    public float Speed;
    public float JumpSpeed;
    public GameObject model;
    public ScreenFlash sf;
    public float jumpTime;
    private Rigidbody2D rigidbody2;
    private float jumpTimeCounter;
    private bool isJumping;

    private void Awake()
    {
        pi = GetComponent<PlayerController>();
        anim =model.GetComponent<Animator>();
        rigidbody2 = GetComponent<Rigidbody2D>();
        sf = GetComponent<ScreenFlash>();
        isJumping = true;
    }


    void Update()
    {
        if (pi.InputEnable==true)
        {
            if (pi.FaceDirection != 0)
            {
                transform.localScale = new Vector3(pi.FaceDirection, 1, 1);
            }

            if (pi.jump&& anim.GetBool("isGround"))
            {
                isJumping = true;
                jumpTimeCounter = jumpTime;
                anim.SetBool("jump", true);
                rigidbody2.velocity = Vector2.up * JumpSpeed;
            }
            if (Input.GetKey(pi.JumpBtn)&&isJumping==true)
            {
                if (jumpTimeCounter > 0)
                {
                    anim.SetBool("jump", true);
                    rigidbody2.velocity = Vector2.up * JumpSpeed;
                    jumpTimeCounter -= Time.deltaTime;
                }else
                {
                    isJumping = false;
                }
            }
            if (Input.GetKeyUp(pi.JumpBtn))
            {
                isJumping = false;
            }
            if (pi.die)
            {
                anim.SetBool("die", true);
                Destroy(gameObject, 1.0f);
            }
            else
            {
                anim.SetBool("die", false);
            }
        }
        if (rigidbody2.velocity.y < -11.0f && (!anim.GetBool("isGround")))
        {
            anim.SetBool("fall_from_high", true);
        }
        else
        {
            anim.SetBool("fall_from_high", false);
        }
        SwitchAnimState();
    }

    public void SwitchAnimState()
    {
        anim.SetBool("idle", false);
        anim.SetBool("run", false);
        if (anim.GetBool("jump"))
        {
            if (rigidbody2.velocity.y < 0)
            {
                anim.SetBool("jump", false);
                anim.SetBool("fall", true);
            }
        }else if (pi.hit)
        {
            anim.SetBool("hit", true);
            anim.SetBool("run", false);
            sf.FlashScreen();
            if (Mathf.Abs(rigidbody2.velocity.x) ==0)
            {
                anim.SetBool("hit", false);
                pi.hit = false;
                pi.CapsuleCol.enabled = true;
            }
        }
        else if (anim.GetBool("isGround"))
        {
            anim.SetBool("fall", false);
            if (Mathf.Abs(rigidbody2.velocity.x) > 0.1f)
            {
                anim.SetBool("run", true);
                anim.SetBool("idle", false);
            }
            else
            {
                anim.SetBool("run", false);
                anim.SetBool("idle", true);
                pi.InputEnable = true;
            }
        }
    }

    void FixedUpdate() {
        if (pi.InputEnable == true)
        {
            if (!pi.hit)
            {
                if (pi.HorizonMove != 0)
                {
                    rigidbody2.velocity = new Vector2(pi.HorizonMove * Speed * Time.fixedDeltaTime, rigidbody2.velocity.y);
                }
            }
        }
        
    }

    public void isGround()
    {
        anim.SetBool("isGround", true);
    }

    public void isNotGround()
    {
        anim.SetBool("isGround", false);
    }


    public bool CheckState(string statename,string layerName= "Base Layer")
    {
        int layerIndex = anim.GetLayerIndex(layerName);
        bool result = anim.GetCurrentAnimatorStateInfo(layerIndex).IsName(statename);
        return result;
    }

    public void onGroundEnter()
    {
        anim.SetBool("jump", false);
        anim.SetBool("fall", false);
    }

}
