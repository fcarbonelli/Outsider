using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using admob;

public class AdManager : MonoBehaviour {

	public static AdManager Instance { get; set; }
    int adcount = 0;

	private void Start ()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        Instance = this;

//#if UNITY_EDITOR
//#elif UNITY_ANDROID
        Admob.Instance().initAdmob("ca-app-pub-1896776195226797/9609563468", "ca-app-pub-1896776195226797/5737767062");
        LoadAd();
//#endif

    }

    public void LoadAd()
    {
        Admob.Instance().loadRewardedVideo("ca-app-pub-1896776195226797/3526099472");
        Admob.Instance().loadInterstitial();
    }

    public bool ShowVideo()
    {

        if (Admob.Instance().isRewardedVideoReady())
        {
            Admob.Instance().showRewardedVideo();
            return true;
        }
        else
        {
            LoadAd();            
            return false;
            
            
        }

    }

    public void ShowInterstitial()
    {
        adcount++;
        if (adcount >= 3)
        {
            adcount = 0;
            if (Admob.Instance().isInterstitialReady())
            {
               Admob.Instance().showInterstitial();          
            }
            else
            {
                LoadAd();
                
            }
        }
    }


}
