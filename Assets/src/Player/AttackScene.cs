using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackScene : MonoBehaviour {
    private static AttackScene instance;
    public static AttackScene Instance
    {
        get
        {
            if (instance == null)
            {
                instance = Transform.FindObjectOfType<AttackScene>();
            }
            return instance;
        }
    }
    private bool isShake=false;


    public void HitPause(int duration)
    {
        StartCoroutine(Pause(duration));
    }


    IEnumerator Pause(int duration)
    {
        float pauseTime = duration / 60f;
        Time.timeScale = 0;     //影响时间缩放的值，1为正常流速
        yield return new WaitForSecondsRealtime(pauseTime);
        Time.timeScale = 1;
    }

    public void CameraShake(float duration,float strength)
    {
        if (!isShake)
        {
            StartCoroutine(Shake(duration, strength));
        }
    }

    IEnumerator Shake(float duration,float strength)
    {
        isShake = true;
        Transform _camera = Camera.main.transform;
        Vector3 startPosition = _camera.position;
        while (duration>0)
        {
            _camera.position = Random.insideUnitSphere * strength + startPosition;
            duration -= Time.deltaTime;
            yield return null;
        }
        _camera.position = startPosition;
        isShake = false;
    }
}
