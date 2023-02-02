using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewQuestionController : MonoBehaviour
{
    public InputField input;
    public Dropdown category;

    public Toggle radioQuestion, radioWish;

    public Text success,error;

    public void checkType(bool question){
        // if(question){
        //     radioQuestion.isOn=true;
        //     radioWish.isOn=false;
        // }
        // else{
        //     radioQuestion.isOn=false;
        //     radioWish.isOn=true;
        // }
    }


    public void save(){
        if(input.text.Length<2){
            if(IsInvoking(nameof(stopErrorAlert))){
                CancelInvoke(nameof(stopErrorAlert));

                stopErrorAlert();
            }
            error.gameObject.SetActive(true);
            Invoke(nameof(stopErrorAlert),2f);

            return;
        }
        string type="Q";
        int currentNumber=0;

        if(radioQuestion.isOn && !radioWish.isOn){
            type="Q";
            currentNumber=PlayerPrefs.GetInt("CurrentAdditionalQuestion");
            PlayerPrefs.SetInt("CurrentAdditionalQuestion",PlayerPrefs.GetInt("CurrentAdditionalQuestion")+1);
        }
        else if(radioWish.isOn && !radioWish.isOn){
            type="W";
            currentNumber=PlayerPrefs.GetInt("CurrentAdditionalWish");
            PlayerPrefs.SetInt("CurrentAdditionalWish",PlayerPrefs.GetInt("CurrentAdditionalWish")+1);
        } else{
            if(IsInvoking(nameof(stopErrorAlert))){
                CancelInvoke(nameof(stopErrorAlert));

                stopErrorAlert();
            }
            error.gameObject.SetActive(true);
            Invoke(nameof(stopErrorAlert),2f);

            return;
        }
        int cat=category.value;
        Debug.Log(cat);
        Debug.Log(type);

        string content=input.text.ToString();
        content.Replace("~","-");

        PlayerPrefs.SetString(cat.ToString()+"_"+type+"_additional_"+currentNumber.ToString(), content.ToString());

        Debug.Log(cat.ToString()+"_"+type+"_additional_"+currentNumber.ToString()+": "+PlayerPrefs.GetString(cat.ToString()+"_"+type+"_additional_"+currentNumber.ToString()).ToString());

        

        if(IsInvoking(nameof(stopSuccessAlert))){
            CancelInvoke(nameof(stopSuccessAlert));

            stopSuccessAlert();
        }

        success.gameObject.SetActive(true);
        Invoke(nameof(stopSuccessAlert),2f);

    
    }


    void stopSuccessAlert(){
        success.gameObject.SetActive(false);
    }

    void stopErrorAlert(){
        error.gameObject.SetActive(false);
    }

    public void openScene(int id){
        Application.LoadLevel(id);
    }
}
