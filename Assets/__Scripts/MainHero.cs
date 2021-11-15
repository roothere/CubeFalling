using System;
using UnityEngine;


public class MainHero : MonoBehaviour, IMover
{
    private Direction direction;
    private Movement movement;

    void Awake() {
        movement = GetComponent<Movement>();
    }
    public Direction GetFacing()
    {
        return movement.direction;
    }

    public bool moving
    {
        get { return (direction != Direction.idle); }
    }
    public float GetSpeed()
    {
        return movement.rollSpeed;
    }

    public float gridMult
    {
        get { return movement.gridMult; }
    }
    public Vector3 levelPos {
        get { return movement.levelPos; }
        set { movement.levelPos = value; }
    }
    public Vector3 GetLevelPosOnGrid(float mult = -1)
    {
        return movement.GetLevelPosOnGrid(mult);
    }
}
