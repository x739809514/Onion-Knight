using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateController : MonoBehaviour {
    public bool idle;
    public bool jump;
    public bool run;
    public bool fall;

    private AnimationController ac;

	void Awake () {
        ac = GetComponent<AnimationController>();
	}
	
	void Update () {
        idle = ac.CheckState("Idle");
        run = ac.CheckState("Run");
        jump = ac.CheckState("jump");
        fall = ac.CheckState("Fall");
	}
}
