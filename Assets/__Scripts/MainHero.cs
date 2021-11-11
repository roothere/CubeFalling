using System;
using UnityEngine;


public class MainHero : MonoBehaviour, IMover
{
    private Direction direction;
    private Movement move;

    void Awake() {
        move = GetComponent<Movement>();
    }
    public Direction GetFacing()
    {
        return move.direction;
    }

    public bool moving
    {
        get { return (direction != Direction.idle); }
    }
    public float GetSpeed()
    {
        return move.rollSpeed;
    }

    public float gridMult
    {
        get { return move.gridMult; }
    }
    public Vector3 levelPos {
        get { return move.levelPos; }
        set { move.levelPos = value; }
    }
    public Vector3 GetLevelPosOnGrid(float mult = -1)
    {
        return move.GetLevelPosOnGrid(mult);
    }
}
