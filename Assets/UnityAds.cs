using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class UnityAds : IUnityAdsListener
{
    string gameId = "4047675";
    bool testMode = true;
    string surfacingId = "bannerPlacement";
    string interstitialId= "Interstitial_Android";
    string videoId = "Rewarded_Android";

    public UnityAds()
    {
        Advertisement.AddListener(this);
        Advertisement.Initialize(gameId, testMode);
    }

    public IEnumerator PlaceBanner()
    {
        while (!Advertisement.isInitialized)
        {
            yield return new WaitForSeconds(0.5f);
        }

        Advertisement.Banner.SetPosition(BannerPosition.BOTTOM_CENTER);
        Advertisement.Banner.Show(surfacingId);
    }

    public void LoadInterstitial()
    {

    }

    public bool IsInterstitialReady()
    {
        return Advertisement.IsReady("Interstitial_Android");
    }
    public void ShowInterstitial()
    {
        if (Advertisement.IsReady("Interstitial_Android"))
        {
            Advertisement.Show("Interstitial_Android");
        }
    }

    public void LoadVideo() {
        
    }

    public bool IsVideoReady()
    {
        return Advertisement.IsReady(videoId);
    }

    public void ShowVideo()
    {
        if (Advertisement.IsReady(videoId))
        {
            Advertisement.Show(videoId);
        }
    }



    public void OnUnityAdsDidFinish(string surfacingId, ShowResult showResult)
    {
        Debug.Log("www");
        // Define conditional logic for each ad completion status:
        if (showResult == ShowResult.Finished)
        {
            Debug.Log("ok watched");
            GameObject.FindObjectOfType<gameLoop>().recieveReward();
            // Reward the user for watching the ad to completion.
        }
        else if (showResult == ShowResult.Skipped)
        {
            // Do not reward the user for skipping the ad.
        }
        else if (showResult == ShowResult.Failed)
        {
            Debug.LogWarning("The ad did not finish due to an error.");
        }
    }

    public void OnUnityAdsDidError(string message)
    {
        Debug.Log(message);
    }

    public void OnUnityAdsDidStart(string surfacingId)
    {
        Debug.Log("AAA" + surfacingId);
    }

    public void OnUnityAdsReady(string placementId)
    {

    }
}
