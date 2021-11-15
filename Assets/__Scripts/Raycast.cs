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
        bool isKinematic = true;
        bool isFreezedRotation = false;
        bool isFalling = false;

        if (!Physics.Raycast(transform.position, Vector3.down, 0.5f)) {
            isKinematic = false;
            isFreezedRotation = true;
            isFalling = true;
        }

        mover.ChangeFall(isKinematic, isFreezedRotation, isFalling);
    }

    public bool IsDirectionBlocked(Vector3 dir) {
        return Physics.Raycast(transform.position, dir, 0.5f);
    }
}