using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 1.3f; // �̵� �ӵ�
    private Rigidbody rigidbody;
    public Camera mainCamera;
    public bool canMove; // �÷��̾ ������ �� �ִ� ��������

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        doMove(); // �÷��̾� �̵�
    }



    void doMove()
    {
        if (canMove)
        {
            Vector3 dir = Vector3.zero;

            if (Input.GetKey(KeyCode.W)) // W -> ������ �̵�
            {
                transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
                dir += Vector3.forward;
            }
            if (Input.GetKey(KeyCode.S)) // S -> �ڷ� �̵�
            {
                transform.Translate(Vector3.back * moveSpeed * Time.deltaTime);
                dir += Vector3.back;
            }
            if (Input.GetKey(KeyCode.A)) // A -> �������� �̵�
            {
                transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
                dir += Vector3.left;
            }
            if (Input.GetKey(KeyCode.D)) // D -> ���������� �̵�
            {
                transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
                dir += Vector3.right;
            }
        }
    }
}
