using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    private enum Mode
    {
        Wandering,
        Attack
    }

    private Mode mode = Mode.Wandering;

    [SerializeField] private float speed;
    [SerializeField] private float minTimeToChangeDir;
    [SerializeField] private float maxTimeToChangeDir;

    private Vector3 direction;
    private Rigidbody rb;

    private Human currentTarget = null;

    private ZombieGraphics graphics;

    private void Start()
    {
        graphics = GetComponent<ZombieGraphics>();
        rb = GetComponent<Rigidbody>();
        
        SetWandering();

        Human.OnHumanDied += SetAttack;
    }
     
    private void Update()
    {
        if (mode == Mode.Wandering)
        {
            Walk();
        }
        else if (mode == Mode.Attack)
        {
            Attack(currentTarget);
        }
    }

    private void SetAttack(Human human)
    {
        graphics.SetRun();
        mode = Mode.Attack;

        currentTarget = human;

        Quaternion rot = transform.rotation;

        transform.LookAt(human.transform.position, Vector3.up);
        rot.x = 0;
        rot.z = 0;
    }

    private void Attack(Human target)
    {
        StopAllCoroutines();

        Vector3 pos = target.transform.position;
        pos.y = transform.position.y;
        transform.position = Vector3.MoveTowards(transform.position, pos, speed * 4 * Time.deltaTime);
    }

    private void SetWandering()
    {
        //graphics.SetWalk();
        direction = transform.rotation * -transform.forward;
        mode = Mode.Wandering;

        StartCoroutine(ChangeDirection());
    }

    private void Walk()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }

    IEnumerator ChangeDirection()
    {
        while (true)
        {
            float timeToChange = Random.Range(minTimeToChangeDir, maxTimeToChangeDir);

            yield return new WaitForSeconds(timeToChange);
         
            transform.rotation = Quaternion.Inverse(transform.rotation);
        }
    }

    private void OnDisable()
    {
        Human.OnHumanDied -= SetAttack;
    }
}

