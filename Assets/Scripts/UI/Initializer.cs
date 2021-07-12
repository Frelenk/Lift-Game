using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Initializer : MonoBehaviour
{
    private void Start()
    {
        LoadSave();
        InitLevel();
    }

    private void LoadSave()
    {
        SaveLoad.Load();
    }

    private void InitLevel()
    {
        Time.timeScale = 1f;
        if (PlayerProgress.lastLevelIndex < 7)
        {
            SceneManager.LoadScene(PlayerProgress.lastLevelIndex + 1);
        }
        else
        {
            SceneManager.LoadScene(2);
        }
    }
}
