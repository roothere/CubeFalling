using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public enum Direction {
    rotateForward,
    rotateBack,
    rotateLeft,
    rotateRight,
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
        if (direction == Direction.fall && !checker.IsFalling()) ChangeDirectionToIdle();
        if (direction != Direction.idle) return;

        ReadInput();
    }

    void ReadInput() {
        switch (_axis.x = -Input.GetAxisRaw("Horizontal")) {
            case 1:
                direction = Direction.rotateLeft;
                Assemble(Vector3.left);
                break;
            case -1:
                direction = Direction.rotateRight;
                Assemble(Vector3.right);
                break;
            default:
                switch (_axis.z = Input.GetAxisRaw("Vertical")) {
                    case 1:
                        direction = Direction.rotateForward;
                        Assemble(Vector3.forward);
                        break;
                    case -1:
                        direction = Direction.rotateBack;
                        Assemble(Vector3.back);
                        break;
                }
                break;
        }
    }
    void Assemble(Vector3 dir) {
        int angular = 90;
        var anchor = transform.position + (Vector3.down + dir) * 0.5f;
        var axis = Vector3.Cross(Vector3.up, dir);
        if (checker.IsDirectionBlocked(dir))
        {
            anchor.y += 1;
            angular = 180;
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

        //transform.position = PosOnGrid();
        direction = Direction.idle;
    }

    IEnumerator Roll(Vector3 anchor, Vector3 axis, int angular) {

        for (int i = 0; i < (angular/ rollSpeed); i++) {
            transform.RotateAround(anchor, axis, rollSpeed);
            yield return new WaitForFixedUpdate();
        }

        if (checker.IsFalling()) 
            ChangeDirectionToFall();
        else 
            ChangeDirectionToIdle();
    }
}
