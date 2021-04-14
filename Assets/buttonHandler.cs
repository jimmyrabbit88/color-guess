using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class buttonHandler : MonoBehaviour
{
    gameLoop gl;
    public void selectThis(int value)
    {
        print(value);
        GameObject.FindObjectOfType<gameLoop>().onSelect(value);
    }

    public void startClick()
    {
        GameObject.FindObjectOfType<gameLoop>().StartGame();
    }

    public void clickLifeline()
    {
        GameObject.FindObjectOfType<gameLoop>().LifeLine();
    }

    public void clickVideoAd()
    {
        GameObject.FindObjectOfType<gameLoop>().watchVideoAd();
    }
    public void clickSettings()
    {
        SceneManager.LoadScene(1);
    }
}
