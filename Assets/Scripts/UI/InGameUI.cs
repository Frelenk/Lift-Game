using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IngameUI : MonoBehaviour
{
    [SerializeField] private GameObject winPanel;
    [SerializeField] private GameObject losePanel;

    private void Awake()
    {
        Application.targetFrameRate = 60;
    }

    private void Start()
    {
        if (SceneManager.sceneCount > 1)
        {
            gameObject.SetActive(false);
            return;
        }

        GameController.Instance.OnLevelDone += WinPanelPopUp;
        GameController.Instance.OnLevelLost += LosePanelPopUp;
    }

    private void LosePanelPopUp()
    {
        StartCoroutine(PopUp(losePanel));
    }

    private void WinPanelPopUp()
    {
        StartCoroutine(PopUp(winPanel));
    }

    IEnumerator PopUp(GameObject panel)
    {
        yield return new WaitForSeconds(2f);

        panel.SetActive(true);
    }

    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void NextLevel()
    {
        if (SceneManager.GetActiveScene().buildIndex < 7)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else
        {
            SceneManager.LoadScene(2);
        }
    }

    public void SkipLevel()
    {
        PlayerProgress.lastLevelIndex = SceneManager.GetActiveScene().buildIndex;
        SaveLoad.Save();
        NextLevel();
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(1); 
    }

    private void OnDisable()
    {
        GameController.Instance.OnLevelDone -= WinPanelPopUp;
        GameController.Instance.OnLevelLost -= LosePanelPopUp;
    }
}
