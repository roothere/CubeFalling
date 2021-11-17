using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public enum Direction {
    rotate,
    fall,
    idle
}

public class Movement : MonoBehaviour
{
    //public static UnityEvent<bool> OnFallingChange;

    [Header("Set in Inspector")] 
    public float rollSpeed = 10;
    public float gridMult = 1;


    [Header("Set Dynamically")]

    public Direction direction = Direction.idle;

    private Rigidbody rigidbody;
    private Raycast checker;
    private Vector3 _axis;

    public Vector3 levelPos {
        get {
            Vector3 tPos = transform.position;
            return tPos;
        }
        set {
            Vector3 tPos = transform.position;
            tPos = value;
        }
    }

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        checker = GetComponent<Raycast>();
    }

    void FixedUpdate() {
        if (direction == Direction.fall && !checker.IsFalling(transform.position)) ChangeDirectionToIdle();
        if (direction != Direction.idle) return;

        ReadInput();
    }

    void ReadInput() {
        switch (_axis.x = -Input.GetAxisRaw("Horizontal")) {
            case 1:
                Assemble(Vector3.left);
                break;
            case -1:
                Assemble(Vector3.right);
                break;
            default:
                switch (_axis.z = Input.GetAxisRaw("Vertical")) {
                    case 1:
                        Assemble(Vector3.forward);
                        break;
                    case -1:
                        Assemble(Vector3.back);
                        break;
                }
                break;
        }
    }
    void Assemble(Vector3 dir)
    {
        if (checker.IsDirectionFullBlocked(transform.position, dir)) return;
        
        int angular = 90;

        Vector3 curPos = transform.position;
        Vector3 anchor = curPos + (Vector3.down + dir) * 0.5f;
        Vector3 axis = Vector3.Cross(Vector3.up, dir);

        if (checker.IsDirectionBlocked(curPos, dir))
        {
            if (checker.IsDirectionLift(curPos, dir))
            {
                anchor += Vector3.up;
                angular = 180;
            } else
                anchor += Vector3.up;
        }

        StartCoroutine(Roll(anchor, axis, angular));
    }

    public Vector3 PosOnGrid(float mult = -1) {
        if (mult == -1) {
            mult = gridMult;
        }

        Vector3 lPos = transform.position;
        lPos /= mult;
        lPos.x = Mathf.Round(lPos.x);
        lPos.y = Mathf.Round(lPos.y);
        lPos.z = Mathf.Round(lPos.z);
        return lPos;
    }

    public void ChangeDirectionToFall()
    {
        rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
        rigidbody.isKinematic = false;

        direction = Direction.fall;
    }

    public void ChangeDirectionToIdle() {
        rigidbody.constraints = RigidbodyConstraints.FreezeRotationY;
        rigidbody.isKinematic = true;

        transform.position = PosOnGrid();
        direction = Direction.idle;
    }

    public void ChangeDirectionToRotate()
    {
        direction = Direction.rotate;
    }

    IEnumerator Roll(Vector3 anchor, Vector3 axis, int angular) {

        ChangeDirectionToRotate();

        for (int i = 0; i < (angular/ rollSpeed); i++) {
            transform.RotateAround(anchor, axis, rollSpeed);
            yield return new WaitForFixedUpdate();
        }

        if (checker.IsFalling(transform.position)) 
            ChangeDirectionToFall();
        else 
            ChangeDirectionToIdle();
    }
}
