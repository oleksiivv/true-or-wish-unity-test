using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using GoogleMobileAds.Api;
using UnityEngine.Advertisements;

public class GameInteraction : InterstitialVideo
{
    public GameObject chooseOption;

    public Text chosenOption;
    public Text category;

    public Text question;

    public QuestionsDisplay questionsManager;
    public WishesDisplay wishesManager;

    public ParticleSystem showEffect;

    public Animator chooseOptionPanel;
    private InterstitialAd intersitional;
    private BannerView banner;

#if UNITY_IOS
    private string admob_app_id="ca-app-pub-4962234576866611~4574307279";
    private string unity_app_id="4236416";

    private string intersitionalId="ca-app-pub-4962234576866611/9147158862";

    private string bannerId="ca-app-pub-4962234576866611/5399485546";
#else
    private string admob_app_id="ca-app-pub-4962234576866611~2099524194";
    private string unity_app_id="4236417";

    private string intersitionalId="ca-app-pub-4962234576866611/9729961572";

    private string bannerId="ca-app-pub-4962234576866611/2077814212";
#endif

    void Start(){
        switch(CategoryController.currentCategory){
            case categories.child:
            category.text="Категорія: для дітей";
            break;

            case categories.adult:
            category.text="Категорія: 18+";
            break;

            case categories.interesting:
            category.text="Категорія: цікаве";
            break;

            default:
            category.text="Категорія: ВСЕ";
            break;

        }

        RequestConfiguration requestConfiguration =
            new RequestConfiguration.Builder()
            .SetSameAppKeyEnabled(true).build();
        MobileAds.SetRequestConfiguration(requestConfiguration);

        // Initialize the Google Mobile Ads SDK.
        MobileAds.Initialize(initStatus => {
            LoadLoadInterstitialAd();
            CreateBannerView();
            LoadBannerAd();
        });

        Advertisement.Initialize(unity_app_id,false);

        this.LoadAd();
    }

    AdRequest AdRequestBannerBuild(){
        return new AdRequest.Builder().Build();
    }
    
    private InterstitialAd _interstitialAd;

    private BannerView _bannerView;
    
    public void LoadLoadInterstitialAd()
    {
        // Clean up the old ad before loading a new one.
        if (_interstitialAd != null)
        {
                _interstitialAd.Destroy();
                _interstitialAd = null;
        }

        Debug.Log("Loading the interstitial ad.");

        // create our request used to load the ad.
        var adRequest = new AdRequest();

        // send the request to load the ad.
        InterstitialAd.Load(intersitionalId, adRequest,
            (InterstitialAd ad, LoadAdError error) =>
            {
                // if error is not null, the load request failed.
                if (error != null || ad == null)
                {
                    Debug.LogError("interstitial ad failed to load an ad " +
                                    "with error : " + error);
                    return;
                }

                Debug.Log("Interstitial ad loaded with response : "
                            + ad.GetResponseInfo());

                _interstitialAd = ad;
            });
    }


      public bool showIntersitionalGoogleAd(){
        if (_interstitialAd != null && _interstitialAd.CanShowAd())
        {
            Debug.Log("Showing interstitial ad.");
            _interstitialAd.Show();

            return true;
        }
        else
        {
            return false;
        }
      }

    public static int showAddCnt=0;

    public void nextPlayer(){
        question.text="";
        chooseOption.SetActive(true);

        chooseOptionPanel.SetBool("close",false);
        chooseOptionPanel.SetBool("open",true);

        if(showAddCnt%2==0){
            if(! showIntersitionalGoogleAd()){
                this.ShowAd();
            }
        }
        showAddCnt++;
    }

    public void chooseTruth(){
        chosenOption.text="Правда";
        Invoke(nameof(displayQuestion),0.5f);

        chooseOptionPanel.SetBool("open",false);
        chooseOptionPanel.SetBool("close",true);

        showEffect.Play();
    }

    public void chooseWish(){
        chosenOption.text="Дія";
        Invoke(nameof(displayWish),0.5f);

        chooseOptionPanel.SetBool("open",false);
        chooseOptionPanel.SetBool("close",true);

        showEffect.Play();
    }


    public void displayQuestion(){
        question.gameObject.SetActive(true);
        question.text=questionsManager.getRandom();
    }

    public void displayWish(){
        question.gameObject.SetActive(true);
        question.text=wishesManager.getRandom();
    }

    public void openScene(int id){
        Application.LoadLevel(id);
    }


    ///


    AdRequest AdRequestBuild(){
         return new AdRequest.Builder().Build();
     }

      public bool showIntersitionalAd(){
          return showIntersitionalGoogleAd();
      }

          public void CreateBannerView()
    {
        Debug.Log("Creating banner view");

        // If we already have a banner, destroy the old one.
        if (_bannerView != null)
        {
            DestroyBannerView();
        }

        // Create a 320x50 banner at top of the screen
        _bannerView = new BannerView(bannerId, AdSize.Banner, AdPosition.Bottom);
    }

    public void LoadBannerAd()
    {
        // create an instance of a banner view first.
        if(_bannerView == null)
        {
            CreateBannerView();
        }

        // create our request used to load the ad.
        var adRequest = new AdRequest();

        // send the request to load the ad.
        Debug.Log("Loading banner ad.");
        _bannerView.LoadAd(adRequest);
    }

    public void DestroyBannerView()
    {
        if (_bannerView != null)
        {
            Debug.Log("Destroying banner view.");
            _bannerView.Destroy();
            _bannerView = null;
        }
    }

    //baner

    // public void RequestBannerAd(){
    //     banner=new BannerView(bannerId,AdSize.Banner,AdPosition.Bottom);
    //     AdRequest request = AdRequestBannerBuild();
    //     banner.LoadAd(request);
    // }

    // public void DestroyBanner(){
    //     if(banner!=null){
    //         banner.Destroy();
    //     }
    // }



    // AdRequest AdRequestBannerBuild(){
    //     return new AdRequest.Builder().Build();
    // }
}
