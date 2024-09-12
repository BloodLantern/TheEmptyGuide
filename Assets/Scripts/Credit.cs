using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Credit : MonoBehaviour
{
    [SerializeField] float scrollingSpeed = 100f;

    private void Update()
    {
        foreach (RectTransform text in transform.GetComponentsInChildren<RectTransform>())
        {
            text.transform.position += Vector3.up * scrollingSpeed * Time.deltaTime;
        }
    }
}
