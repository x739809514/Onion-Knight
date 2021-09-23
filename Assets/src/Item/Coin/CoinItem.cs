using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinItem : MonoBehaviour {
    public AudioClip CoinAudio;

    private void Awake()
    {
        //CoinAudio = GetComponent<AudioClip>();
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            AudioSource.PlayClipAtPoint(CoinAudio, transform.position, 1f);
            CoinUI.currentCoin += 1;
            //StartCoroutine(DestoryCoin(.2f));
            Destroy(gameObject);
        }
    }

    //IEnumerator DestoryCoin(float seconds)
    //{
    //    yield return new WaitForSeconds(seconds);
        
    //}
}
