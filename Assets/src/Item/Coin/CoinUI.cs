using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinUI : MonoBehaviour {

    public int startCoinQuantity;
    public Text coinTextQuantity;
    public static int currentCoin;

	// Use this for initialization
	void Start () {
        currentCoin = startCoinQuantity;
	}
	
	// Update is called once per frame
	void Update () {
        coinTextQuantity.text = currentCoin.ToString();
	}
}
