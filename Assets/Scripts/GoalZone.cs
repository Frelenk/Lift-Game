using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Human human = other.GetComponent<Human>();
        if (human != null)
        {
            StartCoroutine(EnterCoroutine(human));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Human human = other.GetComponent<Human>();
        if (human != null)
        {
            StopAllCoroutines();
            GameController.Instance.RemoveFromList(human);
        }
    }

    IEnumerator EnterCoroutine(Human human)
    {
        yield return new WaitWhile(() => WeightMover.Instance.isMovingObject);

        GameController.Instance.AddToList(human);
    }
}
