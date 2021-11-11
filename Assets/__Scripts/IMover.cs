using UnityEngine;

public interface IMover
{
    Direction GetFacing();
    bool moving { get; }
    float GetSpeed();
    float gridMult { get; }
    Vector3 levelPos { get; set; }
    Vector3 GetLevelPosOnGrid(float mult = -1);
}
