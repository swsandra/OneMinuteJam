using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
    public void MainMenu() {
        SceneManager.LoadScene("Title");
    }

    public void SingleBandMode() {
        SceneManager.LoadScene("EasyMode");
    }

    public void DoubleBandMode() {
        SceneManager.LoadScene("HardMode");
    }

    public void Quit(){
        Application.Quit();
    }
}
