using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSphere : MonoBehaviour
{

    public Vector3 axis;
    public float speed = 5f;

    private Rigidbody rigid;

    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
    }

    void Update()
    {
        ReadInput();
    }

    void ReadInput() {
         
        axis.z = Input.GetAxis("Horizontal");
        axis.x = -Input.GetAxis("Vertical");

        rigid.velocity = axis * speed;

        /*Vector3 pos = transform.position;

        pos.x += axis.x * speed * Time.deltaTime;
        pos.z += axis.z * speed * Time.deltaTime;
        transform.position = pos;*/
    }
}
