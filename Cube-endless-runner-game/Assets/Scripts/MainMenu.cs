using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }

    public void PlayLvl(int i)
    {
        SceneManager.LoadScene(i); //scene 0 is the main menu.
    }

    public void QuitGame()
    {
        Application.Quit();
    }   

}
