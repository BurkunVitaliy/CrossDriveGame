using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds;
using GoogleMobileAds.Api;
using GoogleMobileAds.Api.AdManager;

public class AdsManager : MonoBehaviour
{
    private string adUnitId = "ca-app-pub-4856977533389833/8939437316";
    private InterstitialAd interstitialAd;


    private void Start()
    {
        DontDestroyOnLoad(gameObject);
       // interstitialAd = new InterstitialAd(adUnitId);

        /*interstitial.OnAdLoaded += HandleOnAdLoaded;
        // Called when an ad request failed to load.
        this.interstitial.OnAdFailedToLoad += HandleOnAdFailedToLoad;
        // Called when an ad is shown.
        this.interstitial.OnAdOpening += HandleOnAdOpening;
        // Called when the ad is closed.
        this.interstitial.OnAdClosed += HandleOnAdClosed;*/

    }
}
