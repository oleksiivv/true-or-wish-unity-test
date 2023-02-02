using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using GoogleMobileAds.Api;
using UnityEngine.Advertisements;

public class GameInteraction : MonoBehaviour
{
    public GameObject chooseOption;

    public Text chosenOption;
    public Text category;

    public Text question;

    public QuestionsDisplay questionsManager;
    public WishesDisplay wishesManager;

    public ParticleSystem showEffect;

    public Animator chooseOptionPanel;

    private string admob_app_id="ca-app-pub-4962234576866611~2099524194";
    private string unity_app_id="4236417";

    private InterstitialAd intersitional;
    private string intersitionalId="ca-app-pub-4962234576866611/9729961572";

    private BannerView banner;
    private string bannerId="ca-app-pub-4962234576866611/2077814212";


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
        MobileAds.Initialize(initStatus => { });
        
        RequestConfigurationAd();
        RequestBannerAd();

        Advertisement.Initialize(unity_app_id,false);
    }

    AdRequest AdRequestBannerBuild(){
        return new AdRequest.Builder().Build();
    }
    public void RequestBannerAd(){
        banner=new BannerView(bannerId,AdSize.Banner,AdPosition.Bottom);
        AdRequest request = AdRequestBannerBuild();
        banner.LoadAd(request);
    }

    public void DestroyBanner(){
        if(banner!=null){
            banner.Destroy();
        }
    }

    public static int showAddCnt=0;

    public void nextPlayer(){
        question.text="";
        chooseOption.SetActive(true);

        chooseOptionPanel.SetBool("close",false);
        chooseOptionPanel.SetBool("open",true);

        if(showAddCnt%2==0){
            if(intersitional.IsLoaded()){
                intersitional.Show();
            }
            else{
                if(Advertisement.IsReady()){
                    Advertisement.Show("Interstitial_Android");
                }
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


      void RequestConfigurationAd(){
          intersitional=new InterstitialAd(intersitionalId);
          AdRequest request=AdRequestBuild();
          intersitional.LoadAd(request);

          intersitional.OnAdLoaded+=this.HandleOnAdLoaded;
          intersitional.OnAdOpening+=this.HandleOnAdOpening;
          intersitional.OnAdClosed+=this.HandleOnAdClosed;

      }


      public bool showIntersitionalAd(){
          if(intersitional.IsLoaded()){
              intersitional.Show();

              return true;
          }

          return false;
      }

      private void OnDestroy(){
          DestroyIntersitional();

          intersitional.OnAdLoaded-=this.HandleOnAdLoaded;
          intersitional.OnAdOpening-=this.HandleOnAdOpening;
          intersitional.OnAdClosed-=this.HandleOnAdClosed;

      }

      private void HandleOnAdClosed(object sender, EventArgs e)
      {
          intersitional.OnAdLoaded-=this.HandleOnAdLoaded;
          intersitional.OnAdOpening-=this.HandleOnAdOpening;
          intersitional.OnAdClosed-=this.HandleOnAdClosed;

            RequestConfigurationAd();

        
      }

      private void HandleOnAdOpening(object sender, EventArgs e)
    {
        
    }

    private void HandleOnAdLoaded(object sender, EventArgs e)
    {
        
    }

 

     public void DestroyIntersitional(){
         intersitional.Destroy();
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
