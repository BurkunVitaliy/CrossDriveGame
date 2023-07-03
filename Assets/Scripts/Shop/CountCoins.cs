using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CountCoins : MonoBehaviour
{
    private void Start()
    {
        GetComponent<TMP_Text>().text = PlayerPrefs.GetInt("Coins").ToString();
    }
}
