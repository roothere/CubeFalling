using System.Collections;
using UnityEngine;

public class HeroMoverRigidbody : MonoBehaviour
{
    [Header("Set in Inspector")]
    [SerializeField]
    private float _rollSpeed = 10;


    [Header("Set Dynamically")]

    public Direction direction = Direction.idle;
    private Vector3 _axis;
    private Rigidbody _rigid;

    public Vector3 anchor;
    public Vector3 axis;

    void Awake()
    {
        _rigid = GetComponent<Rigidbody>();
    }

    void Update() {

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
                direction = Direction.idle;
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
        anchor = transform.position - (Vector3.down + dir) * 0.5f;
        axis = Vector3.Cross(Vector3.up, dir);
        StartCoroutine(Roll(anchor, axis));
    }

    IEnumerator Roll(Vector3 anchor, Vector3 axis) {


        for (int i = 0; i < (90 / _rollSpeed); i++) {
            _rigid.AddForceAtPosition(_rollSpeed * axis, anchor);
            yield return new WaitForSeconds(0.01f);
        }

        direction = Direction.idle;
    }
}
