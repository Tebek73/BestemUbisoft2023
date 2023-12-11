using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Menu : MonoBehaviour
{
    
    public void quickPlayScene(){
        SceneManager.LoadScene(1);
    }

    public void setTournament(){
        SceneManager.LoadScene(2);
    }

    public void backToMenu(){
        SceneManager.LoadScene(0);
    }

    public void quit(){
        Application.Quit();
    }
}
