using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 1.3f; // 이동 속도
    private Rigidbody rigidbody;
    public Camera mainCamera;
    public bool canMove; // 플레이어가 움직일 수 있는 상태인지

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        doMove(); // 플레이어 이동
    }



    void doMove()
    {
        if (canMove)
        {
            Vector3 dir = Vector3.zero;

            if (Input.GetKey(KeyCode.W)) // W -> 앞으로 이동
            {
                transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
                dir += Vector3.forward;
            }
            if (Input.GetKey(KeyCode.S)) // S -> 뒤로 이동
            {
                transform.Translate(Vector3.back * moveSpeed * Time.deltaTime);
                dir += Vector3.back;
            }
            if (Input.GetKey(KeyCode.A)) // A -> 왼쪽으로 이동
            {
                transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
                dir += Vector3.left;
            }
            if (Input.GetKey(KeyCode.D)) // D -> 오른쪽으로 이동
            {
                transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
                dir += Vector3.right;
            }
        }
    }
}
