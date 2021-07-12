using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameController : MonoBehaviour
{
    public static GameController Instance { get; private set; }

    public delegate void LevelDone();
    public event LevelDone OnLevelDone;
    public delegate void LevelLost();
    public event LevelDone OnLevelLost;

    [SerializeField] private int peopleOnLevel;
    [SerializeField] private List<Human> peopleToRescue;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        peopleToRescue = new List<Human>();
        Human.OnHumanDied += Lose;
    }


    public void AddToList(Human human)
    {
        if (!peopleToRescue.Contains(human))
        {
            peopleToRescue.Add(human);
            Check();
        }
    }

    public void RemoveFromList(Human human)
    {
        if (peopleToRescue.Contains(human))
        {
            peopleToRescue.Remove(human);
        }
    }

    private void Check()
    {
        if (peopleToRescue.Count == peopleOnLevel)
        {
            Win();
        }
    }

    private void Win()
    {
        OnLevelDone?.Invoke();
        PlayerProgress.lastLevelIndex = SceneManager.GetActiveScene().buildIndex;
        SaveLoad.Save();
    }

    private void Lose(Human human)
    {
        OnLevelLost?.Invoke();
    }

    private void OnDisable()
    {
        Human.OnHumanDied -= Lose;
    }
}
