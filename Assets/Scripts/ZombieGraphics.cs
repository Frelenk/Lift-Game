using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieGraphics : MonoBehaviour
{
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void SetWalk()
    {
        animator.SetTrigger("walk");
    }

    public void SetRun()
    {
        animator.SetTrigger("run");
    }
}
