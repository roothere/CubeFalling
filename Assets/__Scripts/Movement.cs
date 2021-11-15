using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public enum Direction {
    rotateForward,
    rotateBack,
    rotateLeft,
    rotateRight,
    falling,
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
        var anchor = transform.position + (Vector3.down + dir) * 0.5f;
        var axis = Vector3.Cross(Vector3.up, dir);
        if (checker.IsDirectionBlocked(dir)) anchor.y += 1;
        StartCoroutine(Roll(anchor, axis));
    }

    public Vector3 GetLevelPosOnGrid(float mult = -1) {
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

    public void ChangeFall(bool isKinematic = true, bool isFreezedRotation = false, bool isFalling = false)
    {
        if (isFreezedRotation) {
            rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
        } else {
            rigidbody.constraints = RigidbodyConstraints.FreezeRotationY;
        }

        rigidbody.isKinematic = isKinematic;

        if (isFalling) {
            direction = Direction.falling;
        } else {
            transform.position = GetLevelPosOnGrid(1);
            direction = Direction.idle;
        }
    }

    IEnumerator Roll(Vector3 anchor, Vector3 axis) {

        for (int i = 0; i < (90 / rollSpeed); i++) {
            transform.RotateAround(anchor, axis, rollSpeed);
            yield return new WaitForFixedUpdate();
        }

        transform.position = GetLevelPosOnGrid(1);
        //direction = Direction.idle;
    }
}
