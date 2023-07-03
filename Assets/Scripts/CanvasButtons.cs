
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CanvasButtons : MonoBehaviour
{
    public Sprite btn, btnPress, musicON, musicOff;
    
    private Image image;

    private void Start()
    {
        image = GetComponent<Image>();

        if (gameObject.name == "MusicButton")
        {
            if (PlayerPrefs.GetString("Music") == "No")
                transform.GetChild(0).GetComponent<Image>().sprite = musicOff;
        }
        
    }

    public void MusicButton()
    {
        if (PlayerPrefs.GetString("Music") == "No")
        {
            PlayerPrefs.SetString("Music", "Yes");
            transform.GetChild(0).GetComponent<Image>().sprite = musicON;
        }
        else
        {
            PlayerPrefs.SetString("Music", "No");
            transform.GetChild(0).GetComponent<Image>().sprite = musicOff;
        }
        
        PlayButtonSound();
    }

    public void ShopScene()
    {
        StartCoroutine(LoadScene("Shop"));
        PlayButtonSound();
    }
    public void ExitShopScene()
    {
        StartCoroutine(LoadScene("Main"));
        PlayButtonSound();
    }

    


    public void PlayGame()
    {
        StartCoroutine(LoadScene("Game"));
        if (PlayerPrefs.GetString("First Game")== "No")
        {
            StartCoroutine(LoadScene("Game"));
        }
        else
        {
            StartCoroutine(LoadScene("Study"));
        }
        PlayButtonSound();
    }
    public void RestartGame()
    {
        StartCoroutine(LoadScene("Game"));
        PlayButtonSound();
    }

  
    public void SetPressButton()
    {
        image.sprite = btnPress;
        transform.GetChild(0).localPosition -= new Vector3(0,3f,0);
    }
    
    public void SetDefaultButton()
    {
        image.sprite = btn;
        transform.GetChild(0).localPosition += new Vector3(0,3f,0);
    }

    IEnumerator LoadScene(string name)
    {
        float fadeTime = Camera.main.GetComponent<Fading>().Fade(1f);
        yield return new WaitForSeconds(fadeTime);
        SceneManager.LoadScene(name);
       
    }

    private void PlayButtonSound()
    {
        if (PlayerPrefs.GetString("Music") != "No")
         GetComponent<AudioSource>().Play();
    }
}
