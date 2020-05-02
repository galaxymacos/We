using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleCamFollow : MonoBehaviour
{
    [SerializeField] private Transform target = null;

    [SerializeField] private float smoothTime = 5f;
    
    [SerializeField] private Vector3 offset = Vector3.zero;

    [SerializeField] private Vector3 currentVelocity;

    private void LateUpdate()
    {
        transform.LookAt(target);
        transform.position = Vector3.SmoothDamp(transform.position, offset+target.position, ref currentVelocity, smoothTime);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
