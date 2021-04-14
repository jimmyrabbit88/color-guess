using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;
using GoogleMobileAds.Api;

public class gameLoop : MonoBehaviour
{
    //string gameId = "4047675";
    //bool testMode = true;
    //public string surfacingId = "bannerPlacement";

    public AdMobAds adMobAds;
    public UnityAds unityAds;

    private int lives = 10;
    private int points = 0;
    private GameObject hexVal;
    private GameObject[] buttons;
    private GameObject btn_lifeline;
    private GameObject btn_start;
    private GameObject btn_video;
    private GameObject lives_disp;
    private GameObject points_disp;
    private Question q;

    //private BannerView bannerView;

    void Start()
    {
        InitializeAds();
        DisplayBannerAd();
        Assignments();
    }

    private void Assignments()
    {
        hexVal = GameObject.FindGameObjectWithTag("HexText");
        buttons = GameObject.FindGameObjectsWithTag("AnsBtn");
        btn_start = GameObject.FindGameObjectWithTag("BtnStart");
        btn_lifeline = GameObject.Find("btn_lifeline");
        btn_video = GameObject.Find("btn_video");
        points_disp = GameObject.Find("txt_score_val");
        lives_disp = GameObject.Find("txt_lives_val");
        foreach (var btn in buttons)
        {
            btn.SetActive(false);
        };
        hexVal.SetActive(false);
        btn_video.SetActive(false);
    }
    public void StartGame()
    {
        foreach (var btn in buttons)
        {
            btn.SetActive(true);
        }
        hexVal.SetActive(true);
        btn_start.SetActive(false);
        btn_lifeline.SetActive(true);


        lives_disp.GetComponent<Text>().text = lives.ToString();
        points_disp.GetComponent<Text>().text = points.ToString();
        setLevel();
        //print("gameOver");
        
    }

    private void setLevel()
    {
        foreach (var btn in buttons)
        {
            btn.SetActive(true);
        }

        Color newCol;

        q = generateQuestion();
        
        hexVal.GetComponent<Text>().text = q.clue;
        
        for(var i=0; i<6; i++)
        {

            if (ColorUtility.TryParseHtmlString(q.answers[i] , out newCol))
            {
                buttons[i].GetComponent<Image>().color = newCol;
            }
            else
            {
                buttons[i].GetComponent<Image>().color = Color.red;
            }
        }
    }

    public void onSelect(int value)
    {
        Color c = buttons[value].GetComponent<Image>().color;
        string guess = "#" + ColorUtility.ToHtmlStringRGB(c);
        if (guess.Equals(q.clue)){
            points++;
            points_disp.GetComponent<Text>().text = points.ToString();
            setLevel();
            
        }
        else
        {
            lives--;
            lives_disp.GetComponent<Text>().text = lives.ToString();
            if(lives == 1)
            {
                adMobAds.LoadInterstitial();
                unityAds.LoadInterstitial();
            }
            if (lives <= 0)
            {
                resetGame();
            }
        }
    }
    
    // Use Lifeline to remove 3 options
    public void LifeLine()
    {
        var i = 0;
        while(i<3)
        {
            var num = (int)Random.Range(0,5.999f);
            if (!q.answers[num].Equals(q.clue))
            {
                if(buttons[num].GetComponent<Image>().color != Color.clear)
                {
                    buttons[num].SetActive(false);
                    buttons[num].GetComponent<Image>().color = Color.clear;
                    i++;
                }
                
            }
        }
        btn_lifeline.SetActive(false);
        btn_video.SetActive(true);
        unityAds.LoadVideo();
        adMobAds.LoadVideo();
    }
    
    // Watched Video Ad
    public void watchVideoAd()
    {
        if (!adMobAds.IsVideoReady() && !unityAds.IsVideoReady())
        {
            Debug.Log("no video ad was ready");
        }
        else if (!adMobAds.IsVideoReady() && unityAds.IsVideoReady())
        {
            unityAds.ShowVideo();
        }
        else if (adMobAds.IsVideoReady() && !unityAds.IsVideoReady())
        {
            adMobAds.ShowVideo();
        }
        else
        {
            if (Random.Range(0, 10) > 4)
            {
                unityAds.ShowVideo();
            }
            else
            {
                adMobAds.ShowVideo();
            }
        }
    }

    public void recieveReward()
    {
        btn_lifeline.SetActive(true);
        btn_video.SetActive(false);
    }

    //Game reset on end
    public void resetGame()
    {
        points = 0;
        lives = 10;
        hexVal.SetActive(false);
        btn_start.SetActive(true);
        foreach (var btn in buttons)
        {
            btn.SetActive(false);
        }
        ShowInterstitialAd();

    }

    // Both Ad Type Initialization
    private void InitializeAds()
    {
        adMobAds = new AdMobAds();
        unityAds = new UnityAds();
    }

    // Banner Ad Choice
    private void DisplayBannerAd()
    {
        if(Random.Range(0, 10) > 4)
        {
            StartCoroutine(unityAds.PlaceBanner());
        }
        else
        {
            // Admob Banner
            adMobAds.PlaceBanner();
        }
    }

    // UnityAds
    public void ShowInterstitialAd()
    {
        if (!adMobAds.IsInterstitialReady() && !unityAds.IsInterstitialReady())
        {
            Debug.Log("no Interstitial ad was ready");
        }
        else if (!adMobAds.IsInterstitialReady() && unityAds.IsInterstitialReady())
        {
            unityAds.ShowInterstitial();
        }
        else if (adMobAds.IsInterstitialReady() && !unityAds.IsInterstitialReady())
        {
            adMobAds.ShowInterstitial();
        }
        else
        {
            if (Random.Range(0, 10) > 4)
            {
                unityAds.ShowInterstitial();
            }
            else
            {
                // Admob Banner
                adMobAds.ShowInterstitial();
            }
        } 
    }


    public Question generateQuestion()
    {
        string[] colors = new string[6];
        for(var i=0; i<6; i++)
        {
            colors[i] = "#" + ColorUtility.ToHtmlStringRGB(Random.ColorHSV());
        }
        string ans = colors[(int)Random.Range(0, 5.999f)];
        return new Question(ans, colors);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
