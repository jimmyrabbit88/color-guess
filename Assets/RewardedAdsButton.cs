using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;

[RequireComponent(typeof(Button))]
public class RewardedAdsButton : MonoBehaviour, IUnityAdsListener
{
    string gameId = "4047675";
    bool testMode = true;

    Button myButton;
    string mySurfacingId = "Rewarded_Android";//c

    void Start()
    {
        Advertisement.AddListener(this);
        Advertisement.Initialize(gameId, true);
        Debug.Log("something");
        //myButton = GetComponent<Button>();

        // Set interactivity to be dependent on the Ad Unit or legacy Placement’s status:
        myButton.interactable = true;
            //Advertisement.IsReady(mySurfacingId);

        Debug.Log(mySurfacingId);
        // Map the ShowRewardedVideo function to the button’s click listener:
        //if (myButton) myButton.onClick.AddListener(ShowRewardedVideo);
        Debug.Log("qq");
        // Initialize the Ads listener and service:
        
    }

    void TestClick()
    {
        Debug.Log("testclick");
    }

   

    // Implement a function for showing a rewarded video ad:
    void ShowRewardedVideo()
    {
        Debug.Log(Advertisement.IsReady(mySurfacingId));
        if (Advertisement.IsReady(mySurfacingId)) {
            Advertisement.Show(mySurfacingId);
        }
    }

    // Implement IUnityAdsListener interface methods:
    public void OnUnityAdsReady(string surfacingId)
    {
        Debug.Log("AA");
        // If the ready Ad Unit or legacy Placement is rewarded, activate the button: 
        Debug.Log(surfacingId);
        if (surfacingId == mySurfacingId)
        {
            Debug.Log("bb");
            myButton.interactable = true;
        }
    }

    public void OnUnityAdsDidFinish(string surfacingId, ShowResult showResult)
    {
        Debug.Log("www");
        // Define conditional logic for each ad completion status:
        if (showResult == ShowResult.Finished)
        {
            Debug.Log("ok watched");
            GameObject.FindObjectOfType<gameLoop>().watchVideoAd();
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
}
