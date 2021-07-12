using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Moveable : MonoBehaviour
{
    protected Rigidbody rb;

    [SerializeField] private int weight;

    [SerializeField]private Collider col;

    [SerializeField] private GameObject text;

    private Vector3 lastPos;
    private void Start()
    {
        Init();
    }

    protected virtual void Init()
    {
        rb = GetComponent<Rigidbody>();

        Instantiate(text).GetComponent<FollowText>().Init(transform, weight);
    }

    public void MoveTo(Vector3 position)
    {
        //Vector3 newPos = new Vector3(position.x + col.bounds.size.x / 2,
        //            position.y + col.bounds.size.y / 2,
        //            transform.position.z);
        
        //position.z = transform.position.z;
        rb.MovePosition(position);

    }

    public virtual void StartMoving()
    {
        rb.useGravity = false;
    }

    public virtual void StopMoving()
    {
        rb.useGravity = true;
    }

    public int GetWeight()
    {
        return weight;
    }

    public Vector3 GetBounds()
    {
        return col.bounds.size;
    }
}
