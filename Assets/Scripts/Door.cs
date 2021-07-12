using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();

        GameController.Instance.OnLevelDone += Open;
    }

    private void Open()
    {
        animator.SetTrigger("open");
    }

    private void OnDisable()
    {
        GameController.Instance.OnLevelDone -= Open;
    }
}
