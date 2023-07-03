using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CountSlowDown : MonoBehaviour
{
    private void Start()
    {
        if (PlayerPrefs.HasKey("Number"))
        {
            GetComponent<TMP_Text>().text = PlayerPrefs.GetInt("Number").ToString();
        }
       
    }
}
