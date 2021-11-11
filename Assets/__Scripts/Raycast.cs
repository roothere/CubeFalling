using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        if (Physics.Raycast(transform.position, Vector3.down, out hit, 0.75f)) {
            Debug.DrawRay(transform.position, Vector3.down * hit.distance, Color.green);
        } else {
            Debug.DrawRay(transform.position, Vector3.down * 1, Color.red);
            isKinematic = false;
            isFreezedRotation = true;
            isFalling = true;
        }
        
        mover.ChangeFall(isKinematic, isFreezedRotation, isFalling);
        /*if (Physics.Raycast(transform.position, Vector3.down, out hit, 1))
        {
            
        }*/
    }
}