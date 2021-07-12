using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Human : Moveable
{
    public delegate void HumanDied(Human human);
    public static event HumanDied OnHumanDied;

    private HumanGraphics graphics;

    protected override void Init()
    {
        base.Init();

        graphics = GetComponent<HumanGraphics>();

        GameController.Instance.OnLevelDone += MoveLeft;
    }

    public void Die()
    {
        OnHumanDied?.Invoke(this);
        //Destroy(this.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        graphics.SetIdle();
    }

    public override void StartMoving()
    {
        base.StartMoving();

        graphics.SetFloating();
    }

    private void MoveLeft()
    {
        rb.isKinematic = true;
        GetComponent<Collider>().isTrigger = true;
        graphics.SetWalking();

        transform.RotateAroundLocal(transform.up, -90f);

        StartCoroutine(MoveLeftCoroutine());
    }

    IEnumerator MoveLeftCoroutine()
    {
        while (true)
        {
            transform.position += (transform.forward * Time.deltaTime);

            yield return new WaitForSeconds(Time.deltaTime);
        }
    }

    private void OnDisable()
    {
        GameController.Instance.OnLevelDone -= MoveLeft;
    }
}
