using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Raycast : MonoBehaviour
{
    private Movement mover;
    private RaycastHit hit;

    void Awake() {
        mover = GetComponent<Movement>();
    }

    void FixedUpdate()
    {
        
    }

    public bool IsFalling() {
        return !Physics.Raycast(transform.position, Vector3.down, 0.75f);
    }

    public bool IsDirectionBlocked(Vector3 dir) {
        return Physics.Raycast(transform.position, dir, 0.5f);
    }
}