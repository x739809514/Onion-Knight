using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureBox : MonoBehaviour {

    public bool canOpen;
    public bool isOpened;
    public Animator anim;
    public string KeyOpen;
    public GameObject coin;

    private void Start()
    {
        anim = GetComponent<Animator>();
        isOpened = false;
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown(KeyOpen))
        {
            if (canOpen && !isOpened)
            {
                anim.SetTrigger("opening");
                isOpened = true;
            }
        }
	}

    void GenCoin()
    {
        Instantiate(coin, transform.position, Quaternion.identity);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && collision.GetType().ToString() == "UnityEngine.CapsuleCollider2D")
        {
            canOpen = true;
            print(canOpen);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && collision.GetType().ToString() == "UnityEngine.CapsuleCollider2D")
        {
            canOpen = false;
        }
    }
}
