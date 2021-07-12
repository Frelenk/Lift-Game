using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Human human = other.GetComponent<Human>();
        if (human != null)
        {
            human.Die();
        }
    }
}
