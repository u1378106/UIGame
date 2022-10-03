using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameStartManager : MonoBehaviour
{
    public GameObject startPage;
    public static bool isGameStart;

    public void StartGame()
    {
        startPage.SetActive(false);
        isGameStart = true;
    }
}
