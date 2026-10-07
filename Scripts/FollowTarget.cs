using System;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    [SerializeField] private Transform target;

    private void FixedUpdate()
    {
        transform.position = target.position;
    }

    public Transform GetTarget()
    {
        return target;
    }
}
