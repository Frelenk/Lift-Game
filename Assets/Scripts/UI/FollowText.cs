using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FollowText : MonoBehaviour
{
    private RectTransform rectTransform;

    [SerializeField] private Transform objToFollow;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    private void LateUpdate()
    {
        rectTransform.position = Camera.main.WorldToScreenPoint(objToFollow.position);
    }

    public void Init(Transform obj, int weight)
    {
        transform.SetParent(FindObjectOfType<Canvas>().transform);

        objToFollow = obj;
        GetComponent<Text>().text = weight.ToString();
    }
}
