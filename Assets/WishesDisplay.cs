using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.IO;

public class WishesDisplay : MonoBehaviour
{
    public List<string> wishesChild = new List<string>();
    public List<string> wishesAdult = new List<string>();
    public List<string> wishesInteresting = new List<string>();

    public List<string> wishes = new List<string>();

    public List<string> usedWishes = new List<string>();

    void Start(){
        //StartCoroutine(getAllWishes(categories.child, wishesChild));
        //StartCoroutine(getAllWishes(categories.adult, wishesAdult));
        //StartCoroutine(getAllWishes(categories.interesting, wishesInteresting));
        
        /*wishesChild.Remove(wishesChild[wishesChild.Count-1]);
        wishesAdult.Remove(wishesAdult[wishesAdult.Count-1]);
        wishesInteresting.Remove(wishesInteresting[wishesInteresting.Count-1]);*/

        wishesChild.Clear();
        wishesAdult.Clear();
        wishesInteresting.Clear();

        wishesChild = LoadChildFromJson();
        wishesAdult = LoadAdultsFromJson();
        wishesInteresting = LoadInterestingFromJson();

        loadAdditinalWishes(categories.child, wishesChild);
        loadAdditinalWishes(categories.adult, wishesAdult);
        loadAdditinalWishes(categories.interesting, wishesInteresting);
        

        setWishes(CategoryController.currentCategory);
        
        loadAdditinalWishes(categories.all, wishes); 
    }


    public string getRandom(){
        if(wishes.Count==0){
            return getRandom();
        }

        else{
            string randomWish=wishes[Random.Range(0,wishes.Count)];
            if(usedWishes.Contains(randomWish) && usedWishes.Count*2<wishes.Count){
               return getRandom(); 
            }

            else{
                return randomWish;
            }
        }
    }

    public void setWishes(categories ctg){
        wishes.Clear();
        switch(ctg){
            case categories.child:
                foreach(var child in wishesChild)wishes.Add(child);
            break;

            case categories.adult:
                foreach(var child in wishesAdult)wishes.Add(child);
            break;

            case categories.interesting:
                foreach(var child in wishesInteresting)wishes.Add(child);
            break;

            default:
            foreach(var child in wishesChild)wishes.Add(child);
            foreach(var child in wishesAdult)wishes.Add(child);
            foreach(var child in wishesInteresting)wishes.Add(child);
            break;


        }
    }
    


    public IEnumerator getAllWishes(categories ctg, List<string> list){
        list.Clear();
        string url="https://unity-questions-game.herokuapp.com/wishes.php";
        switch(ctg){
            case categories.child:
            url="https://unity-questions-game.herokuapp.com/wishes-childs.php";
            break;

            case categories.adult:
            url="https://unity-questions-game.herokuapp.com/wishes-adults.php";
            break;

            case categories.interesting:
            url="https://unity-questions-game.herokuapp.com/wishes-interesting.php";
            break;

        }
        using(UnityWebRequest www = UnityWebRequest.Get(url)){
            yield return www.Send();

            if(www.isNetworkError || www.isHttpError){
                Debug.Log(www.error);
            }
            else{

                string result=www.downloadHandler.text;
                string[] res = result.Split('~');

                foreach(var str in res){
                    string[] splittedStr = str.Split('/');

                    list.Add(splittedStr[0]);
                }
                //Debug.Log(www.downloadHandler.text);
                //Debug.Log(www.downloadHandler.data);
            }
        }
    }

    void loadAdditinalWishes(categories ctg, List<string> list){
        int n=0;

        while(PlayerPrefs.GetString(((int)ctg).ToString()+"_W_additional_"+n.ToString(),"null")!="null"){
            string w=PlayerPrefs.GetString(((int)ctg).ToString()+"_W_additional_"+n.ToString());

            Debug.Log(w);
            list.Add(w);
            n++;
        }

    }

    private void saveChild()
    {
        string jsonString = JsonUtility.ToJson(new SerializableList(){data=wishesChild});

        Debug.Log(jsonString);

        File.WriteAllText("w_child.json", jsonString);
    }

    private void saveAdult()
    {
        string jsonString = JsonUtility.ToJson(new SerializableList(){data=wishesAdult});

        File.WriteAllText("w_adult.json", jsonString);
    }

    private void saveInteresting()
    {
        string jsonString = JsonUtility.ToJson(new SerializableList(){data=wishesInteresting});

        File.WriteAllText("w_interesting.json", jsonString);
    }

    public List<string> LoadAdultsFromJson()
    {
        TextAsset theList = (TextAsset)Resources.Load("w_adult", typeof (TextAsset));
        string json = theList.text;
            
        JSONObject obj = JsonUtility.FromJson<JSONObject>(json)!;

        Debug.Log(obj.data.Count);

        return obj.data;
    }

    public List<string> LoadChildFromJson()
    {
        TextAsset theList = (TextAsset)Resources.Load("w_child", typeof (TextAsset));
        string json = theList.text;
            
        JSONObject obj = JsonUtility.FromJson<JSONObject>(json)!;

        Debug.Log(obj.data.Count);

        return obj.data;
    }

    public List<string> LoadInterestingFromJson()
    {
        TextAsset theList = (TextAsset)Resources.Load("w_interesting", typeof (TextAsset));
        string json = theList.text;
        
        JSONObject obj = JsonUtility.FromJson<JSONObject>(json)!;

        Debug.Log(obj.data.Count);

        return obj.data;
    }
}
