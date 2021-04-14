using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menuActions : MonoBehaviour
{
  public void PlayGame()
    {
        Debug.Log("clicked");
        SceneManager.LoadScene(0);
    }
}
