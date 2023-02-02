using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteNewQuestionsController : MonoBehaviour
{
    public GameObject deleteConfirmPanel;


    public void delete(){
        deleteConfirmPanel.SetActive(!deleteConfirmPanel.activeSelf);
    }

    public void confirmDelete(){
        PlayerPrefs.DeleteAll();
        deleteConfirmPanel.SetActive(false);
    }

    public void cancelDelete(){
        deleteConfirmPanel.SetActive(false);
    }
}
