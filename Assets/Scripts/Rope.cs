using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour
{
    [SerializeField] private Transform leftElevator;
    [SerializeField] private Transform rightElevator;
    [SerializeField] private Transform leftBlock;
    [SerializeField] private Transform rightBlock;

    private LineRenderer lr;

    private void Start()
    {
        lr = GetComponent<LineRenderer>();

        SetupRope();
    }

    private void LateUpdate()
    {
        UpdateRope();
    }

    private void SetupRope()
    {
        lr.positionCount = 4;
    }

    private void UpdateRope()
    {
        lr.SetPosition(0, leftElevator.position);
        lr.SetPosition(1, leftBlock.position);
        lr.SetPosition(2, rightBlock.position);
        lr.SetPosition(3, rightElevator.position);
    }
}
