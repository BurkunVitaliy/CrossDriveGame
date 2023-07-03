using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimeSlowButton : MonoBehaviour
{
    public int number ;
    public TMP_Text NumberOfDecelerations;
    public float HowSlow;
    public AudioSource audio;
    public Sprite btnPress;
    [NonSerialized]public bool isOnSlowTime;
    private const string IsCountSlowTime =  "IsCountSlowTime";
    
    private Image image;


    private void Start()
    {
        image = GetComponent<Image>();
        if (!PlayerPrefs.HasKey(IsCountSlowTime))
        {
            PlayerPrefs.SetInt("Number", 3);
            
            PlayerPrefs.SetInt(IsCountSlowTime, 1);
            PlayerPrefs.Save();
        }
        
        
        if (PlayerPrefs.HasKey("Number"))
        {
            number = PlayerPrefs.GetInt("Number");
            
        }
    }

    private void Update()
    {
        NumberOfDecelerations.text = "" + number;
        if(number < 1)
        {
            image.sprite = btnPress;
        }
    }
    
    public void OnSlowTime()
    {
        if (number > 0 && !isOnSlowTime)
        {
            StartCoroutine(LiveTime());
            number--;
            PlayerPrefs.SetInt("Number", number);
            NumberOfDecelerations.text = "" + number;
            audio.Play();
            Time.timeScale = HowSlow;
            Time.fixedDeltaTime = Time.timeScale * 0.015f;
            isOnSlowTime = true;
        } 
        
        IEnumerator  LiveTime()
        {
            yield return new WaitForSeconds(0.75f);
            Time.timeScale = 1f;
            isOnSlowTime = false;
        }
    }
}
