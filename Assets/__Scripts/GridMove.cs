using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridMove : MonoBehaviour
{
    private IMover mover;

    void Awake()
    {
        mover = GetComponent<IMover>();
    }

    void FixedUpdate() {
        if (!mover.idling) {
            return;
        }
        Debug.Log("GRID_START");
        Direction facing = mover.GetFacing();

        Vector3 lPos = mover.levelPos;
        Vector3 lPosGrid = mover.PosOnGrid();

        float delta = 0;
        float deltaHigh = 0;

        delta = lPosGrid.z - lPos.z;
        delta = lPosGrid.x - lPos.x;

        if (facing == Direction.fall) {
            deltaHigh = lPosGrid.y - lPos.y;
        }

        if (delta == 0 || deltaHigh == 0) {
            return;
        }

        float move = mover.GetRotatingSpeed() * Time.fixedDeltaTime;
        move = Mathf.Min(move, Mathf.Abs(delta));
        float moveHigh = Mathf.Min(move, Mathf.Abs(deltaHigh));
        
        if (delta < 0) {
            move = -move;
        }

        if (deltaHigh < 0) {
            moveHigh = -moveHigh;
        }

        lPos.z += move;
        lPos.x += move;

        if (facing == Direction.fall) {
            lPos.y += moveHigh;
        }

        mover.levelPos = lPos;
    }
}
