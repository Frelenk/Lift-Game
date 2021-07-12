using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTrigger : MonoBehaviour
{
    public delegate void TutorialTriggerEnter();
    public TutorialTriggerEnter OnTutorialTriggerEnter;

    enum Type
    {
        Human,
        Weight
    }

    [SerializeField] private Type type;

    private void OnTriggerEnter(Collider other)
    {
        if (type == Type.Human)
        {
            if (other.GetComponent<Human>() != null)
            {
                OnTutorialTriggerEnter?.Invoke();
                Destroy(this.gameObject);
            }
        }
        else if (type == Type.Weight)
        {
            if (other.GetComponent<Weight>() != null)
            {
                OnTutorialTriggerEnter?.Invoke();
                Destroy(this.gameObject);
            }
        }
    }
}
