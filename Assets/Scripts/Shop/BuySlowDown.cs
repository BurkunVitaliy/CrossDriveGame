using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BuySlowDown : MonoBehaviour
{
    public AudioClip success, fail;
    public GameObject slowDown_btn;
    public Animation coinsText;
    public TMP_Text coinsCount, amount;

    public void BuyNewSlowTime(int needCoins)
    {
        int coins = PlayerPrefs.GetInt("Coins");
        int number = PlayerPrefs.GetInt("Number");
       
        if (coins < needCoins)
        {
            if (PlayerPrefs.GetString("Music") != "No")
            {
                GetComponent<AudioSource>().clip = fail;
                GetComponent<AudioSource>().Play();
            }
            coinsText.Play();
        }
        else
        {
            int nowNumber = number + 1;
            amount.text = nowNumber.ToString();
            PlayerPrefs.SetInt("Number" , nowNumber );

            int nowCoins = coins - needCoins;
            coinsCount.text = nowCoins.ToString();
            PlayerPrefs.SetInt("Coins", nowCoins);
            
            if (PlayerPrefs.GetString("Music") != "No")
            {
                GetComponent<AudioSource>().clip = success;
                GetComponent<AudioSource>().Play();
            }
        }
    }
}
