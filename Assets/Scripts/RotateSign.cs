using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateSign : MonoBehaviour
{
    [SerializeField] Transform point, pointMirrored;
    [SerializeField] Transform canva;


    void Start()
    {
        
    }

    void Update()
    {
        if (Mathf.Sign(transform.localScale.x) == Mathf.Sign(1))
        {
            canva.transform.position = point.position;
            canva.localScale = Vector3.one;
        }
        else {
            canva.transform.position = pointMirrored.position;
            canva.localScale = new Vector3(-1,1,1);
        }

    }

}
