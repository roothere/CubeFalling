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
        if (!mover.moving) {
            return;
        }

        Direction facing = mover.GetFacing();

        Vector3 lPos = mover.levelPos;
        Vector3 lPosGrid = mover.GetLevelPosOnGrid();

        float delta = 0;
        float deltaHigh = 0;

        if (facing == Direction.rotateBack || facing == Direction.rotateForward) {
            delta = lPosGrid.z - lPos.z;
        } else {
            delta = lPosGrid.x - lPos.x;
        }

        if (facing == Direction.falling) {
            deltaHigh = lPosGrid.y - lPos.y;
        }

        if (delta == 0 || deltaHigh == 0) {
            return;
        }

        float move = mover.GetSpeed() * Time.fixedDeltaTime;
        move = Mathf.Min(move, Mathf.Abs(delta));
        float moveHigh = Mathf.Min(move, Mathf.Abs(deltaHigh));
        
        if (delta < 0) {
            move = -move;
        }

        if (deltaHigh < 0) {
            moveHigh = -moveHigh;
        }

        if (facing == Direction.rotateBack || facing == Direction.rotateForward) {
            lPos.z += move;
        } else {
            lPos.x += move;
        }

        if (facing == Direction.falling) {
            lPos.y += moveHigh;
        }

        mover.levelPos = lPos;
    }
}