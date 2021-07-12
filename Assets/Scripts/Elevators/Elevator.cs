using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    public delegate void WeightChanged(int newWeight);
    public event WeightChanged OnWeightChanged;

    private int weightOnElevator;
    [SerializeField] private int startWeight;

    private List<Moveable> objectsOnElevator;

    private void Start()
    {
        objectsOnElevator = new List<Moveable>();
        //AddWeight(startWeight);
    }

    private void OnCollisionStay(Collision collision)
    {
        Moveable obj = collision.collider.GetComponent<Moveable>();
        if (obj != null && !WeightMover.Instance.isMovingObject)
        {
            if (!objectsOnElevator.Contains(obj))
            {
                AddWeight(obj.GetWeight());

                obj.transform.SetParent(transform.parent);
                objectsOnElevator.Add(obj);
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        Moveable obj = collision.collider.GetComponent<Moveable>();
        if (obj != null)
        {
            StartCoroutine(RemoveWeighCoroutine(obj));
        }
    }

    IEnumerator RemoveWeighCoroutine(Moveable obj)
    {
        yield return new WaitWhile(() => WeightMover.Instance.isMovingObject);

        if (objectsOnElevator.Contains(obj))
        {
            RemoveWeight(obj.GetWeight());
            objectsOnElevator.Remove(obj);
        }
    }

    private void AddWeight(int weight)
    {
        weightOnElevator += weight;
        Debug.Log(weightOnElevator);

        OnWeightChanged?.Invoke(weightOnElevator);
    }

    private void RemoveWeight(int weight)
    {
        weightOnElevator -= weight;
        Debug.Log(weightOnElevator);

        OnWeightChanged?.Invoke(weightOnElevator);
    }

    public int GetWeightOnElevator()
    {
        return weightOnElevator;
    }
}
