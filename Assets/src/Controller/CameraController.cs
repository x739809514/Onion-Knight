using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    public Transform target;
    public float camera_speed;

    public Vector2 minPosition;
    public Vector3 maxPosition;
    private Camera _camera;

	void Awake () {
        _camera = Camera.main;
	}
	

	void Update () {

	}

    private void LateUpdate()
    {
        if (target != null)
        {
            if (transform.position != target.position)
            {
                Vector3 targetPos = target.position;
                targetPos.x = Mathf.Clamp(targetPos.x,minPosition.x, maxPosition.x);
                targetPos.y = Mathf.Clamp(targetPos.y, minPosition.y, maxPosition.y);
                transform.position = Vector3.Lerp(transform.position, targetPos, camera_speed);
            }
        }
    }

    public void SetCamPoLimit(Vector2 minPos,Vector2 maxPos)
    {
        minPosition = minPos;
        maxPosition = maxPos;
    }
}
