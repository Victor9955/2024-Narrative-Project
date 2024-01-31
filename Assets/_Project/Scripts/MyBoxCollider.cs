using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class MyBoxCollider : MonoBehaviour
{
    public event Action<Collision> onCollisionEnter;
    public event Action<Collision> onCollisionExit;
    public event Action<Collision> onCollisionStay;

    public event Action<Collider> onTriggerEnter;
    public event Action<Collider> onTriggerExit;
    public event Action<Collider> onTriggerStay;

    [HideInInspector] public BoxCollider colliderRef;

    private void Start()
    {
        colliderRef = GetComponent<BoxCollider>();
    }


    private void OnCollisionEnter(Collision collision)
    {
        onCollisionEnter?.Invoke(collision);
    }

    private void OnCollisionExit(Collision collision)
    {
        onCollisionExit?.Invoke(collision);
    }

    private void OnCollisionStay(Collision collision)
    {
        onCollisionStay?.Invoke(collision);
    }

    private void OnTriggerEnter(Collider other)
    {
        onTriggerEnter?.Invoke(other);
    }

    private void OnTriggerExit(Collider other)
    {
        onTriggerExit?.Invoke(other);
    }

    private void OnTriggerStay(Collider other)
    {
        onTriggerStay?.Invoke(other);
    }
}
