using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private void Start()
    {
        SaveLoad.Load();

        LoadLevel();
    }

    private void LoadLevel()
    {
        Time.timeScale = 0f;
        if (PlayerProgress.lastLevelIndex < 7)
        {
            SceneManager.LoadScene(PlayerProgress.lastLevelIndex + 1, LoadSceneMode.Additive);
        }
        else
        {
            SceneManager.LoadScene(2, LoadSceneMode.Additive);
        }
    }

    public void Play()
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
