using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;

public class MenuController : MonoBehaviour
{
    public GameObject categoriesPanel;
    public GameObject internetError;

    public GameObject loadingPanel;

    private string admob_app_id="ca-app-pub-4962234576866611~2099524194";

    private BannerView banner;
    private string bannerId="ca-app-pub-4962234576866611/2077814212";

    void Start(){
        RequestConfiguration requestConfiguration =
            new RequestConfiguration.Builder()
            .SetSameAppKeyEnabled(true).build();
        MobileAds.SetRequestConfiguration(requestConfiguration);

        // Initialize the Google Mobile Ads SDK.
        MobileAds.Initialize(initStatus => { });
        
        RequestBannerAd();
    }

    AdRequest AdRequestBuild(){
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



    AdRequest AdRequestBannerBuild(){
        return new AdRequest.Builder().Build();
    }

    public void startGame(){
        // if(Application.internetReachability == NetworkReachability.NotReachable)
        // {
        //     internetError.SetActive(true);
        // }
        // else{
        //     internetError.SetActive(false);
        //     categoriesPanel.SetActive(true);
        // }

        categoriesPanel.SetActive(true);
    }

    public void refreshConnection(){
        startGame();
    }

    public void chooseCategoryChild(){
        CategoryController.currentCategory=categories.child;
        openScene(1);

    }

    public void chooseCategoryAdult(){
        CategoryController.currentCategory=categories.adult;
        openScene(1);

    }

    public void chooseCategoryInteresting(){
        CategoryController.currentCategory=categories.interesting;
        openScene(1);
    }

    public void chooseCategoryAll(){
        CategoryController.currentCategory=categories.all;
        openScene(1);
    }


    // IEnumerator loadQuestions(categories ctg){

    //     loadingPanel.SetActive(true);
    //     QuestionsDisplay.questions.Clear();
    //     WishesDisplay.wishes.Clear();

    //     do{
    //         StartCoroutine(QuestionsDisplay.getAllQuestions(ctg));
    //         StartCoroutine(WishesDisplay.getAllWishes(ctg));

    //         yield return null;

    //     }while(QuestionsDisplay.questions.Count==0 || WishesDisplay.wishes.Count==0);

    //     Debug.Log("Questions: "+QuestionsDisplay.questions.Count.ToString());
    //     Debug.Log("Wishes: "+WishesDisplay.wishes.Count.ToString());

    //     Application.LoadLevel(1);
    // }

    


    public void openScene(int id){
        loadingPanel.SetActive(true);
        Application.LoadLevel(id);
    }

}
