using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialController2 : MonoBehaviour
{
    private Animator animator;
    [SerializeField] private Elevator leftElevator;
    [SerializeField] private TutorialTrigger rightTrigger;
    [SerializeField] private TutorialTrigger leftTrigger;

    private void Start()
    {
        if (PlayerProgress.tutorial2Passed)
        {
            this.gameObject.SetActive(false);
            return;
        }

        animator = GetComponent<Animator>();

        leftElevator.OnWeightChanged += ShowTutorial;
        rightTrigger.OnTutorialTriggerEnter += TutorialPassed;
        leftTrigger.OnTutorialTriggerEnter += TutorialPassed;
    }

    private void ShowTutorial(int weight)
    {
        animator.SetTrigger("show");
    }

    private void TutorialPassed()
    {
        leftElevator.OnWeightChanged -= ShowTutorial;

        PlayerProgress.tutorial2Passed = true;
        SaveLoad.Save();

        Destroy(this.gameObject);
    }

    private void OnDisable()
    {
        rightTrigger.OnTutorialTriggerEnter -= TutorialPassed;
        leftTrigger.OnTutorialTriggerEnter -= TutorialPassed;
    }
}
