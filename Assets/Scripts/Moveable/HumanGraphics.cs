using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanGraphics : MonoBehaviour
{
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void SetFloating()
    {
        animator.SetTrigger("floating");
    }

    public void SetWalking()
    {
        animator.SetTrigger("walking");
    }

    public void SetPraying()
    {
        animator.SetTrigger("praying");
    }

    public void SetIdle()
    {
        animator.SetTrigger("idle");
    }
}
