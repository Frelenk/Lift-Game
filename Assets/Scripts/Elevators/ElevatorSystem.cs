using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorSystem : MonoBehaviour
{
    [System.Serializable]
    private struct Floor
    {
        public Transform position;
        public int weightDifference;
    }

    [SerializeField] private Transform[] blocks;
    [SerializeField] private Elevator leftElevator;
    [SerializeField] private Elevator rightElevator;

    [Header("Positions")]
    private Dictionary<int, Transform> leftElevatorFloorPositions;
    [SerializeField] private Floor[] leftElevatorFloors;
    private Dictionary<int, Transform> rightElevatorFloorPositions;
    [SerializeField] private Floor[] rightElevatorFloors;

    [Header("Bounds")]
    [SerializeField] private int leftHighest; 
    [SerializeField] private int leftLowest; 
    [SerializeField] private int rightHighest; 
    [SerializeField] private int rightLowest; 

    [SerializeField] private float elevatorDefaultSpeed;

    private void Start()
    {
        InitDictionary();

        leftElevator.OnWeightChanged += LeftElevatorChanged;
        rightElevator.OnWeightChanged += RightElevatorChanged;
    }

    private void InitDictionary()
    {
        leftElevatorFloorPositions = new Dictionary<int, Transform>();
        rightElevatorFloorPositions = new Dictionary<int, Transform>();

        foreach (Floor f in leftElevatorFloors)
        {
            leftElevatorFloorPositions.Add(f.weightDifference, f.position);
        }
        foreach (Floor f in rightElevatorFloors)
        {
            rightElevatorFloorPositions.Add(f.weightDifference, f.position);
        }
    }

    private void LeftElevatorChanged(int newWeight)
    {
        RecalculateElevators(newWeight, rightElevator.GetWeightOnElevator());
    }

    private void RightElevatorChanged(int newWeight)
    {
        RecalculateElevators(leftElevator.GetWeightOnElevator(), newWeight);
    }

    private void RecalculateElevators(int leftWeight, int rightWeight)
    {
        int leftToRightDifference = leftWeight - rightWeight;
        int rightToLeftDifference = rightWeight - leftWeight;

        if (-leftToRightDifference > leftHighest)
        {
            leftToRightDifference = -leftHighest;
        }
        else if (-leftToRightDifference < leftLowest)
        {
            leftToRightDifference = -leftLowest;
        }

        if (-rightToLeftDifference > rightHighest)
        {
            rightToLeftDifference = -rightHighest;
        }
        else if (-rightToLeftDifference < rightLowest)
        {
            rightToLeftDifference = -rightLowest;
        }

        Transform leftPos;
        Transform rightPos;
        if (leftElevatorFloorPositions.TryGetValue(-leftToRightDifference, out leftPos))
        {
            this.StopAllCoroutines();
            StartCoroutine(MoveElevatorCoroutine(leftElevator, leftPos, leftToRightDifference));
        }
        if (rightElevatorFloorPositions.TryGetValue(-rightToLeftDifference, out rightPos))
        {
            StartCoroutine(MoveElevatorCoroutine(rightElevator, rightPos, leftToRightDifference));
        }
        Debug.Log("CHECK");

        //if (leftPos != null && rightPos != null)
        //{
        //    this.StopAllCoroutines();
        //    StartCoroutine(MoveElevatorCoroutine(leftElevator, leftPos));
        //    StartCoroutine(MoveElevatorCoroutine(rightElevator, rightPos));
        //}
    }

    IEnumerator MoveElevatorCoroutine(Elevator elevator, Transform pos, int difference)
    {
        Vector3 startPoint = elevator.transform.parent.position;
        Vector3 destPoint = new Vector3(elevator.transform.parent.position.x,
            pos.position.y,
            elevator.transform.parent.position.z);

        float distance = (elevator.transform.parent.position - destPoint).magnitude;
        float timeToPassDistance = distance / elevatorDefaultSpeed;
        float time = 0;

        if (timeToPassDistance == 0)
        {
            timeToPassDistance = .01f;
        }

        while (time <= timeToPassDistance)
        {
            elevator.transform.parent.position = Vector3.Lerp(startPoint, destPoint, time / timeToPassDistance);
            time += Time.deltaTime;

            Rotate(difference);
            yield return new WaitForSeconds(Time.deltaTime);
        }

        elevator.transform.parent.position = destPoint;
    }

    private void Rotate(int difference)
    {
        if (difference > 0)
        {
            foreach(Transform t in blocks)
            {
                t.rotation *= Quaternion.AngleAxis(elevatorDefaultSpeed * 5, t.forward);
            }
        } 
        else if (difference < 0)
        {
            foreach (Transform t in blocks)
            {
                t.rotation *= Quaternion.AngleAxis(-elevatorDefaultSpeed * 5, t.forward);
            }
        }
        else
        {
            return;
        }
    }

    private void OnDisable()
    {
        leftElevator.OnWeightChanged -= LeftElevatorChanged;
        rightElevator.OnWeightChanged -= RightElevatorChanged;
    }
}
