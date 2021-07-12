using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialController : MonoBehaviour
{
    private Animator animator;

    [SerializeField] Elevator leftElevator;
    [SerializeField] Elevator rightElevator;
    [SerializeField] TutorialTrigger topTrigger;
    private void Start()
    {
        if (PlayerProgress.tutorialPassed)
        {
            this.gameObject.SetActive(false);
            return;
        }

        animator = GetComponent<Animator>();
        LeftWeight();

        leftElevator.OnWeightChanged += RightWeight;
        rightElevator.OnWeightChanged += Idle;
        topTrigger.OnTutorialTriggerEnter += Top;
        GameController.Instance.OnLevelDone += TutorialPassed;
    }



    private void OnDisable()
    {
        leftElevator.OnWeightChanged -= RightWeight;
        rightElevator.OnWeightChanged -= Idle;
        topTrigger.OnTutorialTriggerEnter += Top;
        GameController.Instance.OnLevelDone += TutorialPassed;
    }

    private void LeftWeight()
    {
        animator.SetTrigger("left");
    }

    private void Idle(int weight)
    {
        animator.SetTrigger("idle");
    }

    private void RightWeight(int weight)
    {
        animator.SetTrigger("right");
    }

    private void Top()
    {
        animator.SetTrigger("top");
    }

    private void TutorialPassed()
    {
        PlayerProgress.tutorialPassed = true;
        SaveLoad.Save();

        this.gameObject.SetActive(false);
    }
}
