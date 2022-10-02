using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public GameObject info;

    public void LoadLevel(int level)
    {
        SceneManager.LoadScene(1);
    }

    public void QuitApp()
    {
        Application.Quit();
    }

    public void BackPressed()
    {
        info.SetActive(false);
    }

    public void InfoPressed()
    {
        info.SetActive(true);
    }
}
