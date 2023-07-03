
using UnityEngine;

public class ChooseMap : MonoBehaviour
{

    public AudioClip btnClicl;
    
    public void ChooseNewMap(int numberMap)
    {
        if (PlayerPrefs.GetString("Music") != "No")
        {
            GetComponent<AudioSource>().clip = btnClicl;    
            GetComponent<AudioSource>().Play();
        }
        PlayerPrefs.SetInt("NowMap", numberMap);
        GetComponent<CheckMaps>().whichMapSelected();
    }
}
