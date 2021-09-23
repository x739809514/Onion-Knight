using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class onGroundSeasor : MonoBehaviour {

    public Transform FeetPos;
    public float checkRadius;
    public LayerMask WhatIsGround;
	
	void FixedUpdate () {
        CheckOnGround();
	}

    void CheckOnGround()
    {
        bool isground = Physics2D.OverlapCircle(FeetPos.position, checkRadius, WhatIsGround); 
        if (isground)
        {
            SendMessageUpwards("isGround");
        }
        else
        {
            SendMessageUpwards("isNotGround");
        }
    }
}
