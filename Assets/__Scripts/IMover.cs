using UnityEngine;

public interface IMover
{
    Direction GetFacing();
    bool idling { get; }
    float GetRotatingSpeed();
    float GetFallingSpeed();
    float gridMult { get; }
    Vector3 levelPos { get; set; }
    Vector3 PosOnGrid(float mult = -1);
}
