using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Raycast : MonoBehaviour, IChecker
{
    private static float distance = 0.5f;
    private RaycastHit hitinfo;

    public bool IsFalling(Vector3 startPoint) {
        return !Physics.Raycast(startPoint, Vector3.down, distance);
    }

    public bool IsDirectionBlocked(Vector3 startPoint, Vector3 direction)
    {
        return Physics.Raycast(startPoint, direction, distance);
    }
    public bool IsDirectionLift(Vector3 startPoint, Vector3 direction)
    {
        Vector3 endPoint = direction + 2 * Vector3.up;
        float magnitude = Vector3.Magnitude(startPoint - endPoint);
        bool isDirectionLift = !Physics.Raycast(startPoint, endPoint, magnitude) &&
                               !Physics.Raycast(startPoint - Vector3.back, endPoint - Vector3.back, magnitude);
        return isDirectionLift;
    }
    public bool IsDirectionPartiallyBlocked(Vector3 startPoint, Vector3 direction)
    {
        Vector3 endPoint = direction + 2 * Vector3.up;
        float magnitude = Vector3.Magnitude(startPoint - endPoint);
        
        if (IsDirectionBlocked(startPoint, direction))
        {
            
        }
        return Physics.Raycast(startPoint, direction, distance);
    }
    public bool IsDeadlyFall(Vector3 startPoint)
    {
        return Physics.Raycast(startPoint, Vector3.down, Mathf.Infinity);
    }

    public bool IsDirectionFullBlocked(Vector3 startPoint, Vector3 direction)
    {
        bool isDirectionFullBlocked = (Physics.Raycast(startPoint + direction + Vector3.down, Vector3.up, 2) &&
                                       Physics.Raycast(startPoint - direction + Vector3.down, Vector3.up, 2)) ||
                                       Physics.Raycast(startPoint, Vector3.up, distance);

       /* Debug.Log(Physics.Raycast(startPoint + direction + Vector3.down, Vector3.up, out hitinfo, 2));
        Debug.Log(hitinfo.collider);
        Debug.Log(Physics.Raycast(startPoint - direction + Vector3.down, Vector3.up, out hitinfo, 2));
        Debug.Log(hitinfo.collider);
        Debug.Log(Physics.Raycast(startPoint, Vector3.up, out hitinfo, distance));
        Debug.Log(hitinfo.collider);
        Debug.DrawLine(startPoint + direction + Vector3.down, startPoint + direction + 2 * Vector3.up, Color.blue);
        Debug.DrawLine(startPoint - direction + Vector3.down, startPoint - direction + 2 * Vector3.up, Color.blue);*/

        return isDirectionFullBlocked;
    }
}