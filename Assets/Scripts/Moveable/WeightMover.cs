using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeightMover : MonoBehaviour
{
    public static WeightMover Instance { get; private set; }

    public bool isMovingObject { get; private set; }
    private Moveable objToMove;

    private Vector2 offset;

    private bool canDrag = true;

    private Vector3 lastPos = Vector3.zero;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        GameController.Instance.OnLevelDone += DisableDrag;
        GameController.Instance.OnLevelLost += DisableDrag;
    }

    private void Update()
    {
        if (canDrag)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Moveable obj = Check();
                if (obj != null)
                {
                    StartMoving(obj);
                }
            }

            if (Input.GetMouseButton(0) && isMovingObject)
            {
                Move(objToMove);
            }

            if (Input.GetMouseButtonUp(0) && isMovingObject)
            {
                StopMoving();
            }
        }
    }

    private Moveable Check()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.GetComponent<Moveable>() != null)
            {
                Debug.Log("HIT");
                return hit.collider.GetComponent<Moveable>();
            }
            else
            {
                return null;
            }
        }
        else
        {
            return null;
        }
    }

    private void StartMoving(Moveable obj)
    {
        isMovingObject = true;
        offset = (Vector2)obj.transform.position - GetMouseWorldPosition();

        lastPos = obj.transform.position;
        obj.StartMoving();
        objToMove = obj;
    }

    private void Move(Moveable obj)
    {
        Vector3 pos;

        Vector3 direction = (GetMouseWorldPosition() - (Vector2)lastPos) + offset;
        float distance = direction.magnitude;
        direction.Normalize();

        RaycastHit hit;
        Ray ray = new Ray(lastPos, direction);
        Debug.DrawRay(lastPos, direction, Color.red, 5);

        if (Physics.Raycast(ray, out hit, distance))
        {
            if (hit.collider.CompareTag("Obstacle"))
            {
                pos = lastPos;
            }
            else
            {
                pos = GetMouseWorldPosition() + offset;
            }
        }
        else
        {
            pos = GetMouseWorldPosition() + offset;
        }

        //Debug.Log(GetMouseWorldPosition());
        obj.transform.SetParent(null);
        obj.MoveTo(pos);
        Debug.Log(pos);
        lastPos = pos;
    }

    private void StopMoving()
    {
        isMovingObject = false;
        objToMove.StopMoving();
        objToMove = null;
    }

    private Vector2 GetMouseWorldPosition()
    {
        Vector2 mousePos = Input.mousePosition;
        return Camera.main.ScreenToWorldPoint(mousePos);
    }

    private void DisableDrag()
    {
        canDrag = false;
    }

    private void OnDisable()
    {
        GameController.Instance.OnLevelDone -= DisableDrag;
        GameController.Instance.OnLevelLost -= DisableDrag;
    }
}
